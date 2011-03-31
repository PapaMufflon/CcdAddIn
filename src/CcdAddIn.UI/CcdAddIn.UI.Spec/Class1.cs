using NUnit.Framework;
using TechTalk.SpecFlow;
using White.Core;
using White.Core.UIItems;
using White.Core.UIItems.Finders;
using White.Core.UIItems.WindowItems;
using Button = White.Core.UIItems.Button;
using TextBox = White.Core.UIItems.TextBox;

namespace CcdAddIn.UI.Spec
{
    [Binding]
    public class StepDefinitions
    {
        private Window _mainWindow;
        private Label _ccdLevelTextBox;

        [Given(@"I am at the black level")]
        public void GivenIAmAtTheBlackLevel()
        {
            var application = Application.Launch(@"..\..\CcdAddIn.TestHarness\bin\Debug\CcdAddIn.TestHarness.exe");
            _mainWindow = application.GetWindow("MainWindow");
            
            if (_mainWindow == null)
                Assert.Fail("Cannot find MainWindow");

            _ccdLevelTextBox = _mainWindow.Get<Label>("ccdLevelTextBox");

            if (_ccdLevelTextBox == null)
                Assert.Fail("Cannot find ccdLevelTextBox");

            Assert.That(_ccdLevelTextBox.Text, Is.EqualTo("Schwarz"));
        }

        [When(@"I browse to the next one")]
        public void WhenIBrowseToTheNextOne()
        {
            var nextLevelButton = _mainWindow.Get<Button>("nextLevelButton");
            nextLevelButton.Click();
        }

        [Then(@"I should see the red level")]
        public void ThenIShouldSeeTheRedLevel()
        {
            Assert.That(_ccdLevelTextBox.Text, Is.EqualTo("Rot"));
        }
    }
}
