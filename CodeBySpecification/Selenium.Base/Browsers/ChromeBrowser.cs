using CodeBySpecification.API.Domain;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Selenium.Base.Api;

namespace Selenium.Base.Browsers
{
	public class ChromeBrowser : IBrowser
	{
		private const string browserType = BrowserTypes.CHROME;
		public string Type { get; set; }

		public ChromeBrowser(string type)
		{
			this.Type = type;
		}

		public IWebDriver Create()
		{
			if (browserType == Type)
			{
				var profileCH = new ChromeOptions();
				var browser = new ChromeDriver(profileCH);
				browser.Manage().Cookies.DeleteAllCookies();
				return browser;
			}
			return null;
		}
	}
}
