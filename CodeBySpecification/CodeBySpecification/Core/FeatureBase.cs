using System;
using System.Collections.Generic;
using System.Configuration;
using CodeBySpecification.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using TechTalk.SpecFlow;

namespace CodeBySpecification.Core
{
	[Binding]
	public class FeatureBase
	{ //TODO: At present this class has too many responsibility, but we will refactor it once we have the basic system avilable.
		#region Methods and fields used by other Features

		protected readonly double TimeOut = Double.Parse(ConfigurationManager.AppSettings["UI.Tests.Timeout"]);
		protected readonly string SutUrl = ConfigurationManager.AppSettings["UI.Tests.SUT.Url"];

		protected IWebDriver GetBrowser()
		{
			return SetupHook.Browser;
		}

		protected IDictionary<string, ElementInfo> GetObjectRepo()
		{
			return SetupHook.ObjectRepo;
		}

		protected void AddNewElementToObjectRepo(string key, ElementInfo element)
		{
			SetupHook.ObjectRepo.Add(key.ToUpper(), element);
		}

		#endregion

		#region Core Step Definition Vocabulary

		#region Read the content of <element>

		[Given(@"Read the content of ""(.*)""")]
		[When(@"Read the content of ""(.*)""")]
		[Then(@"Read the content of ""(.*)""")]
		public void ReadTheContentOf(string elementKey)
		{
			var element = GetElementByKey(elementKey);
			if (element == null) Assert.Fail("\"" + elementKey + "\" is not avilable to read the content.");
			FeatureContext.Current[elementKey] = element.Text;
		}

		[Given(@"Read the content of ""(.*)"" \(with the ""(.*)"" of ""(.*)""\)")]
		[When(@"Read the content of ""(.*)"" \(with the ""(.*)"" of ""(.*)""\)")]
		[Then(@"Read the content of ""(.*)"" \(with the ""(.*)"" of ""(.*)""\)")]
		public void ReadTheContentOfWithTheOf(string elementKey, string selectionType, string selection)
		{
			var element = GetElement(elementKey, selectionType, selection);
			if (element == null) Assert.Fail("\"" + elementKey + "\" is not avilable to read the content.");
			FeatureContext.Current[elementKey] = element.Text;
		}

		#endregion

		#region The content of <element> is equal to <expected value>

		[Given(@"The content of ""(.*)"" is equal to ""(.*)""")]
		[When(@"The content of ""(.*)"" is equal to ""(.*)""")]
		[Then(@"The content of ""(.*)"" is equal to ""(.*)""")]
		public void TheConentOfIsEqualTo(string elementKey, string expectedContent)
		{
			IsElementContentEqual(elementKey, expectedContent);
		}

		[Given(@"The content of ""(.*)"" \(with the ""(.*)"" of ""(.*)""\) is equal to ""(.*)""")]
		[When(@"The content of ""(.*)"" \(with the ""(.*)"" of ""(.*)""\) is equal to ""(.*)""")]
		[Then(@"The content of ""(.*)"" \(with the ""(.*)"" of ""(.*)""\) is equal to ""(.*)""")]
		public void TheConentOfWithTheOfIsEqualTo(string elementKey, string selectionMethod, string selection, string expectedContent)
		{
			IsElementContentEqual(elementKey, selectionMethod, selection, expectedContent);
		}

		#endregion

		#region Click on <element>

		[Given(@"I click on ""(.*)""")]
		[When(@"I click on ""(.*)""")]
		[Then(@"I click on ""(.*)""")]
		public void IClickOn(string elementKey)
		{
			ClickOn(elementKey);
		}

		[Given(@"I click on ""(.*)"" \(with the ""(.*)"" of ""(.*)""\)")]
		[When(@"I click on ""(.*)"" \(with the ""(.*)"" of ""(.*)""\)")]
		[Then(@"I click on ""(.*)"" \(with the ""(.*)"" of ""(.*)""\)")]
		public void IClickOnWithTheOf(string elementKey, string selectionMethod, string selection)
		{
			ClickOn(elementKey, selectionMethod, selection);
		}

		#endregion

		#region Enter <value> to <element>

		[Given(@"I enter ""(.*)"" to the ""(.*)""")]
		[When(@"I enter ""(.*)"" to the ""(.*)""")]
		[Then(@"I enter ""(.*)"" to the ""(.*)""")]
		public void IEnterToThe(string value, string elementKey)
		{
			EnterTextTo(elementKey, value);
		}

		[Given(@"I enter ""(.*)"" to the ""(.*)"" \(with the ""(.*)"" of ""(.*)""\)")]
		[When(@"I enter ""(.*)"" to the ""(.*)"" \(with the ""(.*)"" of ""(.*)""\)")]
		[Then(@"I enter ""(.*)"" to the ""(.*)"" \(with the ""(.*)"" of ""(.*)""\)")]
		public void IEnterToTheWithTheOf(string value, string elementKey, string selectionMethod, string selection)
		{
			EnterTextTo(elementKey, value, selectionMethod, selection);
		}

		#endregion

		#region Navigate to SUT

		[Given(@"I navigate to SUT")]
		[When(@"I navigate to SUT")]
		[Then(@"I navigate to SUT")]
		public void INavigateToSut()
		{
			GotoUrl(SutUrl);
		}

		#endregion

		#region Navigate to <URL>

		[Given(@"I navigate to ""(.*)""")]
		[When(@"I navigate to ""(.*)""")]
		[Then(@"I navigate to ""(.*)""")]
		public void INavigateTo(string url)
		{
			GotoUrl(url);
		}

		#endregion

		#region The <Element> is Visible

		[Given(@"The ""(.*)"" is visible")]
		[Then(@"The ""(.*)"" is visible")]
		[When(@"The ""(.*)"" is visible")]
		[Given(@"I wait for the ""(.*)"" to appear")]
		[When(@"I wait for the ""(.*)"" to appear")]
		[Then(@"I wait for the ""(.*)"" to appear")]
		public void TheElementIsVisible(string elementKey)
		{
			Assert.IsNotNull(GetElementByKey(elementKey));
		}

		[Given(@"The ""(.*)"" \(with the ""(.*)"" of ""(.*)""\) is visible")]
		[Then(@"The ""(.*)"" \(with the ""(.*)"" of ""(.*)""\) is visible")]
		[When(@"The ""(.*)"" \(with the ""(.*)"" of ""(.*)""\) is visible")]
		[Given(@"I wait for the ""(.*)"" \(with the ""(.*)"" of ""(.*)""\) to appear")]
		[When(@"I wait for the ""(.*)"" \(with the ""(.*)"" of ""(.*)""\) to appear")]
		[Then(@"I wait for the ""(.*)"" \(with the ""(.*)"" of ""(.*)""\) to appear")]
		public void TheElementWithTheOfIsVisible(string elementKey, string selectionMethod, string selection)
		{
			Assert.IsNotNull(GetElement(elementKey, selectionMethod, selection));
		}

		#endregion

		#region Accept the confirmation

		[Given(@"I accept the confirmation")]
		[Then(@"I accept the confirmation")]
		[When(@"I accept the confirmation")]
		public void IAcceptTheConfirmation()
		{
			AcceptTheConfirmation();
		}

		#endregion

		#endregion

		#region Core methods used in the system. Consider refactoring to another class

		private void AcceptTheConfirmation()
		{
			GetBrowser().SwitchTo().Alert().Accept();
		}

		private IWebElement GetElement(string key, string selectionMethod, string selection)
		{
			var elementList = GetObjectRepo();
			if (elementList.ContainsKey(key)) return GetElementBy(elementList[key].SelectionMethod, elementList[key].Selection);
			var element = GetElementBy(selectionMethod, selection);
			if (element == null) return null;
			AddNewElementToObjectRepo(key, new ElementInfo { SelectionMethod = selectionMethod, Selection = selection });
			return element;
		}

		private void IsElementContentEqual(string elementKey, string expectedContent)
		{
			var element = GetElementByKey(elementKey);
			if (element == null) Assert.Fail("\"" + elementKey + "\" is not avilable.");
			Assert.AreEqual(expectedContent, element.Text.Trim().Replace("\r\n", ""));
		}

		private void IsElementContentEqual(string elementKey, string selectionMethod, string selection, string expectedContent)
		{
			var element = GetElement(elementKey, selectionMethod, selection);
			if (element == null) Assert.Fail("\"" + element.TagName + "\" is not avilable.");
			Assert.AreEqual(expectedContent, element.Text.Trim().Replace("\r\n", ""));
		}

		private void ClickOn(string elementKey)
		{
			var element = GetElementByKey(elementKey);
			if (element == null) Assert.Fail("\"" + elementKey + "\" is not avilable to Click");
			element.Click();
		}

		private void ClickOn(string elementKey, string selectionMethod, string selection)
		{
			var element = GetElement(elementKey, selectionMethod, selection);
			if (element == null) Assert.Fail("\"" + elementKey + "\" is not avilable to Click");
			element.Click();
		}

		private void EnterTextTo(string elementKey, string text)
		{
			var element = GetElementByKey(elementKey);
			if (element == null) Assert.Fail("\"" + elementKey + "\" is not avilable to input the value \"" + text + "\"");
			element.SendKeys(text);
		}

		private void EnterTextTo(string elementKey, string text, string selectionMethod, string selection)
		{
			var element = GetElement(elementKey, selectionMethod, selection);
			if (element == null) Assert.Fail("\"" + elementKey + "\" is not avilable to input the value \"" + text + "\"");
			element.SendKeys(text);
		}

		private void GotoUrl(string url)
		{
			GetBrowser().Navigate().GoToUrl(new Uri(url));
		}

		private IWebElement GetElementByKey(string key)
		{
			var elementList = GetObjectRepo();
			return elementList.ContainsKey(key.ToUpper()) ? GetElementBy(elementList[key.ToUpper()].SelectionMethod, elementList[key.ToUpper()].Selection) : null;
		}

		private IWebElement GetElementBy(string selecitonMethod, string selection)
		{
			switch (selecitonMethod.ToUpper())
			{
				case "ID":
					return WaitAndCreateElement(By.Id(selection));

				case "XPATH":
					return WaitAndCreateElement(By.XPath(selection));
			}
			return null;
		}

		private IWebElement WaitAndCreateElement(By selction)
		{
			return new WebDriverWait(GetBrowser(), TimeSpan.FromSeconds(TimeOut)).Until(ExpectedConditions.ElementExists((selction)));
		}

		private bool WaitForElementContentToLoad(string key, string content, string selectionMethod = null, string selection = null)
		{
			var elementList = GetObjectRepo();
			key = key.ToUpper();
			if (!elementList.ContainsKey(key) && selectionMethod == null && selection == null) return false;
			if (!elementList.ContainsKey(key) && selectionMethod != null && selection != null) elementList.Add(key, new ElementInfo { Selection = selection, SelectionMethod = selectionMethod });

			switch (elementList[key].SelectionMethod.ToUpper())
			{
				case "ID":
					return new WebDriverWait(GetBrowser(), TimeSpan.FromSeconds(TimeOut)).Until(d => d.FindElement(By.Id(elementList[key].Selection)).Text.Contains(content));

				case "XPATH":
					return new WebDriverWait(GetBrowser(), TimeSpan.FromSeconds(TimeOut)).Until(d => d.FindElement(By.XPath(elementList[key].Selection)).Text.Contains(content));
			}
			return false;
		}

		#endregion
	}
}
