using System.Collections.Generic;
using CodeBySpecification.API.Domain;

namespace CodeBySpecification.API.Service.Api
{
	public interface IDataRepoService
	{
		IDictionary<string, string> GetRepo();

		void Populate(string objectRepoResource);

		string GetData(string key);

		bool DataExists(string key);

		void AddData(string key, string value);

		void DeleteData(string key);

		void Save(string objectRepoResource);

		int DataCount();
	}
}
