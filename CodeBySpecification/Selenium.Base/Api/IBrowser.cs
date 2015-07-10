using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace Selenium.Base.Api
{
	public interface IBrowser
	{
		string Type { get; set; }

		IWebDriver Create();
	}
}
