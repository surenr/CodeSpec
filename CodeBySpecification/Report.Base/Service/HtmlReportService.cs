using CodeBySpecification.API;
using Newtonsoft.Json.Linq;
using RazorEngine;
using RazorEngine.Templating;

namespace Report.Base.Service
{
	public class HtmlReportService : IReportService
	{
		public string Generate(JObject currentFeature)
		{
			string template = GetReportTemplate();
			var result =
				 Engine.Razor.RunCompile(template, "feature", null, new { Feature = currentFeature });
			return result;
		}

		private static string GetReportTemplate()
		{
            return System.IO.File.ReadAllText(".\\Report\\Template\\page.html");
        }
	}
}