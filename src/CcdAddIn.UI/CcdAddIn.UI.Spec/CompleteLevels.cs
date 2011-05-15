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
    public class CompleteLevels
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

        [Given(@"I start at the red level")]
        public void GivenIStartAtTheRedLevel()
        {
            _application = Application.Launch(@"..\..\CcdAddIn.TestHarness\bin\Debug\CcdAddIn.TestHarness.exe");
            _mainWindow = _application.GetWindow("MainWindow");
            _mainWindow.Get<Button>("goToRedLevelButton").Click();
        }

        [When(@"I browse through all colored levels")]
        public void WhenIBrowseThroughAllColoredLevels()
        {
            _mainWindow.Get<Button>("retrospectiveButton").Click();
            _mainWindow.Get<Button>("retrospectiveDoneButton").Click();
            _mainWindow.Get<Button>("denyAdviceButton").Click();

            var firstPrinciple = _mainWindow.Get<ListBox>("principlesListView").Items[0];
            Assert.That(firstPrinciple.Text, Is.StringContaining(Resource.SingleLevelOfAbstraction));

            _mainWindow.Get<Button>("retrospectiveButton").Click();
            _mainWindow.Get<Button>("retrospectiveDoneButton").Click();
            _mainWindow.Get<Button>("denyAdviceButton").Click();

            firstPrinciple = _mainWindow.Get<ListBox>("principlesListView").Items[0];
            Assert.That(firstPrinciple.Text, Is.StringContaining(Resource.InterfaceSegregationPrinciple));

            _mainWindow.Get<Button>("retrospectiveButton").Click();
            _mainWindow.Get<Button>("retrospectiveDoneButton").Click();
            _mainWindow.Get<Button>("denyAdviceButton").Click();

            firstPrinciple = _mainWindow.Get<ListBox>("principlesListView").Items[0];
            Assert.That(firstPrinciple.Text, Is.StringContaining(Resource.OpenClosedPrinciple));

            _mainWindow.Get<Button>("retrospectiveButton").Click();
            _mainWindow.Get<Button>("retrospectiveDoneButton").Click();
            _mainWindow.Get<Button>("denyAdviceButton").Click();
        }

        [Then(@"I will end at the blue level")]
        public void ThenIWillEndAtTheBlueLevel()
        {
            var firstPrinciple = _mainWindow.Get<ListBox>("principlesListView").Items[0];
            Assert.That(firstPrinciple.Text, Is.StringContaining(Resource.DesignAndImplementationDoNotOverlap));
        }

        [When(@"I advance to the next level")]
        public void WhenIAdvanceToTheNextLevel()
        {
            _mainWindow.Get<Button>("retrospectiveButton").Click();
            _mainWindow.Get<Button>("retrospectiveDoneButton").Click();
            _mainWindow.Get<Button>("denyAdviceButton").Click();
        }

        [Then(@"I should be at the white level")]
        public void ThenIShouldBeAtTheWhiteLevel()
        {
            Assert.That(_mainWindow.Get<Label>("whiteLevelLabel").Text, Is.StringContaining(Resource.WhiteLevelText));
        }

        [Then(@"I should not have the possibility to do a retrospective")]
        public void ThenIShouldNotHaveThePossibilityToDoARetrospective()
        {
            Assert.That(_mainWindow.Get<Button>("retrospectiveButton").IsOffScreen, Is.True);
        }

        [Then(@"I should have the possibility to begin again with the red level")]
        public void ThenIShouldHaveThePossibilityToBeginAgainWithTheRedLevel()
        {
            Assert.That(_mainWindow.Get<Button>("restartButton").IsOffScreen, Is.False);
        }

        [When(@"I restart the cycle")]
        public void WhenIRestartTheCycle()
        {
            _mainWindow.Get<Button>("restartButton").Click();
        }

        [Then(@"I should end at the red level again")]
        public void ThenIShouldEndAtTheRedLevelAgain()
        {
            var firstPrinciple = _mainWindow.Get<ListBox>("principlesListView").Items[0];
            Assert.That(firstPrinciple.Text, Is.StringContaining("Don't Repeat Yourself"));
        }
    }
}