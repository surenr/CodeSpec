using CodeBySpecification.API;
using Newtonsoft.Json.Linq;
using RazorEngine;
using RazorEngine.Templating;

namespace Report.Base.Service
{
	public class HtmlReportService : IReportService
	{
		private static string path;

		public HtmlReportService()
		{
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
			return Properties.Resources.html_template;
		}
	}
}