using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeBySpecification.API.Domain;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using Selenium.Base.Api;

namespace Selenium.Base.Browsers
{
	public class FireFoxBrowser : IBrowser
	{
		private const string browserType = BrowserTypes.FIRE_FOX;
		public string Type { get; set; }

		public FireFoxBrowser(string type)
		{
			this.Type = type;
		}

		public IWebDriver Create()
		{
			if (browserType == Type)
			{
				var profileFF = new FirefoxProfile();
				profileFF.EnableNativeEvents = true;
				profileFF.AcceptUntrustedCertificates = true;
				return new FirefoxDriver(profileFF);
			}
			return null;
		}
	}
}
