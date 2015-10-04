using OpenQA.Selenium;

namespace Selenium.Base.Api
{
	public interface IBrowser
	{
		string Type { get; set; }

		IWebDriver Create();
	}
}
