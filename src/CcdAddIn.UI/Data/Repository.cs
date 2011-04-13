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
        private XDocument _document;

        public Repository(IFileService fileService)
        {
            var content = fileService.OpenAsString("repository");

            _document = XDocument.Load(new StringReader(content));

            if (_document == null)
                throw new InvalidOperationException("Wrong content in repository.");
        }

        public List<CcdLevel> GetRetrospectives()
        {
            var retrospectives = new List<CcdLevel>();

            if (_document.Root.Element("History") == null)
                return retrospectives;

            foreach (var retrospectiveElement in _document.Root.Element("History").Descendants("Retrospective"))
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

                retrospectives.Add(ccdLevel);
            }

            return retrospectives;
        }
    }
}