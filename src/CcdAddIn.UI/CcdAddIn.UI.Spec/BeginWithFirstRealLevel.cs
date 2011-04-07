using NUnit.Framework;
using TechTalk.SpecFlow;
using White.Core;
using White.Core.UIItems;
using White.Core.UIItems.ListBoxItems;
using White.Core.UIItems.WindowItems;
using Button = White.Core.UIItems.Button;

namespace CcdAddIn.UI.Spec
{
    [Binding]
    public class StepDefinitions
    {
        private Window _mainWindow;
        private string _mainText;
        private Application _application;
        private Button _retroButton;

        [AfterScenario(null)]
        public void AfterScenario()
        {
            if (_application != null)
                _application.Dispose();
        }

        [Given(@"I start the addin for the first time")]
        public void GivenIStartTheAddinForTheFirstTime()
        {
            _application = Application.Launch(@"..\..\CcdAddIn.TestHarness\bin\Debug\CcdAddIn.TestHarness.exe");
            _mainWindow = _application.GetWindow("MainWindow");
        }

        [When(@"I read the main text")]
        public void WhenIReadTheMainText()
        {
            var paragraph = _mainWindow.Get<Label>("warmWelcomeMessageDocument");
            _mainText = paragraph.Text;
        }

        [Then(@"it should be a warm welcome message")]
        public void ThenItShouldBeAWarmWelcomeMessage()
        {
            Assert.That(_mainText, Is.StringContaining("Warm welcome message"));
        }

        [Given(@"I am at the black level")]
        public void GivenIAmAtTheBlackLevel()
        {
            _application = Application.Launch(@"..\..\CcdAddIn.TestHarness\bin\Debug\CcdAddIn.TestHarness.exe");
            _mainWindow = _application.GetWindow("MainWindow");
        }

        [When(@"I search for a retro-button")]
        public void WhenISearchForARetro_Button()
        {
            _retroButton = _mainWindow.Get<Button>("retrospectiveButton");
        }

        [Then(@"I should not find one")]
        public void ThenIShouldNotFindOne()
        {
            Assert.That(_retroButton.Visible, Is.False);
        }

        [When(@"I browse to the next one")]
        public void WhenIBrowseToTheNextOne()
        {
            var nextLevelButton = _mainWindow.Get<Button>("goToRedLevelButton");
            nextLevelButton.Click();
        }

        [Then(@"I should see the red level")]
        public void ThenIShouldSeeTheRedLevel()
        {
            var firstPrinciple = _mainWindow.Get<ListBox>("principlesListView").Items[0];
            Assert.That(firstPrinciple.Text, Is.StringContaining("Don't Repeat Yourself"));
        }
    }
}
