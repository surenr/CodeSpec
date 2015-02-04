using System;
using System.Collections.Generic;
using System.Configuration;
using CodeBySpecification.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace CodeBySpecification.Core
{
	public class UiFeatureTestsHelper
	{
		private readonly double timeOut = Double.Parse(ConfigurationManager.AppSettings["UI.Tests.Timeout"]);
		public readonly string SutUrl = ConfigurationManager.AppSettings["UI.Tests.SUT.Url"];

		private IWebDriver GetBrowser()
		{
			return SetupHook.Browser;
		}

		private IDictionary<string, ElementInfo> GetObjectRepo()
		{
			return SetupHook.ObjectRepo;
		}

		public void AddNewElementToObjectRepo(string key, ElementInfo element)
		{
			SetupHook.ObjectRepo.Add(key.ToUpper(), element);
		}

		public void AcceptTheConfirmation()
		{
			GetBrowser().SwitchTo().Alert().Accept();
		}

		public IWebElement GetElement(string key, string selectionMethod, string selection)
		{
			var elementList = GetObjectRepo();
			if (elementList.ContainsKey(key)) return GetElementBy(elementList[key].SelectionMethod, elementList[key].Selection);
			var element = GetElementBy(selectionMethod, selection);
			if (element == null) return null;
			AddNewElementToObjectRepo(key, new ElementInfo { SelectionMethod = selectionMethod, Selection = selection });
			return element;
		}

		public void IsElementContentEqual(string elementKey, string expectedContent)
		{
			var element = GetElementByKey(elementKey);
			if (element == null) Assert.Fail("\"" + elementKey + "\" is not avilable.");
			Assert.AreEqual<string>(expectedContent, element.Text.Trim().Replace("\r\n", ""));
		}

		public void IsElementContentEqual(string elementKey, string selectionMethod, string selection, string expectedContent)
		{
			var element = GetElement(elementKey, selectionMethod, selection);
			if (element == null) Assert.Fail("\"" + element.TagName + "\" is not avilable.");
			Assert.AreEqual(expectedContent, element.Text.Trim().Replace("\r\n", ""));
		}

		public void ClickOn(string elementKey)
		{
			var element = GetElementByKey(elementKey);
			if (element == null) Assert.Fail("\"" + elementKey + "\" is not avilable to Click");
			element.Click();
		}

		public void ClickOn(string elementKey, string selectionMethod, string selection)
		{
			var element = GetElement(elementKey, selectionMethod, selection);
			if (element == null) Assert.Fail("\"" + elementKey + "\" is not avilable to Click");
			element.Click();
		}

		public void EnterTextTo(string elementKey, string text)
		{
			var element = GetElementByKey(elementKey);
			if (element == null) Assert.Fail("\"" + elementKey + "\" is not avilable to input the value \"" + text + "\"");
			element.SendKeys(text);
		}

		public void EnterTextTo(string elementKey, string text, string selectionMethod, string selection)
		{
			var element = GetElement(elementKey, selectionMethod, selection);
			if (element == null) Assert.Fail("\"" + elementKey + "\" is not avilable to input the value \"" + text + "\"");
			element.SendKeys(text);
		}

		public void GotoUrl(string url)
		{
			GetBrowser().Navigate().GoToUrl(new Uri(url));
		}

		public IWebElement GetElementByKey(string key)
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
			return new WebDriverWait(GetBrowser(), TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists((selction)));
		}

		public bool WaitForElementContentToLoad(string key, string content, string selectionMethod = null, string selection = null)
		{
			var elementList = GetObjectRepo();
			key = key.ToUpper();
			if (!elementList.ContainsKey(key) && selectionMethod == null && selection == null) return false;
			if (!elementList.ContainsKey(key) && selectionMethod != null && selection != null) elementList.Add(key, new ElementInfo { Selection = selection, SelectionMethod = selectionMethod });

			switch (elementList[key].SelectionMethod.ToUpper())
			{
				case "ID":
					return new WebDriverWait(GetBrowser(), TimeSpan.FromSeconds(timeOut)).Until(d => d.FindElement(By.Id(elementList[key].Selection)).Text.Contains(content));

				case "XPATH":
					return new WebDriverWait(GetBrowser(), TimeSpan.FromSeconds(timeOut)).Until(d => d.FindElement(By.XPath(elementList[key].Selection)).Text.Contains(content));
			}
			return false;
		}
	}
}