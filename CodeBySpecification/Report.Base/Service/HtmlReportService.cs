using CodeBySpecification.API;
using Newtonsoft.Json.Linq;
using RazorEngine;
using RazorEngine.Templating;
using System.Configuration;
using System.IO;
using System.Reflection;

namespace Report.Base.Service
{
	public class HtmlReportService : IReportService
	{

        private static string path;
        public HtmlReportService()
        {
            var baseDir = new DirectoryInfo(System.AppDomain.CurrentDomain.BaseDirectory + "\\..\\..");
            path = baseDir.FullName+"\\Templates\\page.html";
        }
		public string Generate(JObject test)
		{
			string template = GetReportTemplate();
			var result =
				 Engine.Razor.RunCompile(template, "test", null, new { test = test });
			return result;
		}

        private static string GetReportTemplate()
        {
            return System.IO.File.ReadAllText(path);
        }

	}
}