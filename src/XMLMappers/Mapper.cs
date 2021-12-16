using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using DomainObjects;
using XMLGateway;

namespace XMLMappers {
	public abstract class Mapper<T> where T : DomainObject {
		readonly Document doc;
		readonly Dictionary<int, T> identityMap = new();

		protected void DeleteWhere(Func<T, bool> predicate) => FindAll().Where(predicate).ToList().ForEach(t => Delete(t));
		protected List<T> FindWhere(Func<T, bool> predicate) => FindAll().Where(predicate).ToList();
		protected Mapper(string name) => doc = Database.GetInstance().GetDocument(name);
		protected MapperRegistry Mappers => MapperRegistry.GetInstance();

		public void Delete(T t) {
			identityMap.Remove(t.ID);
			doc.Read(t.ID).Remove();
			doc.Save();
		}

		public T Find(int id) {
			if(identityMap.ContainsKey(id))
				return identityMap[id];

			var item = FromElement(doc.Read(id));
			item.ID = id;
			identityMap[id] = item;
			return item;
		}

		public List<T> FindAll() {
			var elements = doc.ReadAll();
			var res = new List<T>();

			foreach(var e in elements) {
				var id = e.ID();

				if(identityMap.ContainsKey(id))
					res.Add(identityMap[id]);
				else {
					var t = FromElement(e);
					t.ID = id;
					identityMap[id] = t;
					res.Add(t);
				}
			}

			return res;
		}


		public abstract T FromElement(XElement e);

		public void Insert(T t) {
			t.ID = doc.Add(ToElement(t));
			doc.Save();
		}

		XElement ToElement(T t) {
			var e = new XElement(doc.RecordName);
			ToElement(t, e);
			return e;
		}

		public abstract void ToElement(T t, XElement e);

		public void Update(T t) {
			var e = ToElement(t);
			e.SetAttributeValue("id", t.ID);
			doc.Read(t.ID).ReplaceWith(e);
			doc.Save();
		}
	}
}
