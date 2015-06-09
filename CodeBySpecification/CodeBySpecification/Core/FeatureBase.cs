using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing.Printing;
using System.Linq.Expressions;
using CodeBySpecification.API.Service.Api;
using Selenium.Base.Service;
using TechTalk.SpecFlow;

namespace CodeBySpecification.Core
{
	[Binding]
	public class FeatureBase
	{ //TODO: At present this class has too many responsibility, but we will refactor it once we have the basic system avilable.
		private static readonly IUIAutomationService UiFeatureTestsHelper = new SeleniumUIAutomationService();
		private static IDictionary<string, string> dataShare = new Dictionary<string, string>();
		private static string objectRepoResource = null;

		#region Core Step Definition Vocabulary

		[BeforeFeature("UIAutomationTest")]
		public static void BeforeSeleniumTestFeature()
		{
			var browserName = ConfigurationManager.AppSettings["UI.Tests.Target.Browser"];
			objectRepoResource = ConfigurationManager.AppSettings["UI.Tests.Object.Definitions.Path"];

			UiFeatureTestsHelper.InitilizeTests(browserName, objectRepoResource);
		}

		[AfterFeature("UIAutomationTest")]
		public static void AfterSeleniumTestFeature()
		{
			UiFeatureTestsHelper.Dispose();
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

		[Given(@"Content of ""(.*)"" contains text ""(.*)""")]
		[When(@"Content of ""(.*)"" contains text ""(.*)""")]
		[Then(@"Content of ""(.*)"" contains text ""(.*)""")]
		[Given(@"The content of ""(.*)"" contains text ""(.*)""")]
		[When(@"The content of ""(.*)"" contains text ""(.*)""")]
		[Then(@"The content of ""(.*)"" contains text ""(.*)""")]
		public void TheConentOfIsEqualTo(string elementKey, string expectedContent)
		{
			UiFeatureTestsHelper.IsElementContentEqual(elementKey, expectedContent);
		}

		[Given(@"The content of ""(.*)"" \(with the ""(.*)"" of ""(.*)""\) contains text ""(.*)""")]
		[When(@"The content of ""(.*)"" \(with the ""(.*)"" of ""(.*)""\) contains text ""(.*)""")]
		[Then(@"The content of ""(.*)"" \(with the ""(.*)"" of ""(.*)""\) contains text ""(.*)""")]
		[Given(@"Content of ""(.*)"" \(with the ""(.*)"" of ""(.*)""\) contains text ""(.*)""")]
		[When(@"Content of ""(.*)"" \(with the ""(.*)"" of ""(.*)""\) contains text ""(.*)""")]
		[Then(@"Content of ""(.*)"" \(with the ""(.*)"" of ""(.*)""\) contains text ""(.*)""")]
		public void TheConentOfWithTheOfIsEqualTo(string elementKey, string selectionMethod, string selection, string expectedContent)
		{
			UiFeatureTestsHelper.IsElementContentEqual(elementKey, selectionMethod, selection, expectedContent);
		}

		#endregion

		#region The content of page/element contains text with pattern <text pattern>

		[Given(@"Page contains the text pattern ""(.*)""")]
		[When(@"Page contains the text pattern ""(.*)""")]
		[Then(@"Page contains the text pattern ""(.*)""")]
		[Given(@"The page contains text pattern ""(.*)""")]
		[When(@"The page contains text pattern ""(.*)""")]
		[Then(@"The page contains text pattern ""(.*)""")]
		[Given(@"The content of the page contains text pattern ""(.*)""")]
		[When(@"The content of the page contains text pattern ""(.*)""")]
		[Then(@"The content of the page contains text pattern ""(.*)""")]
		public void ThePageContainsTextPattern(string textPattern)
		{
			UiFeatureTestsHelper.IsPageContainsTextPattern(textPattern);
		}

		[Given(@"""(.*)"" contains the text pattern ""(.*)""")]
		[When(@"""(.*)"" contains the text pattern ""(.*)""")]
		[Then(@"""(.*)"" contains the text pattern ""(.*)""")]
		[Given(@"The ""(.*)"" contains text pattern ""(.*)""")]
		[When(@"The ""(.*)"" contains text pattern ""(.*)""")]
		[Then(@"The ""(.*)"" contains text pattern ""(.*)""")]
		[Given(@"The content of the ""(.*)"" contains text pattern ""(.*)""")]
		[When(@"The content of the ""(.*)"" contains text pattern ""(.*)""")]
		[Then(@"The content of the ""(.*)"" contains text pattern ""(.*)""")]
		public void TheContainsTextPattern(string elementKey, string textPattern)
		{
			UiFeatureTestsHelper.IsElementContainsTextPattern(elementKey, textPattern);
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

		[Given(@"I click on ""(.*)"" and wait ""(.*)"" seconds")]
		[When(@"I click on ""(.*)"" and wait ""(.*)"" seconds")]
		[Then(@"I click on ""(.*)"" and wait ""(.*)"" seconds")]
		[Given(@"Click on ""(.*)"" and wait ""(.*)"" seconds")]
		[When(@"Click on ""(.*)"" and wait ""(.*)"" seconds")]
		[Then(@"Click on ""(.*)"" and wait ""(.*)"" seconds")]
		public void IClickOnAndWaitSeconds(string elementKey, string waitTime)
		{
			int timeout;
			if (int.TryParse(waitTime, out timeout))
			{
				UiFeatureTestsHelper.ClickOn(elementKey, timeout);
			}
			else
			{
				throw new Exception("\"" + waitTime + "\" is invalid.");
			}
		}

		[Given(@"I click on ""(.*)"" \(with the ""(.*)"" of ""(.*)""\) and wait ""(.*)"" seconds")]
		[When(@"I click on ""(.*)"" \(with the ""(.*)"" of ""(.*)""\) and wait ""(.*)"" seconds")]
		[Then(@"I click on ""(.*)"" \(with the ""(.*)"" of ""(.*)""\) and wait ""(.*)"" seconds")]
		[Given(@"Click on ""(.*)"" \(with the ""(.*)"" of ""(.*)""\) and wait ""(.*)"" seconds")]
		[When(@"Click on ""(.*)"" \(with the ""(.*)"" of ""(.*)""\) and wait ""(.*)"" seconds")]
		[Then(@"Click on ""(.*)"" \(with the ""(.*)"" of ""(.*)""\) and wait ""(.*)"" seconds")]
		public void IClickOnWithTheOfAndWaitSeconds(string elementKey, string selectionMethod, string selection, string waitTime)
		{
			int timeout;
			if (int.TryParse(waitTime, out timeout))
			{
				UiFeatureTestsHelper.ClickOn(elementKey, timeout, selectionMethod, selection);
			}
			else
			{
				throw new Exception("\"" + waitTime + "\" is invalid.");
			}
		}

		#endregion

		#region Drag <element1> and drop on to <element2>

		[Given(@"I drag ""(.*)"" and drop on to ""(.*)""")]
		[When(@"I drag ""(.*)"" and drop on to ""(.*)""")]
		[Then(@"I drag ""(.*)"" and drop on to ""(.*)""")]
		[Given(@"drag ""(.*)"" and drop on to ""(.*)""")]
		[When(@"drag ""(.*)"" and drop on to ""(.*)""")]
		[Then(@"drag ""(.*)"" and drop on to ""(.*)""")]
		public void DragAndDropOnTo(string elementToDrag, string elementToDrop)
		{
			UiFeatureTestsHelper.DragAndDrop(elementToDrag, elementToDrop);
		}

		[Given(@"I drag ""(.*)"" \(with the ""(.*)"" of ""(.*)""\) and drop on to ""(.*)"" \(with the ""(.*)"" of ""(.*)""\)")]
		[When(@"I drag ""(.*)"" \(with the ""(.*)"" of ""(.*)""\) and drop on to ""(.*)"" \(with the ""(.*)"" of ""(.*)""\)")]
		[Then(@"I drag ""(.*)"" \(with the ""(.*)"" of ""(.*)""\) and drop on to ""(.*)"" \(with the ""(.*)"" of ""(.*)""\)")]
		[Given(@"drag ""(.*)"" \(with the ""(.*)"" of ""(.*)""\) and drop on to ""(.*)"" \(with the ""(.*)"" of ""(.*)""\)")]
		[When(@"drag ""(.*)"" \(with the ""(.*)"" of ""(.*)""\) and drop on to ""(.*)"" \(with the ""(.*)"" of ""(.*)""\)")]
		[Then(@"drag ""(.*)"" \(with the ""(.*)"" of ""(.*)""\) and drop on to ""(.*)"" \(with the ""(.*)"" of ""(.*)""\)")]
		public void DragAndDropOnTo(string elementToDrag, string elementToDragSelectionMethod, string elementToDragSelection, string elementToDrop, string elementToDropSelectionMethod, string elementToDropSelection)
		{
			UiFeatureTestsHelper.DragAndDrop(elementToDrag, elementToDrop);
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

		[Given(@"I navigate to URL stored in ""(.*)""")]
		[When(@"I navigate to URL stored in ""(.*)""")]
		[Then(@"I navigate to URL stored in ""(.*)""")]
		[Given(@"Navigate to URL stored in ""(.*)""")]
		[When(@"Navigate to URL stored in ""(.*)""")]
		[Then(@"Navigate to URL stored in ""(.*)""")]
		public void INavigateToUrlStoredIn(string veriable)
		{
			if (dataShare.ContainsKey(veriable.ToUpper()))
				UiFeatureTestsHelper.GotoUrl(dataShare[veriable.ToUpper()]);
			else
			{
				throw new Exception("Veriable \"" + veriable + "\" not found.");
			}
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

		#region Read URL/<element> and store in data share

		[Given(@"I read the URL and store in ""(.*)"" variable")]
		[When(@"I read the URL and store in ""(.*)"" variable")]
		[Then(@"I read the URL and store in ""(.*)"" variable")]
		[Given(@"read the URL and store in ""(.*)"" variable")]
		[When(@"read the URL and store in ""(.*)"" variable")]
		[Then(@"read the URL and store in ""(.*)"" variable")]
		public void ReadTheUrlAndStoreIn(string veriable)
		{
			if (dataShare.ContainsKey(veriable.ToUpper()))
				dataShare[veriable.ToUpper()] = UiFeatureTestsHelper.ReadURL();
			else
				dataShare.Add(veriable.ToUpper(), UiFeatureTestsHelper.ReadURL());
		}

		[Given(@"I read the content of element ""(.*)"" and store in variable ""(.*)""")]
		[When(@"I read the content of element ""(.*)"" and store in variable ""(.*)""")]
		[Then(@"I read the content of element ""(.*)"" and store in variable ""(.*)""")]
		[Given(@"Read the content of element ""(.*)"" and store in variable ""(.*)""")]
		[When(@"Read the content of element ""(.*)"" and store in variable ""(.*)""")]
		[Then(@"Read the content of element ""(.*)"" and store in variable ""(.*)""")]
		public void ReadTheContentOfElementAndStoreInVeriable(string elementKey, string veriable)
		{
			var elementContent = UiFeatureTestsHelper.GetElementText(elementKey);
			if (dataShare.ContainsKey(veriable.ToUpper()))
				dataShare[veriable.ToUpper()] = elementContent;
			else
				dataShare.Add(veriable.ToUpper(), elementContent);
		}

		[Given(@"I read the ""(.*)""st element of the URL path and store in ""(.*)"" variable")]
		[When(@"I read the ""(.*)""st element of the URL path and store in ""(.*)"" variable")]
		[Then(@"I read the ""(.*)""st element of the URL path and store in ""(.*)"" variable")]
		[Given(@"Read the ""(.*)""st element of the URL path and store in ""(.*)"" variable")]
		[When(@"Read the ""(.*)""st element of the URL path and store in ""(.*)"" variable")]
		[Then(@"Read the ""(.*)""st element of the URL path and store in ""(.*)"" variable")]
		[Given(@"I read the ""(.*)""nd element of the URL path and store in ""(.*)"" variable")]
		[When(@"I read the ""(.*)""nd element of the URL path and store in ""(.*)"" variable")]
		[Then(@"I read the ""(.*)""nd element of the URL path and store in ""(.*)"" variable")]
		[Given(@"read the ""(.*)""nd element of the URL path and store in ""(.*)"" variable")]
		[When(@"read the ""(.*)""nd element of the URL path and store in ""(.*)"" variable")]
		[Then(@"read the ""(.*)""nd element of the URL path and store in ""(.*)"" variable")]
		[Given(@"I read the ""(.*)""rd element of the URL path and store in ""(.*)"" variable")]
		[When(@"I read the ""(.*)""rd element of the URL path and store in ""(.*)"" variable")]
		[Then(@"I read the ""(.*)""rd element of the URL path and store in ""(.*)"" variable")]
		[Given(@"Read the ""(.*)""rd element of the URL path and store in ""(.*)"" variable")]
		[When(@"Read the ""(.*)""rd element of the URL path and store in ""(.*)"" variable")]
		[Then(@"Read the ""(.*)""rd element of the URL path and store in ""(.*)"" variable")]
		[Given(@"I read the ""(.*)""th element of the URL path and store in ""(.*)"" variable")]
		[When(@"I read the ""(.*)""th element of the URL path and store in ""(.*)"" variable")]
		[Then(@"I read the ""(.*)""th element of the URL path and store in ""(.*)"" variable")]
		[Given(@"Read the ""(.*)""th element of the URL path and store in ""(.*)"" variable")]
		[When(@"Read the ""(.*)""th element of the URL path and store in ""(.*)"" variable")]
		[Then(@"Read the ""(.*)""th element of the URL path and store in ""(.*)"" variable")]
		public void ReadTheElementOfTheUrlPathAndStoreInVeriable(string index, string veriable)
		{
			var urlTosplit = UiFeatureTestsHelper.ReadURL().Replace("http://", "");
			var urlArray = urlTosplit.Split('/');
			var elementInex = -1;
			if (!int.TryParse(index, out elementInex)) throw new Exception("\"" + index + "\" is not a valid integer.");
			var element = urlArray[elementInex];
			if (dataShare.ContainsKey(veriable.ToUpper()))
				dataShare[veriable.ToUpper()] = element;
			else
				dataShare.Add(veriable.ToUpper(), element);
		}

		#endregion

		#region veriable manipulation

		[Given(@"I enter value of variable ""(.*)"" to the ""(.*)""")]
		[When(@"I enter value of variable ""(.*)"" to the ""(.*)""")]
		[Then(@"I enter value of variable ""(.*)"" to the ""(.*)""")]
		[Given(@"Enter value of variable ""(.*)"" to the ""(.*)""")]
		[When(@"Enter value of variable ""(.*)"" to the ""(.*)""")]
		[Then(@"Enter value of variable ""(.*)"" to the ""(.*)""")]
		public void IEnterContentOfVeriableToThe(string veriable, string elementKey)
		{
			if (!dataShare.ContainsKey(veriable.ToUpper())) throw new Exception("Veriable \"" + veriable + "\" is not defined.");
			UiFeatureTestsHelper.EnterTextTo(elementKey, dataShare[veriable.ToUpper()]);
		}

		[Given(@"I enter value of variable ""(.*)"" to the ""(.*)"" \(with the ""(.*)"" of ""(.*)""\)")]
		[When(@"I enter value of variable ""(.*)"" to the ""(.*)"" \(with the ""(.*)"" of ""(.*)""\)")]
		[Then(@"I enter value of variable ""(.*)"" to the ""(.*)"" \(with the ""(.*)"" of ""(.*)""\)")]
		[Given(@"Enter value of variable ""(.*)"" to the ""(.*)"" \(with the ""(.*)"" of ""(.*)""\)")]
		[When(@"Enter value of variable ""(.*)"" to the ""(.*)"" \(with the ""(.*)"" of ""(.*)""\)")]
		[Then(@"Enter value of variable ""(.*)"" to the ""(.*)"" \(with the ""(.*)"" of ""(.*)""\)")]
		public void IEnterContentOfVeriableToTheWithTheOf(string veriable, string elementKey, string selectionMethod, string selection)
		{
			if (!dataShare.ContainsKey(veriable.ToUpper())) throw new Exception("Veriable \"" + veriable + "\" is not defined.");
			UiFeatureTestsHelper.EnterTextTo(elementKey, dataShare[veriable.ToUpper()], selectionMethod, selection);
		}

		[Given(@"The value of variable ""(.*)"" is equal to ""(.*)""")]
		[When(@"The value of variable ""(.*)"" is equal to ""(.*)""")]
		[Then(@"The value of variable ""(.*)"" is equal to ""(.*)""")]
		[Given(@"Value of variable ""(.*)"" is equal to ""(.*)""")]
		[When(@"Value of variable ""(.*)"" is equal to ""(.*)""")]
		[Then(@"Value of variable ""(.*)"" is equal to ""(.*)""")]
		public void ValueOfVeriableIsEqualTo(string veriable, string value)
		{
			if (!dataShare.ContainsKey(veriable.ToUpper())) throw new Exception("Veriable \"" + veriable + "\" is not defined.");
			UiFeatureTestsHelper.AreValuesEqual(dataShare[veriable.ToUpper()], value);
		}

		#endregion

		#endregion
	}
}
