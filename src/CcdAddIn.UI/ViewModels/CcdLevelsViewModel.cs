using System.Collections.Generic;

namespace CcdAddIn.UI.ViewModels
{
    public class CcdLevelsViewModel
    {
        private CcdLevel _currentLevel;
        private List<string> _principles = new List<string>();
        private List<string> _practices = new List<string>();

        public CcdLevelsViewModel()
        {
            _principles.Add("Open Closed Principle (OCP)");
            _principles.Add("Tell don't ask");
            _principles.Add("Law of Demeter (LoD)");

            _practices.Add("Continuous Integration (CI)");
            _practices.Add("Statische Codeanalyse");
            _practices.Add("Inversion of Control Container");
            _practices.Add("Erfahrung weitergeben");
            _practices.Add("Messen von Fehlern");
        }

        public CcdLevel CurrentLevel
        {
            get { return _currentLevel; }
        }

        public List<string> Principles
        {
            get { return _principles; }
        }

        public List<string> Practices
        {
            get { return _practices; }
        }
    }
}
