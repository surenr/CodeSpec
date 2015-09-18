using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeBySpecification.API.Domain;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.iOS;
using Selenium.Base.Api;
using OpenQA.Selenium.Remote;
using System.Configuration;

namespace Selenium.Base.Browsers
{
	public class iOSBrowser : IBrowser
	{
		private const string browserType = BrowserTypes.IOS;
		public string Type { get; set; }

		public iOSBrowser(string type)
		{
			this.Type = type;
		}

		public IWebDriver Create()
		{
			if (browserType == Type)
			{
                DesiredCapabilities capabilities = new DesiredCapabilities();
                var settings = ConfigurationManager.AppSettings;
                var requriedKeys = settings.AllKeys.Where(t => t.Contains("UI.Tests.Appium.capability"));
                foreach(var keys in requriedKeys)
                {
                    var keyArray = keys.Split('.');
                    var tempVallue = ConfigurationManager.AppSettings[keys];
                    capabilities.SetCapability(keyArray[(keyArray.Length-1)], tempVallue);
                }
                AppiumDriver<AppiumWebElement> browser = new IOSDriver<AppiumWebElement>(new Uri(ConfigurationManager.AppSettings["UI.Tests.Appium.URI"]), capabilities);
				return browser;
			}
			return null;
		}
	}
}
