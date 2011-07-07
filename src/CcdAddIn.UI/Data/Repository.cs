using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using CcdAddIn.UI.CleanCodeDeveloper;
using NLog;

namespace CcdAddIn.UI.Data
{
    public class Repository : IRepository
    {
        public List<CcdLevel> Retrospectives { get; set; }

        private const string FileName = "repository";

        private static Logger _logger = LogManager.GetCurrentClassLogger();

        private readonly IFileService _fileService;
        private CcdLevel _currentLevel;

        public Repository(IFileService fileService)
        {
            _logger.Trace("Creating repository");
            _fileService = fileService;

            Initialize();
        }

        private void Initialize()
        {
            var content = _fileService.OpenAsString(FileName);

            if (string.IsNullOrEmpty(content))
                content = "<Repository/>";

            try
            {
                var document = XDocument.Load(new StringReader(content));
                ReadOutRetrospectives(document);
            }
            catch (Exception e)
            {
                _logger.Fatal("Unable to load repository");
                throw new InvalidOperationException("Wrong content in repository.", e);
            }

            var lastRetrospective = Retrospectives.FirstOrDefault();

            if (lastRetrospective == null)
                _currentLevel = new CcdLevel(Level.Black);
            else
                _currentLevel = lastRetrospective.Clone();
        }

        private void ReadOutRetrospectives(XDocument document)
        {
            Retrospectives = new List<CcdLevel>();

            if (document.Root.Element("History") == null)
            {
                _logger.Info("Repository does not have a History-element. Assuming a first start.");
                return;
            }

            foreach (var retrospectiveElement in document.Root.Element("History").Descendants("Retrospective"))
            {
                _logger.Trace("Parsing {0}", retrospectiveElement.ToString());
                Level level;

                var levelValue = retrospectiveElement.Attribute("Level").Value;

                if (!Enum.TryParse(levelValue, out level))
                {
                    _logger.Error("Cannot parse level: {0}", levelValue);
                    throw new InvalidOperationException("Wrong content in repository.");
                }

                var ccdLevel = new CcdLevel(level);

                foreach (var practice in ccdLevel.Practices)
                {
                    var practiceElement = (from x in retrospectiveElement.Descendants()
                                           where x.Attribute("Name").Value.Equals(practice.Name.ToString())
                                           select x).FirstOrDefault();

                    if (practiceElement == null)
                    {
                        _logger.Error("Cannot find practice {0}", practice.Name);
                        throw new InvalidOperationException("Wrong content in repository.");
                    }

                    _logger.Info("Found practice {0}, parsing value from {1}", practice.Name, practiceElement.ToString());
                    practice.EvaluationValue = int.Parse(practiceElement.Attribute("Value").Value);
                }

                foreach (var principle in ccdLevel.Principles)
                {
                    var principleElement = (from x in retrospectiveElement.Descendants()
                                            where x.Attribute("Name").Value.Equals(principle.Name.ToString())
                                            select x).FirstOrDefault();

                    if (principleElement == null)
                    {
                        _logger.Error("Cannot find principle {0}", principle.Name);
                        throw new InvalidOperationException("Wrong content in repository.");
                    }

                    _logger.Info("Found principle {0}, parsing value from {1}", principle.Name, principleElement.ToString());
                    principle.EvaluationValue = int.Parse(principleElement.Attribute("Value").Value);
                }

                _logger.Trace("Found all elements, adding retrospective");
                Retrospectives.Add(ccdLevel);
            }
        }

        public CcdLevel CurrentLevel
        {
            get { return _currentLevel; }
        }

        public void SaveChanges()
        {
            var history = new XElement("History");

            _logger.Trace("Creating retrospectives");
            foreach (var retrospective in (new List<CcdLevel> { _currentLevel }).Concat(Retrospectives))
            {
                var retrospectiveElement = new XElement("Retrospective",
                                                        new XAttribute("Level", retrospective.Level));

                foreach (var principle in retrospective.Principles)
                {
                    retrospectiveElement.Add(new XElement("Item",
                                                          new XAttribute("Name", principle.Name),
                                                          new XAttribute("Value", principle.EvaluationValue)));
                    _logger.Trace("Converted principle {0} with value {1} to xml: {2}",
                                  principle.Name,
                                  principle.EvaluationValue,
                                  retrospectiveElement.LastNode.ToString());
                }

                foreach (var practice in retrospective.Practices)
                {
                    retrospectiveElement.Add(new XElement("Item",
                                                          new XAttribute("Name", practice.Name),
                                                          new XAttribute("Value", practice.EvaluationValue)));
                    _logger.Trace("Converted practice {0} with value {1} to xml: {2}",
                                  practice.Name,
                                  practice.EvaluationValue,
                                  retrospectiveElement.LastNode.ToString());
                }

                history.Add(retrospectiveElement);
            }

            var repository = new XDocument(new XElement("Repository", history));
            var repositoryAsXml = repository.ToString();

            _logger.Trace("Writing {0} as repository to {1}", repositoryAsXml, FileName);
            _fileService.WriteTo(repositoryAsXml, FileName);
        }
    }
}                       