using System;
using System.Configuration;
using CodeBySpecification.API.Domain;
using CodeBySpecification.API.Service.Api;
using ObjectRepository.Base.Service;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using TestFramework.Base.Service;

namespace Selenium.Base.Service
{
	public class SeleniumUIAutomationService : IUIAutomationService
	{
		private readonly double timeOut = Double.Parse(ConfigurationManager.AppSettings["UI.Tests.Timeout"]);
		private readonly string SutUrl = ConfigurationManager.AppSettings["UI.Tests.SUT.Url"];
		private readonly ITestAssertService assert = new MSTestAssertService();
		private readonly IObjectRepoService objectRepoManager = new CSVObjectRepositoryService();

		public object GetBrowser { get; set; }

		public void AddNewElementToObjectRepo(string key, UiElement uiElement)
		{
			objectRepoManager.AddObject(key, uiElement);
		}

		public void AcceptTheConfirmation()
		{
			((IWebDriver) GetBrowser).SwitchTo().Alert().Accept();
		}

		public IWebElement GetElement(string key, string selectionMethod, string selection)
		{
			if (objectRepoManager.ObjectExists(key)) return GetElementBy(objectRepoManager.GetObject(key).SelectionMethod, objectRepoManager.GetObject(key).Selection);
			var element = GetElementBy(selectionMethod, selection);
			if (element == null) return null;
			AddNewElementToObjectRepo(key, new UiElement { SelectionMethod = selectionMethod, Selection = selection });
			return element;
		}

		public void IsElementContentEqual(string elementKey, string expectedContent)
		{
			var element = GetElementByKey(elementKey);
			if (element == null) assert.Fail("\"" + elementKey + "\" is not avilable.");
			assert.IsEqual(expectedContent, element.Text.Trim().Replace("\r\n", ""));
		}

		public void IsElementContentEqual(string elementKey, string selectionMethod, string selection, string expectedContent)
		{
			var element = GetElement(elementKey, selectionMethod, selection);
			if (element == null) assert.Fail("\"" + element.TagName + "\" is not avilable.");
			assert.IsEqual(expectedContent, element.Text.Trim().Replace("\r\n", ""));
		}

		public void ClickOn(string elementKey)
		{
			var element = GetElementByKey(elementKey);
			if (element == null) assert.Fail("\"" + elementKey + "\" is not avilable to Click");
			element.Click();
		}

		public void ClickOn(string elementKey, string selectionMethod, string selection)
		{
			var element = GetElement(elementKey, selectionMethod, selection);
			if (element == null) assert.Fail("\"" + elementKey + "\" is not avilable to Click");
			element.Click();
		}

		public void EnterTextTo(string elementKey, string text)
		{
			var element = GetElementByKey(elementKey);
			if (element == null) assert.Fail("\"" + elementKey + "\" is not avilable to input the value \"" + text + "\"");
			element.SendKeys(text);
		}

		public void EnterTextTo(string elementKey, string text, string selectionMethod, string selection)
		{
			var element = GetElement(elementKey, selectionMethod, selection);
			if (element == null) assert.Fail("\"" + elementKey + "\" is not avilable to input the value \"" + text + "\"");
			element.SendKeys(text);
		}

		public void GotoUrl(string url)
		{
			((IWebDriver) GetBrowser).Navigate().GoToUrl(new Uri(url));
		}

		private IWebElement GetElementByKey(string key)
		{
			return objectRepoManager.ObjectExists(key) ? GetElementBy(objectRepoManager.GetObject(key).SelectionMethod, objectRepoManager.GetObject(key).Selection) : null;
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
			return new WebDriverWait(((IWebDriver) GetBrowser), TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists((selction)));
		}

		private bool WaitForElementContentToLoad(string key, string content, string selectionMethod = null, string selection = null)
		{
			key = key.ToUpper();
			if (!objectRepoManager.ObjectExists(key) && selectionMethod == null && selection == null) return false;
			if (!objectRepoManager.ObjectExists(key) && selectionMethod != null && selection != null) objectRepoManager.AddObject(key, new UiElement { Selection = selection, SelectionMethod = selectionMethod });

			switch (objectRepoManager.GetObject(key).SelectionMethod.ToUpper())
			{
				case "ID":
					return new WebDriverWait(((IWebDriver) GetBrowser), TimeSpan.FromSeconds(timeOut)).Until(d => d.FindElement(By.Id(objectRepoManager.GetObject(key).Selection)).Text.Contains(content));

				case "XPATH":
					return new WebDriverWait(((IWebDriver) GetBrowser), TimeSpan.FromSeconds(timeOut)).Until(d => d.FindElement(By.XPath(objectRepoManager.GetObject(key).Selection)).Text.Contains(content));
			}
			return false;
		}

		public void IsElementVisible(string elementKey)
		{
			assert.IsNotNull(GetElementByKey(elementKey));
		}

		public void IsElementVisible(string elementKey, string selectionMethod, string selection)
		{
			assert.IsNotNull(GetElement(elementKey, selectionMethod, selection));
		}

		public string GetElementText(string elementKey)
		{
			var element = GetElementByKey(elementKey);
			if (element == null) assert.Fail("\"" + elementKey + "\" is not avilable to read the content.");
			return element.Text;
		}

		public string GetElementText(string elementKey, string selectionMethod, string selection)
		{
			var element = GetElement(elementKey, selectionMethod, selection);
			if (element == null) assert.Fail("\"" + elementKey + "\" is not avilable to read the content.");
			return element.Text;
		}

		string IUIAutomationService.SutUrl
		{
			get { return SutUrl; }
		}

		public void InitilizeTests(string browserType, string objectRepoResource)
		{
			var browser = (IWebDriver) GetBrowser;

			if (browser == null)
			{
				switch (browserType)
				{
					case "FireFox":

						var profile = new FirefoxProfile();
						profile.EnableNativeEvents = true;
						profile.AcceptUntrustedCertificates = true;
						browser = new FirefoxDriver(profile);
						break;
				}
				if (browser == null) throw new Exception("Browser driver initilization error. Please ensure you have set the \"UI.Tests.Target.Browser\" setting correctly in the App.Config file.");
				browser.Manage().Window.Maximize();
				browser.Manage().Cookies.DeleteAllCookies();
				GetBrowser = browser;
			}
			if (objectRepoManager.ObjectCount() == 0)
				objectRepoManager.Populate(objectRepoResource);
		}

		public void DragAndDrop(string dragElementKey, string dropElementKey)
		{
			var locatorFrom = GetElementByKey(dragElementKey);
			var locatorTo = GetElementByKey(dropElementKey);
			SeleniumDragAndDrop(dragElementKey, dropElementKey, locatorFrom, locatorTo);
		}

		private void SeleniumDragAndDrop(string dragElementKey, string dropElementKey, IWebElement locatorFrom,
			IWebElement locatorTo)
		{
			if (locatorFrom == null) assert.Fail("\"" + dragElementKey + "\" is not avilable to drag.");
			if (locatorTo == null) assert.Fail("\"" + dropElementKey + "\" is not avilable to drop \"" + dropElementKey + "\".");
			var driver = (IWebDriver) GetBrowser;
			var action = new Actions(driver);
			action.DragAndDrop(locatorFrom, locatorTo);
		}

		public void DragAndDrop(string dragElementKey, string dragElementSelectionMethod, string dragElementSelection, string dropElementKey, string dropElementKeySelectionMethod, string dropElementKeySelection)
		{
			var locatorFrom = GetElement(dragElementKey, dragElementSelectionMethod, dragElementSelection);
			var locatorTo = GetElement(dropElementKey, dropElementKeySelectionMethod, dropElementKeySelection);
			SeleniumDragAndDrop(dragElementKey, dropElementKey, locatorFrom, locatorTo);
		}
	}
}