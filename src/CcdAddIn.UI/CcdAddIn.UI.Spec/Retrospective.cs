using CcdAddIn.UI.Resources;
using NUnit.Framework;
using TechTalk.SpecFlow;
using White.Core;
using White.Core.UIItems;
using White.Core.UIItems.Finders;
using White.Core.UIItems.ListBoxItems;
using White.Core.UIItems.WindowItems;

namespace CcdAddIn.UI.Spec
{
    [Binding]
    public class Retrospective
    {
        private Application _application;
        private Window _mainWindow;

        [AfterScenario(null)]
        public void AfterScenario()
        {
            if (_application != null)
                _application.Dispose();
        }

        [Given(@"I am at a non-black level")]
        public void GivenIAmAtANon_BlackLevel()
        {
            _application = Application.Launch(@"..\..\CcdAddIn.TestHarness\bin\Debug\CcdAddIn.TestHarness.exe");
            _mainWindow = _application.GetWindow("MainWindow");
            _mainWindow.Get<Button>("goToRedLevelButton").Click();

            var firstPrinciple = _mainWindow.Get<ListBox>("principlesListView").Items[0];
            Assert.That(firstPrinciple.Text, Is.StringContaining(Resource.DoNotRepeatYourself));
        }

        [When(@"I click on retrospective")]
        public void WhenIClickOnRetrospective()
        {
            _mainWindow.Get<Button>("retrospectiveButton").Click();
        }

        [Then(@"I should be able to evaluate the principles and practices")]
        public void ThenIShouldBeAbleToEvaluateThePrinciplesAndPractices()
        {
            var firstPrinciple = _mainWindow.Get<ListBox>("principlesListView").Items[0];
            var slider = firstPrinciple.GetElement(SearchCriteria.ByControlType(typeof (Slider)));

            Assert.That(slider.Current.IsOffscreen, Is.False);
        }

        [Then(@"the retrospective-button should be invisible")]
        public void ThenTheRetrospective_ButtonShouldBeInvisible()
        {
            Assert.That(_mainWindow.Get<Button>("retrospectiveButton").IsOffScreen, Is.True);
        }

        [When(@"I finish my retrospective")]
        public void WhenIFinishMyRetrospective()
        {
            _mainWindow.Get<Button>("retrospectiveButton").Click();
            _mainWindow.Get<Button>("retrospectiveDoneButton").Click();
        }

        [Then(@"I should see an advice reflecting my performance")]
        public void ThenIShouldSeeAnAdviceReflectingMyPerformance()
        {
            Assert.That(_mainWindow.Get<Label>("adviceDocument").Text, Is.StringContaining(Resource.NegativeAdvice));
        }

        [Given(@"I finish my retrospective with no suggestion to advance to the next level")]
        public void GivenIFinishMyRetrospectiveWithNoSuggestionToAdvanceToTheNextLevel()
        {
            _mainWindow.Get<Button>("retrospectiveButton").Click();
            _mainWindow.Get<Button>("retrospectiveDoneButton").Click();
        }

        [When(@"I accept that")]
        public void WhenIAcceptThat()
        {
            _mainWindow.Get<Button>("takeAdviceButton").Click();
        }

        [Then(@"I should stay at the current level")]
        public void ThenIShouldStayAtTheCurrentLevel()
        {
            var firstPrinciple = _mainWindow.Get<ListBox>("principlesListView").Items[0];
            Assert.That(firstPrinciple.Text, Is.StringContaining(Resource.DoNotRepeatYourself));
        }

        [Given(@"I make a retrospective")]
        public void GivenIMakeARetrospective()
        {
            _mainWindow.Get<Button>("retrospectiveButton").Click();
        }

        [When(@"I finish the retrospective")]
        public void WhenIFinishTheRetrospective()
        {
            _mainWindow.Get<Button>("retrospectiveDoneButton").Click();
        }

        [Then(@"I should not be able to evaluate the principles and practices")]
        public void ThenIShouldNotBeAbleToEvaluateThePrinciplesAndPractices()
        {
            var listBox = _mainWindow.Get<ListBox>("principlesListView");
            Assert.IsNull(listBox);
        }

        [Then(@"the retrospective-button should be visible again")]
        public void ThenTheRetrospective_ButtonShouldBeVisibleAgain()
        {
            Assert.That(_mainWindow.Get<Button>("retrospectiveButton").IsOffScreen, Is.False);
        }

        [Given(@"I finish my retrospective with a suggestion to advance to the next level")]
        public void GivenIFinishMyRetrospectiveWithASuggestionToAdvanceToTheNextLevel()
        {
            ScenarioContext.Current.Pending();
        }

        [When(@"I decline to advance")]
        public void WhenIDeclineToAdvance()
        {
            ScenarioContext.Current.Pending();
        }
    }
}