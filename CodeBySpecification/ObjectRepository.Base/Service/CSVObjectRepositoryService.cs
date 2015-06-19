using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using CodeBySpecification.API.Domain;
using CodeBySpecification.API.Service.Api;

namespace ObjectRepository.Base.Service
{
	public class CSVObjectRepositoryService : IObjectRepoService
	{
		private readonly IDictionary<string, UiElement> objectRepo = new Dictionary<string, UiElement>();

		public void Populate(string objectRepoResource)
		{
			var fileList = Directory.GetFiles(objectRepoResource, "*.csv");
			foreach (var file in fileList)
			{
				var reader = new StreamReader(File.OpenRead(file));
				reader.ReadLine(); //read out the first line so the topics line is ignored
				while (!reader.EndOfStream)
				{
					var line = reader.ReadLine();
					if (string.IsNullOrEmpty(line)) continue;
					var values = line.Split(',');
					if (objectRepo.ContainsKey(values[0].Trim().ToUpper())) continue;
					objectRepo.Add(values[0].Trim().ToUpper(), new UiElement
					{
						SelectionMethod = values[1].Trim(),
						Selection = values[2].Trim()
					});
				}
			}
		}

		public UiElement GetObject(string key)
		{
			return !objectRepo.ContainsKey(key.ToUpper()) ? null : objectRepo[key.ToUpper()];
		}

		public bool ObjectExists(string key)
		{
			return objectRepo.ContainsKey(key.ToUpper());
		}

		public void AddObject(string key, UiElement element)
		{
			if (objectRepo.ContainsKey(key.ToUpper())) return;
			objectRepo.Add(key.ToUpper(), element);
		}

		public void AddObject(string key, string selectionType, string selection)
		{
			if (objectRepo.ContainsKey(key.ToUpper())) return;
			objectRepo.Add(key.ToUpper(), new UiElement
			{
				Selection = selection,
				SelectionMethod = selectionType
			});
		}

		public void DeleteObject(string key)
		{
			objectRepo.Remove(key.ToUpper());
		}

		public void Save(string objectRepoResource)
		{
			var fileList = Directory.GetFiles(objectRepoResource, "*.csv");
			foreach (var file in fileList)
			{
				File.Delete(file);
			}
			var newFilePath = objectRepoResource + "objecrepo" +
									DateTime.Now.ToUniversalTime()
										.Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc))
										.TotalMilliseconds + ".csv";
			var csvString = new StringBuilder();
			csvString.Append("Page Object,Selection Type,Selection");
			foreach (var element in objectRepo)
			{
				var newLine = string.Format("{0},{1},{2}{3}", element.Key, element.Value.SelectionMethod, element.Value.Selection, Environment.NewLine);
				csvString.Append(newLine);
			}
			File.WriteAllText(newFilePath, csvString.ToString());
		}

		public IDictionary<string, UiElement> GetRepo()
		{
			return objectRepo;
		}

		public int ObjectCount()
		{
			return objectRepo.Count;
		}
	}
}
