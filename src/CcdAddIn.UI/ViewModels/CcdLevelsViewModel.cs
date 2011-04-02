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
            _principles.Add("Don't Repeat Yourself (DRY)");
            _principles.Add("Keep It Simple, Stupid (KISS)");
            _principles.Add("Vorsicht vor Optimierungen");
            _principles.Add("Favor Composition over Inheritance (FCoI)");

            _practices.Add("Pfadfinderregel");
            _practices.Add("Root Cause Analysis");
            _practices.Add("Versionskontrolle");
            _practices.Add("Einfache Refaktorisierungen");
            _practices.Add("Täglich reflektieren");

            _currentLevel = CcdLevel.Red;
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
