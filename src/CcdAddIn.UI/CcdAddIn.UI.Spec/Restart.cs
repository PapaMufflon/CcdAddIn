using CcdAddIn.UI.Resources;
using NUnit.Framework;
using TechTalk.SpecFlow;
using White.Core;
using White.Core.UIItems;
using White.Core.UIItems.ListBoxItems;
using White.Core.UIItems.WindowItems;

namespace CcdAddIn.UI.Spec
{
    [Binding]
    public class Restart
    {
        private Application _application;
        private Window _mainWindow;

        [BeforeScenario]
        public void BeforeScenario()
        {
            System.IO.File.Delete(@"..\..\CcdAddIn.TestHarness\bin\Debug\repository_save");

            if (System.IO.File.Exists(@"..\..\CcdAddIn.TestHarness\bin\Debug\repository"))
                System.IO.File.Copy(@"..\..\CcdAddIn.TestHarness\bin\Debug\repository", @"..\..\CcdAddIn.TestHarness\bin\Debug\repository_save");

            System.IO.File.Delete(@"..\..\CcdAddIn.TestHarness\bin\Debug\repository");
        }

        [AfterScenario(null)]
        public void AfterScenario()
        {
            if (_application != null)
                _application.Dispose();

            System.IO.File.Delete(@"..\..\CcdAddIn.TestHarness\bin\Debug\repository");

            if (System.IO.File.Exists(@"..\..\CcdAddIn.TestHarness\bin\Debug\repository_save"))
                System.IO.File.Copy(@"..\..\CcdAddIn.TestHarness\bin\Debug\repository_save", @"..\..\CcdAddIn.TestHarness\bin\Debug\repository");

            System.IO.File.Delete(@"..\..\CcdAddIn.TestHarness\bin\Debug\repository_save");
        }

        [Given(@"I am at the red level")]
        public void GivenIAmAtTheRedLevel()
        {
            _application = Application.Launch(@"..\..\CcdAddIn.TestHarness\bin\Debug\CcdAddIn.TestHarness.exe");
            _mainWindow = _application.GetWindow("MainWindow");
            _mainWindow.Get<Button>("goToRedLevelButton").Click();

            var firstPrinciple = _mainWindow.Get<ListBox>("principlesListView").Items[0];
            Assert.That(firstPrinciple.Text, Is.StringContaining(Resource.DoNotRepeatYourself));
        }

        [When(@"restarting the application")]
        public void WhenRestartingTheApplication()
        {
            _application.Kill();
            _application = Application.Launch(@"..\..\CcdAddIn.TestHarness\bin\Debug\CcdAddIn.TestHarness.exe");
            _mainWindow = _application.GetWindow("MainWindow");
        }

        [Then(@"I am still at the red level")]
        public void ThenIAmStillAtTheRedLevel()
        {
            var firstPrinciple = _mainWindow.Get<ListBox>("principlesListView").Items[0];
            Assert.That(firstPrinciple.Text, Is.StringContaining(Resource.DoNotRepeatYourself));
        }
    }
}