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

        [AfterScenario(null)]
        public void AfterScenario()
        {
            if (_application != null)
                _application.Dispose();
        }

        [Given(@"I start at the red level")]
        public void GivenIStartAtTheRedLevel()
        {
            _application = Application.Launch(@"..\..\CcdAddIn.TestHarness\bin\Debug\CcdAddIn.TestHarness.exe");
            _mainWindow = _application.GetWindow("MainWindow");
            _mainWindow.Get<Button>("goToRedLevelButton").Click();
        }

        [When(@"I browse through all levels")]
        public void WhenIBrowseThroughAllLevels()
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
    }
}