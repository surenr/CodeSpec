using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeBySpecification.API.Domain;

namespace CodeBySpecification.API.Service.Api
{
	public interface IObjectRepoService
	{
		IDictionary<string, UiElement> GetRepo();

		void Populate(string objectRepoResource);

		UiElement GetObject(string key);

		bool ObjectExists(string key);

		void AddObject(string key, UiElement element);

		void AddObject(string key, string selectionType, string selection);

		void DeleteObject(string key);

		void Save(string objectRepoResource);

		int ObjectCount();
	}
}
