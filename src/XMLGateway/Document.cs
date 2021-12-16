using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace XMLGateway {
	public class Document {
		readonly XDocument doc;
		int nextID;
		readonly string path;
		readonly XElement root;

		public string RecordName { get; private set; }

		public int Add(XElement e) {
			e.SetAttributeValue("id", nextID);
			root.Add(e);
			return nextID++;
		}

		public Document(string path, string recordName) {
			this.path = path;
			RecordName = recordName;
			doc = File.Exists(this.path) ? XDocument.Load(this.path) : new XDocument(new XElement("root", new XAttribute("nextID", 1)));
			root = doc.Element("root");
			nextID = root.ElementIntAttr("nextID");
		}

		public XElement Read(int id) => ReadAll().FirstOrDefault(e => e.ElementID() == id);
		public IEnumerable<XElement> ReadAll() => root.Descendants(RecordName);

		public void Save() {
			root.SetAttributeValue("nextID", nextID);
			using var w = XmlWriter.Create(path, new() {
				Encoding = new UTF8Encoding(false),
				Indent = true
			});
			doc.Save(w);
		}
	}
}
