using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeBySpecification.API.Domain;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using Selenium.Base.Api;

namespace Selenium.Base.Browsers
{
	public class IEBrowser : IBrowser
	{
		private const string browserType = BrowserTypes.IE;
		public string Type { get; set; }

		public IEBrowser(string type)
		{
			this.Type = type;
		}

		public IWebDriver Create()
		{
			if (browserType == Type)
			{
				var profileIE = new InternetExplorerOptions();
				profileIE.EnableNativeEvents = true;
				var browser = new InternetExplorerDriver(profileIE);
				browser.Manage().Cookies.DeleteAllCookies();
				return browser;
			}
			return null;
		}
	}
}
