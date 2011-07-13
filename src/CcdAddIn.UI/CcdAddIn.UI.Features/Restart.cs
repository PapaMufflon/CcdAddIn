using CcdAddIn.UI.Resources;
using NUnit.Framework;
using TechTalk.SpecFlow;
using White.Core;
using White.Core.UIItems;
using White.Core.UIItems.Finders;
using White.Core.UIItems.ListBoxItems;
using White.Core.UIItems.WindowItems;

namespace CcdAddIn.UI.Features
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

        [Given(@"I have done nearly enough retrospectives to advance a level")]
        public void GivenIHaveDoneNearlyEnoughRetrospectivesToAdvanceALevel()
        {
            _mainWindow.Get<Button>("retrospectiveButton").Click();
            var firstPrinciple = _mainWindow.Get<ListBox>("principlesListView").Items[0];
            var slider = firstPrinciple.GetElement(SearchCriteria.ByControlType(typeof(WPFSlider)));
            
            // how to slide slider to value 100?
            ScenarioContext.Current.Pending();
        }

        [Given(@"doing the next to last retrospective for advancing a level")]
        public void GivenDoingTheNextToLastRetrospectiveForAdvancingALevel()
        {
            ScenarioContext.Current.Pending();
        }

        [When(@"doing the last retrospective for advancing a level")]
        public void WhenDoingTheLastRetrospectiveForAdvancingALevel()
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"I should get an advice to advance to the next level")]
        public void ThenIShouldGetAnAdviceToAdvanceToTheNextLevel()
        {
            ScenarioContext.Current.Pending();
        }
    }
}