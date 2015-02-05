using System.Configuration;
using CodeBySpecification.API.Service.Api;
using Selenium.Base.Service;
using TechTalk.SpecFlow;

namespace CodeBySpecification.Core
{
	[Binding]
	public class FeatureBase
	{ //TODO: At present this class has too many responsibility, but we will refactor it once we have the basic system avilable.
		private static readonly IUIAutomationService UiFeatureTestsHelper = new SeleniumUIAutomationService();

		#region Core Step Definition Vocabulary

		[BeforeFeature("UIAutomationTest")]
		public static void BeforeSeleniumTestFeature()
		{
			var browserName = ConfigurationManager.AppSettings["UI.Tests.Target.Browser"];
			var objectDefSource = ConfigurationManager.AppSettings["UI.Tests.Object.Definitions.Path"];

			UiFeatureTestsHelper.InitilizeTests(browserName, objectDefSource);
		}

		[AfterFeature("UIAutomationTest")]
		public static void AfterSeleniumTestFeature()
		{
			//Browser.Quit();
		}

		#region Read the content of <element>

		[Given(@"Read the content of ""(.*)""")]
		[When(@"Read the content of ""(.*)""")]
		[Then(@"Read the content of ""(.*)""")]
		public void ReadTheContentOf(string elementKey)
		{
			FeatureContext.Current[elementKey] = UiFeatureTestsHelper.GetElementText(elementKey);
		}

		[Given(@"Read the content of ""(.*)"" \(with the ""(.*)"" of ""(.*)""\)")]
		[When(@"Read the content of ""(.*)"" \(with the ""(.*)"" of ""(.*)""\)")]
		[Then(@"Read the content of ""(.*)"" \(with the ""(.*)"" of ""(.*)""\)")]
		public void ReadTheContentOfWithTheOf(string elementKey, string selectionType, string selection)
		{
			FeatureContext.Current[elementKey] = UiFeatureTestsHelper.GetElementText(elementKey, selectionType, selection);
		}

		#endregion

		#region The content of <element> is equal to <expected value>

		[Given(@"Content of ""(.*)"" is equal to ""(.*)""")]
		[When(@"Content of ""(.*)"" is equal to ""(.*)""")]
		[Then(@"Content of ""(.*)"" is equal to ""(.*)""")]
		[Given(@"The content of ""(.*)"" is equal to ""(.*)""")]
		[When(@"The content of ""(.*)"" is equal to ""(.*)""")]
		[Then(@"The content of ""(.*)"" is equal to ""(.*)""")]
		public void TheConentOfIsEqualTo(string elementKey, string expectedContent)
		{
			UiFeatureTestsHelper.IsElementContentEqual(elementKey, expectedContent);
		}

		[Given(@"The content of ""(.*)"" \(with the ""(.*)"" of ""(.*)""\) is equal to ""(.*)""")]
		[When(@"The content of ""(.*)"" \(with the ""(.*)"" of ""(.*)""\) is equal to ""(.*)""")]
		[Then(@"The content of ""(.*)"" \(with the ""(.*)"" of ""(.*)""\) is equal to ""(.*)""")]
		[Given(@"Content of ""(.*)"" \(with the ""(.*)"" of ""(.*)""\) is equal to ""(.*)""")]
		[When(@"Content of ""(.*)"" \(with the ""(.*)"" of ""(.*)""\) is equal to ""(.*)""")]
		[Then(@"Content of ""(.*)"" \(with the ""(.*)"" of ""(.*)""\) is equal to ""(.*)""")]
		public void TheConentOfWithTheOfIsEqualTo(string elementKey, string selectionMethod, string selection, string expectedContent)
		{
			UiFeatureTestsHelper.IsElementContentEqual(elementKey, selectionMethod, selection, expectedContent);
		}

		#endregion

		#region Click on <element>

		[Given(@"I click on ""(.*)""")]
		[When(@"I click on ""(.*)""")]
		[Then(@"I click on ""(.*)""")]
		[Given(@"Click on ""(.*)""")]
		[When(@"Click on ""(.*)""")]
		[Then(@"Click on ""(.*)""")]
		public void IClickOn(string elementKey)
		{
			UiFeatureTestsHelper.ClickOn(elementKey);
		}

		[Given(@"I click on ""(.*)"" \(with the ""(.*)"" of ""(.*)""\)")]
		[When(@"I click on ""(.*)"" \(with the ""(.*)"" of ""(.*)""\)")]
		[Then(@"I click on ""(.*)"" \(with the ""(.*)"" of ""(.*)""\)")]
		[Given(@"Click on ""(.*)"" \(with the ""(.*)"" of ""(.*)""\)")]
		[When(@"Click on ""(.*)"" \(with the ""(.*)"" of ""(.*)""\)")]
		[Then(@"Click on ""(.*)"" \(with the ""(.*)"" of ""(.*)""\)")]
		public void IClickOnWithTheOf(string elementKey, string selectionMethod, string selection)
		{
			UiFeatureTestsHelper.ClickOn(elementKey, selectionMethod, selection);
		}

		#endregion

		#region Enter <value> to <element>

		[Given(@"I enter ""(.*)"" to the ""(.*)""")]
		[When(@"I enter ""(.*)"" to the ""(.*)""")]
		[Then(@"I enter ""(.*)"" to the ""(.*)""")]
		[Given(@"Enter ""(.*)"" to the ""(.*)""")]
		[When(@"Enter ""(.*)"" to the ""(.*)""")]
		[Then(@"Enter ""(.*)"" to the ""(.*)""")]
		public void IEnterToThe(string value, string elementKey)
		{
			UiFeatureTestsHelper.EnterTextTo(elementKey, value);
		}

		[Given(@"I enter ""(.*)"" to the ""(.*)"" \(with the ""(.*)"" of ""(.*)""\)")]
		[When(@"I enter ""(.*)"" to the ""(.*)"" \(with the ""(.*)"" of ""(.*)""\)")]
		[Then(@"I enter ""(.*)"" to the ""(.*)"" \(with the ""(.*)"" of ""(.*)""\)")]
		[Given(@"Enter ""(.*)"" to the ""(.*)"" \(with the ""(.*)"" of ""(.*)""\)")]
		[When(@"Enter ""(.*)"" to the ""(.*)"" \(with the ""(.*)"" of ""(.*)""\)")]
		[Then(@"Enter ""(.*)"" to the ""(.*)"" \(with the ""(.*)"" of ""(.*)""\)")]
		public void IEnterToTheWithTheOf(string value, string elementKey, string selectionMethod, string selection)
		{
			UiFeatureTestsHelper.EnterTextTo(elementKey, value, selectionMethod, selection);
		}

		#endregion

		#region Navigate to SUT

		[Given(@"I navigate to System Under Test")]
		[When(@"I navigate to System Under Test")]
		[Then(@"I navigate to System Under Test")]
		[Given(@"I navigate to SUT")]
		[When(@"I navigate to SUT")]
		[Then(@"I navigate to SUT")]
		[Given(@"Navigate to System Under Test")]
		[When(@"Navigate to System Under Test")]
		[Then(@"Navigate to System Under Test")]
		[Given(@"Navigate to SUT")]
		[When(@"Navigate to SUT")]
		[Then(@"Navigate to SUT")]
		public void INavigateToSut()
		{
			UiFeatureTestsHelper.GotoUrl(UiFeatureTestsHelper.SutUrl);
		}

		#endregion

		#region Navigate to <URL>

		[Given(@"I navigate to ""(.*)""")]
		[When(@"I navigate to ""(.*)""")]
		[Then(@"I navigate to ""(.*)""")]
		[Given(@"Navigate to ""(.*)""")]
		[When(@"Navigate to ""(.*)""")]
		[Then(@"Navigate to ""(.*)""")]
		public void INavigateTo(string url)
		{
			UiFeatureTestsHelper.GotoUrl(url);
		}

		#endregion

		#region The <Element> is Visible

		[Given(@"""(.*)"" is visible")]
		[Then(@"""(.*)"" is visible")]
		[When(@"""(.*)"" is visible")]
		[Given(@"The ""(.*)"" is visible")]
		[Then(@"The ""(.*)"" is visible")]
		[When(@"The ""(.*)"" is visible")]
		[Given(@"I wait for the ""(.*)"" to appear")]
		[When(@"I wait for the ""(.*)"" to appear")]
		[Then(@"I wait for the ""(.*)"" to appear")]
		[Given(@"Wait for the ""(.*)"" to appear")]
		[When(@"Wait for the ""(.*)"" to appear")]
		[Then(@"Wait for the ""(.*)"" to appear")]
		public void TheElementIsVisible(string elementKey)
		{
			UiFeatureTestsHelper.IsElementVisible(elementKey);
		}

		[Given(@"The ""(.*)"" \(with the ""(.*)"" of ""(.*)""\) is visible")]
		[Then(@"The ""(.*)"" \(with the ""(.*)"" of ""(.*)""\) is visible")]
		[When(@"The ""(.*)"" \(with the ""(.*)"" of ""(.*)""\) is visible")]
		[Given(@"I wait for the ""(.*)"" \(with the ""(.*)"" of ""(.*)""\) to appear")]
		[When(@"I wait for the ""(.*)"" \(with the ""(.*)"" of ""(.*)""\) to appear")]
		[Then(@"I wait for the ""(.*)"" \(with the ""(.*)"" of ""(.*)""\) to appear")]
		[Given(@"""(.*)"" \(with the ""(.*)"" of ""(.*)""\) is visible")]
		[Then(@"""(.*)"" \(with the ""(.*)"" of ""(.*)""\) is visible")]
		[When(@"""(.*)"" \(with the ""(.*)"" of ""(.*)""\) is visible")]
		[Given(@"Wait for the ""(.*)"" \(with the ""(.*)"" of ""(.*)""\) to appear")]
		[When(@"Wait for the ""(.*)"" \(with the ""(.*)"" of ""(.*)""\) to appear")]
		[Then(@"Wait for the ""(.*)"" \(with the ""(.*)"" of ""(.*)""\) to appear")]
		public void TheElementWithTheOfIsVisible(string elementKey, string selectionMethod, string selection)
		{
			UiFeatureTestsHelper.IsElementVisible(elementKey, selectionMethod, selection);
		}

		#endregion

		#region Accept the confirmation

		[Given(@"I accept the confirmation")]
		[Then(@"I accept the confirmation")]
		[When(@"I accept the confirmation")]
		[Given(@"I accept the confirmation alert")]
		[Then(@"I accept the confirmation alert")]
		[When(@"I accept the confirmation alert")]
		[Given(@"Accept the confirmation")]
		[Then(@"Accept the confirmation")]
		[When(@"Accept the confirmation")]
		[Given(@"Accept the confirmation alert")]
		[Then(@"Accept the confirmation alert")]
		[When(@"Accept the confirmation alert")]
		public void IAcceptTheConfirmation()
		{
			UiFeatureTestsHelper.AcceptTheConfirmation();
		}

		#endregion

		#endregion
	}
}
