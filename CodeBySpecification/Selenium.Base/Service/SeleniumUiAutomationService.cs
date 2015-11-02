using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using CodeBySpecification.API.Domain;
using CodeBySpecification.API.Service.Api;
using ObjectRepository.Base.Service;
using DataRepository.Base.Service;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using Selenium.Base.Api;
using Selenium.Base.Browsers;
using TestFramework.Base.Service;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium;

namespace Selenium.Base.Service
{
	public class SeleniumUIAutomationService : IUiAutomationService
	{
		private readonly double timeOut = Double.Parse(ConfigurationManager.AppSettings["UI.Tests.Timeout"]);
		private readonly string SutUrl = ConfigurationManager.AppSettings["UI.Tests.SUT.Url"];
		private readonly ITestAssertService assert = new MSTestAssertService();
		private readonly IObjectRepoService objectRepoManager = new CSVObjectRepositoryService();
        private readonly IDataRepoService dataRepoManager = new CSVDataRepositoryService();
        private string objectRepoResource = null;

		public object GetBrowser { get; set; }

		public void AddNewElementToObjectRepo(string key, UiElement uiElement)
		{
			objectRepoManager.AddObject(key, uiElement);
		}

		public void AcceptTheConfirmation()
		{
			((IWebDriver)GetBrowser).SwitchTo().Alert().Accept();
		}

		public IWebElement GetElement(string key, string selectionMethod, string selection)
		{
			if (objectRepoManager.ObjectExists(key)) objectRepoManager.DeleteObject(key);
			var element = GetElementBy(selectionMethod, selection);
			if (element == null) return null;
			if (!objectRepoManager.ObjectExists(key))
				AddNewElementToObjectRepo(key, new UiElement { SelectionMethod = selectionMethod, Selection = selection });
			return element;
		}

		public void IsElementContentEqual(string elementKey, string expectedContent, string selectionMethod = null, string selection = null)
		{
			assert.IsTrue(WaitForElementContentToLoad(elementKey, expectedContent, selectionMethod, selection));
		}

		public void ClickOn(string elementKey, string selectionMethod = null, string selection = null)
		{
			var element = selectionMethod != null ? GetElement(elementKey, selectionMethod, selection) : GetElementByKey(elementKey);
			if (element == null) assert.Fail("\"" + elementKey + "\" is not avilable to Click");
			element.Click();
		}

		public void ClickOn(string elementKey, int timeout, string selectionMethod = null, string selection = null)
		{
			ClickOn(elementKey, selectionMethod, selection);
			Thread.Sleep(timeout * 1000);
		}

		public virtual void EnterTextTo(string elementKey, string text, string selectionMethod = null, string selection = null)
		{
			var element = selectionMethod != null ? GetElement(elementKey, selectionMethod, selection) : GetElementByKey(elementKey);
			if (element == null) assert.Fail("\"" + elementKey + "\" is not avilable to input the value \"" + text + "\"");
			try
			{
				element.Clear();
			}
			catch (Exception)
			{
				//ignore element clear related exceptions
			}
			element.SendKeys(text);
		}

        public virtual void SelectValueOf(string elementKey, string text, string selectionMethod = null, string selection = null)
        {
            var element = selectionMethod != null ? GetElement(elementKey, selectionMethod, selection) : GetElementByKey(elementKey);
            if (element == null) assert.Fail("\"" + elementKey + "\" is not avilable to input the value \"" + text + "\"");
            new SelectElement(element).SelectByText(text);
        }

        public void GotoUrl(string url)
		{
			var driver = ((IWebDriver)GetBrowser);
			driver.Navigate().GoToUrl(new Uri(url));
			var tabList = driver.WindowHandles.ToArray();
			driver.SwitchTo().Window(tabList[0]);
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
			return new WebDriverWait(((IWebDriver)GetBrowser), TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists((selction)));
		}

		private bool WaitForElementContentToLoad(string key, string content, string selectionMethod = null, string selection = null)
		{
			key = key.ToUpper();
			if (!objectRepoManager.ObjectExists(key) && selectionMethod == null && selection == null) return false;

			if (!objectRepoManager.ObjectExists(key) && selectionMethod != null && selection != null) objectRepoManager.AddObject(key, new UiElement { Selection = selection, SelectionMethod = selectionMethod });

			switch (objectRepoManager.GetObject(key).SelectionMethod.ToUpper())
			{
				case "ID":
					return new WebDriverWait(((IWebDriver)GetBrowser), TimeSpan.FromSeconds(timeOut)).Until(d => d.FindElement(By.Id(objectRepoManager.GetObject(key).Selection)).Text.Trim().Contains(content));

				case "XPATH":
					return new WebDriverWait(((IWebDriver)GetBrowser), TimeSpan.FromSeconds(timeOut)).Until(d => d.FindElement(By.XPath(objectRepoManager.GetObject(key).Selection)).Text.Trim().Contains(content));
			}
			return false;
		}

		public void IsElementVisible(string elementKey, string selectionMethod = null, string selection = null)
		{
			assert.IsNotNull(selectionMethod != null ? GetElement(elementKey, selectionMethod, selection) : GetElementByKey(elementKey));
		}

		public void IsElementNotVisible(string elementKey, string selectionMethod = null, string selection = null)
		{
			assert.IsNull(selectionMethod != null ? GetElement(elementKey, selectionMethod, selection) : GetElementByKey(elementKey));
		}

		public string GetElementText(string elementKey, string selectionMethod = null, string selection = null)
		{
			var element = selectionMethod == null ? GetElementByKey(elementKey) : GetElement(elementKey, selectionMethod, selection);
			if (element == null) assert.Fail("\"" + elementKey + "\" is not avilable to read the content.");
			return element.Text;
		}

		string IUiAutomationService.SutUrl
		{
			get { return SutUrl; }
		}

		public void InitilizeTests(string browserType, string objectRepoResource)
		{
			var browser = (IWebDriver)GetBrowser;
			this.objectRepoResource = objectRepoResource;
			var candidateBrowsers = new List<IBrowser>
			{
				new FireFoxBrowser(browserType),
				new IEBrowser(browserType),
				new ChromeBrowser(browserType),
                new AndroidBrowser(browserType),
                new iOSBrowser(browserType),
            };

			if (browser == null)
			{
				foreach (var candiateBrowser in candidateBrowsers)
				{
					browser = candiateBrowser.Create();
					if (browser != null) break;
				}

				if (browser == null) throw new Exception("Browser driver initilization error. Please ensure you have set the \"UI.Tests.Target.Browser\" setting correctly in the App.Config file.");
				browser.Manage().Window.Maximize();
                if(browserType != "Android" && browserType != "iOS") {
                    browser.Manage().Cookies.DeleteAllCookies();
                }
				GetBrowser = browser;
			}
			if (objectRepoManager.ObjectCount() == 0)
				objectRepoManager.Populate(objectRepoResource);
		}

		private void SeleniumDragAndDrop(string dragElementKey, string dropElementKey, IWebElement locatorFrom,
			IWebElement locatorTo)
		{
			if (locatorFrom == null) assert.Fail("\"" + dragElementKey + "\" is not avilable to drag.");
			if (locatorTo == null) assert.Fail("\"" + dropElementKey + "\" is not avilable to drop \"" + dropElementKey + "\".");
			var driver = (IWebDriver)GetBrowser;
			var action = new Actions(driver);
			action.DragAndDrop(locatorFrom, locatorTo).Perform();
        }

		public void DragAndDrop(string dragElementKey, string dropElementKey, string dragElementSelectionMethod = null, string dragElementSelection = null, string dropElementKeySelectionMethod = null, string dropElementKeySelection = null)
		{
			var locatorFrom = dragElementSelectionMethod != null ? GetElement(dragElementKey, dragElementSelectionMethod, dragElementSelection) : GetElementByKey(dragElementKey);
			var locatorTo = dropElementKeySelectionMethod != null ? GetElement(dropElementKey, dropElementKeySelectionMethod, dropElementKeySelection) : GetElementByKey(dropElementKey);
			SeleniumDragAndDrop(dragElementKey, dropElementKey, locatorFrom, locatorTo);
		}

		public string ReadUrl()
		{
			var driver = (IWebDriver)GetBrowser;
			return driver.Url;
		}

		public void AreValuesEqual(string value1, string value2)
		{
			assert.IsEqual(value1, value2);
		}

		public void IsPageContainsTextPattern(string textPattern)
		{
			var pageSource = ((IWebDriver)GetBrowser).PageSource;
			var match = Regex.Match(pageSource, @textPattern, RegexOptions.IgnoreCase);
			assert.IsTrue(match.Success);
		}

		public void IsElementContainsTextPattern(string elementKey, string textPattern, string selectionMethod = null, string selection = null)
		{
			var element = (selectionMethod == null) ? GetElementByKey(elementKey) : GetElement(elementKey, selectionMethod, selection);
			if (element == null) throw new Exception("\"" + elementKey + "\" is not found.");
			var match = Regex.Match(element.Text, @textPattern, RegexOptions.IgnoreCase);
			assert.IsTrue(match.Success);
		}

		public void SwitchToWindow(int tab)
		{
			var driver = (IWebDriver)GetBrowser;
			var tabs = driver.WindowHandles.ToArray();
			driver.SwitchTo().Window(tabs[tab]);
		}

		public void CloseWindow(int tab)
		{
			var driver = (IWebDriver)GetBrowser;
			var tabs = driver.WindowHandles.ToArray();
			if (tab > 0)
			{
				driver.SwitchTo().Window(tabs[tab]);
				driver.Close();
				driver.SwitchTo().Window(tabs[0]);
			}
			else
			{
				driver.Close();
			}
		}

		public void TableHasRowCountOf(string elementKey, int numberOfRows)
		{
			var element = GetElementByKey(elementKey);
			var rowCount = element.FindElements(By.XPath(".//tbody/tr")).Count;
			assert.IsEqual(numberOfRows, rowCount);
		}

		public void TableHasColumnCountOf(string elementKey, int columnCount)
		{
			var element = GetElementByKey(elementKey);
			var count = element.FindElements(By.XPath(".//tbody/tr[1]/td")).Count;
			assert.IsEqual(columnCount, count);
		}

		public void ValueOfTableRowColEqualTo(string elementKey, int row, int col, string value)
		{
			var element = GetElementByKey(elementKey);
			var valueElement = element.FindElement(By.XPath(".//tbody/tr[" + row + "]/td[" + col + "]")).Text.Trim();
			assert.IsEqual(value, valueElement);
		}

        public void switchToFrame(string selectionMethod, string selection)
        {
            var driver = ((IWebDriver)GetBrowser);
            driver.SwitchTo().Frame(GetElementBy(selectionMethod, selection));
        }

        public void switchToDefaultContent()
        {
            var driver = ((IWebDriver)GetBrowser);
            driver.SwitchTo().DefaultContent();
        }

        public void GetTheValuesFrom(string objectRepoResource)
        {
            dataRepoManager.Populate(objectRepoResource);
        }
    }
}