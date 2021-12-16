using System.Collections.Generic;
using System.IO;

namespace XMLGateway {
	public class Database {
		static Database instance;

		readonly Dictionary<string, Document> documents = new();

		public string DirPath { private get; set; }

		public static Database GetInstance() {
			if(instance is null)
				instance = new Database();
			return instance;
		}

		Database() {}

		public Document GetDocument(string name) {
			if(documents.ContainsKey(name))
				return documents[name];

			var d = new Document(Path.Combine(DirPath, $"{name}.xml"), name);
			documents[name] = d;
			return d;
		}
	}
}
