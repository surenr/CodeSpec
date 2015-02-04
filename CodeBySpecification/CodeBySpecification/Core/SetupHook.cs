using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using CodeBySpecification.Domain;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using TechTalk.SpecFlow;

namespace CodeBySpecification.Core
{
	[Binding]
	public class SetupHook
	{
		// For additional details on SpecFlow hooks see http://go.specflow.org/doc-hooks

		public static IWebDriver Browser = null;
		public static readonly Dictionary<string, ElementInfo> ObjectRepo = new Dictionary<string, ElementInfo>();

		[BeforeFeature("SeleniumTest")]
		public static void BeforeSeleniumTestFeature()
		{
			var browserName = ConfigurationManager.AppSettings["UI.Tests.Target.Browser"];
			var objectDefSource = ConfigurationManager.AppSettings["UI.Tests.Object.Definitions.Path"];

			if (Browser == null)
			{
				switch (browserName)
				{
					case "FireFox":
						Browser = new FirefoxDriver();
						break;
				}
				Browser.Manage().Window.Maximize();
				Browser.Manage().Cookies.DeleteAllCookies();
			}

			if (Browser == null) throw new Exception("Browser driver initilization error. Please ensure you have set the \"UI.Tests.Target.Browser\" setting correctly in the App.Config file.");

			if (ObjectRepo.Count != 0) return; //ensure we don't Unnecessarily  read and create the repo all over again.

			var fileList = Directory.GetFiles(objectDefSource, "*.csv");
			foreach (var file in fileList)
			{
				var reader = new StreamReader(File.OpenRead(file));
				reader.ReadLine(); //read out the first line so the topics line is ignored
				while (!reader.EndOfStream)
				{
					var line = reader.ReadLine();
					var values = line.Split(',');
					if (ObjectRepo.ContainsKey(values[0].Trim().ToUpper())) continue;
					ObjectRepo.Add(values[0].Trim().ToUpper(), new ElementInfo
					{
						SelectionMethod = values[1].Trim(),
						Selection = values[2].Trim()
					});
				}
			}
		}

		[AfterFeature("SeleniumTest")]
		public static void AfterSeleniumTestFeature()
		{
			//Browser.Quit();
		}
	}
}
