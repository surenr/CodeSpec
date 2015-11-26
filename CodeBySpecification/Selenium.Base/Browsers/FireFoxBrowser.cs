using CodeBySpecification.API.Domain;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using Selenium.Base.Api;
using System.Configuration;

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
                if(ConfigurationManager.AppSettings["UI.Tests.Reports.output.path"] != null && ConfigurationManager.AppSettings["UI.Tests.Reports.output.path"].Equals("true"))
                {
                    profileFF.SetPreference("security.fileuri.strict_origin_policy", false);
                }
                return new FirefoxDriver(profileFF);
			}
			return null;
		}
	}
}
