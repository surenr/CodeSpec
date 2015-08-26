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
using OpenQA.Selenium.Appium.Android;
using Selenium.Base.Api;
using OpenQA.Selenium.Remote;
using System.Configuration;

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
                capabilities.SetCapability("platformName", "Android");
                capabilities.SetCapability("deviceName", ConfigurationManager.AppSettings["UI.Tests.Appium.capability.deviceName"]);
                AppiumDriver<AppiumWebElement> browser = new AndroidDriver<AppiumWebElement>(new Uri(ConfigurationManager.AppSettings["UI.Tests.Target.URI"]), capabilities);
				return browser;
			}
			return null;
		}
	}
}
