using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using CodeBySpecification.API.Domain;
using CodeBySpecification.API.Service.Api;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace Selenium.Base.Service
{
	public class SeleniumUiFeatureTestHelper : IFeatureTestHelper
	{
		private readonly double timeOut = Double.Parse(ConfigurationManager.AppSettings["UI.Tests.Timeout"]);
		private readonly string SutUrl = ConfigurationManager.AppSettings["UI.Tests.SUT.Url"];

		public SeleniumUiFeatureTestHelper()
		{
			ObjectRepo = new Dictionary<string, UiElement>();
		}

		public void AddNewElementToObjectRepo(string key, UiElement uiElement)
		{
			ObjectRepo.Add(key.ToUpper(), uiElement);
		}

		public void AcceptTheConfirmation()
		{
			((IWebDriver) GetBrowser).SwitchTo().Alert().Accept();
		}

		public IWebElement GetElement(string key, string selectionMethod, string selection)
		{
			var elementList = ObjectRepo;
			if (elementList.ContainsKey(key)) return GetElementBy(elementList[key].SelectionMethod, elementList[key].Selection);
			var element = GetElementBy(selectionMethod, selection);
			if (element == null) return null;
			AddNewElementToObjectRepo(key, new UiElement { SelectionMethod = selectionMethod, Selection = selection });
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
			Assert.AreEqual<string>(expectedContent, element.Text.Trim().Replace("\r\n", ""));
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
			((IWebDriver) GetBrowser).Navigate().GoToUrl(new Uri(url));
		}

		private IWebElement GetElementByKey(string key)
		{
			var elementList = ObjectRepo;
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
			return new WebDriverWait(((IWebDriver) GetBrowser), TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists((selction)));
		}

		public bool WaitForElementContentToLoad(string key, string content, string selectionMethod = null, string selection = null)
		{
			var elementList = ObjectRepo;
			key = key.ToUpper();
			if (!elementList.ContainsKey(key) && selectionMethod == null && selection == null) return false;
			if (!elementList.ContainsKey(key) && selectionMethod != null && selection != null) elementList.Add(key, new UiElement { Selection = selection, SelectionMethod = selectionMethod });

			switch (elementList[key].SelectionMethod.ToUpper())
			{
				case "ID":
					return new WebDriverWait(((IWebDriver) GetBrowser), TimeSpan.FromSeconds(timeOut)).Until(d => d.FindElement(By.Id(elementList[key].Selection)).Text.Contains(content));

				case "XPATH":
					return new WebDriverWait(((IWebDriver) GetBrowser), TimeSpan.FromSeconds(timeOut)).Until(d => d.FindElement(By.XPath(elementList[key].Selection)).Text.Contains(content));
			}
			return false;
		}

		public void IsElementVisible(string elementKey)
		{
			Assert.IsNotNull(GetElementByKey(elementKey));
		}

		public void IsElementVisible(string elementKey, string selectionMethod, string selection)
		{
			Assert.IsNotNull(GetElement(elementKey, selectionMethod, selection));
		}

		public string GetElementText(string elementKey)
		{
			var element = GetElementByKey(elementKey);
			if (element == null) Assert.Fail("\"" + elementKey + "\" is not avilable to read the content.");
			return element.Text;
		}

		public string GetElementText(string elementKey, string selectionMethod, string selection)
		{
			var element = GetElement(elementKey, selectionMethod, selection);
			if (element == null) Assert.Fail("\"" + elementKey + "\" is not avilable to read the content.");
			return element.Text;
		}

		string IFeatureTestHelper.SutUrl
		{
			get { return SutUrl; }
		}

		public IDictionary<string, UiElement> ObjectRepo { get; set; }

		public object GetBrowser { get; set; }

		public void InitilizeTests(string browserType, string objectRepoSourcePath)
		{
			var Browser = (IWebDriver) GetBrowser;
			if (Browser == null)
			{
				switch (browserType)
				{
					case "FireFox":
						Browser = new FirefoxDriver();
						break;
				}
				Browser.Manage().Window.Maximize();
				Browser.Manage().Cookies.DeleteAllCookies();
				GetBrowser = Browser;
			}

			if (GetBrowser == null) throw new Exception("Browser driver initilization error. Please ensure you have set the \"UI.Tests.Target.Browser\" setting correctly in the App.Config file.");

			if (ObjectRepo.Count != 0) return; //ensure we don't Unnecessarily  read and create the repo all over again.

			var fileList = Directory.GetFiles(objectRepoSourcePath, "*.csv");
			foreach (var file in fileList)
			{
				var reader = new StreamReader(File.OpenRead(file));
				reader.ReadLine(); //read out the first line so the topics line is ignored
				while (!reader.EndOfStream)
				{
					var line = reader.ReadLine();
					var values = line.Split(',');
					if (ObjectRepo.ContainsKey(values[0].Trim().ToUpper())) continue;
					ObjectRepo.Add(values[0].Trim().ToUpper(), new UiElement
					{
						SelectionMethod = values[1].Trim(),
						Selection = values[2].Trim()
					});
				}
			}
		}
	}
}