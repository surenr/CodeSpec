using System;
using System.Configuration;
using System.Linq;
using CodeBySpecification.API.Domain;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Remote;
using Selenium.Base.Api;

namespace Selenium.Base.Browsers
{
	public class AndroidBrowser : IBrowser
	{
		private const string browserType = BrowserTypes.ANDROID;
		public string Type { get; set; }

		public AndroidBrowser(string type)
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
				foreach (var keys in requriedKeys)
				{
					var keyArray = keys.Split('.');
					var tempVallue = ConfigurationManager.AppSettings[keys];
					capabilities.SetCapability(keyArray[(keyArray.Length - 1)], tempVallue);
				}
				AppiumDriver<AppiumWebElement> browser = new AndroidDriver<AppiumWebElement>(new Uri(ConfigurationManager.AppSettings["UI.Tests.Appium.URI"]), capabilities);
                var x = browser.Contexts;
                if (ConfigurationManager.AppSettings["UI.Tests.Appium.context"] != null && !ConfigurationManager.AppSettings["UI.Tests.Appium.context"].Equals(""))
                {
                    foreach (string contextName in browser.Contexts)
                    {
                        if (contextName.Contains(ConfigurationManager.AppSettings["UI.Tests.Appium.context"].ToUpper()))
                        {
                            browser.Context = contextName;
                        }
                    }
                }
                var z = browser.Context;
                return browser;
			}
			return null;
		}
	}
}
