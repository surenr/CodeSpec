using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using CodeBySpecification.API.Domain;
using CodeBySpecification.API.Service.Api;

namespace DataRepository.Base.Service
{
	public class CSVDataRepositoryService : IDataRepoService
	{
		private static readonly IDictionary<string, string> dataRepo = new Dictionary<string, string>();

		public void Populate(string dataRepoResource)
		{
			var fileList = Directory.GetFiles(dataRepoResource, "*.csv");
			foreach (var file in fileList)
			{
				var reader = new StreamReader(File.OpenRead(file));
				reader.ReadLine(); //read out the first line so the topics line is ignored
				while (!reader.EndOfStream)
				{
					var line = reader.ReadLine();
					if (string.IsNullOrEmpty(line)) continue;
					var values = line.Split(',');
					if (dataRepo.ContainsKey(values[0].Trim().ToUpper())) continue;
					dataRepo.Add(values[0].Trim().ToUpper(), values[1].Trim());
				}
			}
		}

		public string GetData(string key)
		{
			return !dataRepo.ContainsKey(key.ToUpper()) ? null : dataRepo[key.ToUpper()];
		}

		public bool DataExists(string key)
		{
			return dataRepo.ContainsKey(key.ToUpper());
		}

		public void AddData(string key, string value)
		{
			if (dataRepo.ContainsKey(key.ToUpper()))
			{
				dataRepo[key.ToUpper()] = value;
			}
			else
			{
				dataRepo.Add(key.ToUpper(), value);
			}
		}

		public void DeleteData(string key)
		{
			dataRepo.Remove(key.ToUpper());
		}

		public void Save(string dataRepoResource)
		{
			var fileList = Directory.GetFiles(dataRepoResource, "*.csv");
			foreach (var file in fileList)
			{
				File.Delete(file);
			}
			var newFilePath = dataRepoResource + "objecrepo" +
											DateTime.Now.ToUniversalTime()
												 .Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc))
												 .TotalMilliseconds + ".csv";
			var csvString = new StringBuilder();
			csvString.Append("Key,Value");
			foreach (var element in dataRepo)
			{
				var newLine = string.Format("{0},{1}{3}", element.Key, element.Value, Environment.NewLine);
				csvString.Append(newLine);
			}
			File.WriteAllText(newFilePath, csvString.ToString());
		}

		public IDictionary<string, string> GetRepo()
		{
			return dataRepo;
		}

		public int DataCount()
		{
			return dataRepo.Count;
		}
	}
}
