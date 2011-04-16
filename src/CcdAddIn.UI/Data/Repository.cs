using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using CcdAddIn.UI.CleanCodeDeveloper;

namespace CcdAddIn.UI.Data
{
    public class Repository : IRepository
    {
        public List<CcdLevel> Retrospectives { get; set; }

        private const string FileName = "repository";

        private readonly IFileService _fileService;

        public Repository(IFileService fileService)
        {
            _fileService = fileService;

            var content = _fileService.OpenAsString(FileName);
            var document = XDocument.Load(new StringReader(content));

            if (document == null)
                throw new InvalidOperationException("Wrong content in repository.");

            ReadOutRetrospectives(document);
        }

        public void SaveChanges()
        {
            var history = new XElement("History");

            foreach (var retrospective in Retrospectives)
            {
                var retrospectiveElement = new XElement("Retrospective",
                                                        new XAttribute("Level", retrospective.Level));

                foreach (var principle in retrospective.Principles)
                {
                    retrospectiveElement.Add(new XElement("Item",
                                                          new XAttribute("Name", principle.Name),
                                                          new XAttribute("Value", principle.EvaluationValue)));
                }

                foreach (var practice in retrospective.Practices)
                {
                    retrospectiveElement.Add(new XElement("Item",
                                                          new XAttribute("Name", practice.Name),
                                                          new XAttribute("Value", practice.EvaluationValue)));
                }

                history.Add(retrospectiveElement);
            }

            var repository = new XDocument(new XElement("Repository", history));
            _fileService.WriteTo(repository.ToString(), FileName);
        }

        private void ReadOutRetrospectives(XDocument document)
        {
            Retrospectives = new List<CcdLevel>();

            if (document.Root.Element("History") == null)
                return;

            foreach (var retrospectiveElement in document.Root.Element("History").Descendants("Retrospective"))
            {
                Level level;

                if (!Enum.TryParse(retrospectiveElement.Attribute("Level").Value, out level))
                    throw new InvalidOperationException("Wrong content in repository.");

                var ccdLevel = new CcdLevel(level);

                foreach (var practice in ccdLevel.Practices)
                {
                    var practiceElement = (from x in retrospectiveElement.Descendants()
                                           where x.Attribute("Name").Value.Equals(practice.Name.ToString())
                                           select x).FirstOrDefault();

                    if (practiceElement == null)
                        throw new InvalidOperationException("Wrong content in repository.");

                    practice.EvaluationValue = int.Parse(practiceElement.Attribute("Value").Value);
                }

                foreach (var principle in ccdLevel.Principles)
                {
                    var principleElement = (from x in retrospectiveElement.Descendants()
                                           where x.Attribute("Name").Value.Equals(principle.Name.ToString())
                                           select x).FirstOrDefault();

                    if (principleElement == null)
                        throw new InvalidOperationException("Wrong content in repository.");

                    principle.EvaluationValue = int.Parse(principleElement.Attribute("Value").Value);
                }

                Retrospectives.Add(ccdLevel);
            }
        }
    }
}