using Newtonsoft.Json.Linq;

namespace CodeBySpecification.API
{
	public interface IReportService
	{
		string Generate(JObject currentFeature);
	}
}