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
			return @"
<h1>@Model.Feature.title</h1>
<p>@Model.Feature.description</p>
<ul>@foreach (var scenario in @Model.Feature.scenarios)
{
    <li> @scenario.title </li>
}
</ul>";
		}
	}
}