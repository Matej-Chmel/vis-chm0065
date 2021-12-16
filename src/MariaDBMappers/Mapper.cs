using System;
using System.Collections.Generic;
using DomainObjects;
using MariaDBGateway;
using MySql.Data.MySqlClient;

namespace MariaDBMappers {
	public abstract class Mapper<T> where T : DomainObject {
		readonly Dictionary<int, T> identityMap = new();
		readonly string table;

		static MySqlDataReader DataReader(string query) => Database.GetInstance().Select(query);

		List<T> SelectList(string query) {
			var reader = DataReader(query);
			var res = new List<T>();

			while(reader.Read()) {
				var id = Convert.ToInt32(reader["id"]);

				if(identityMap.ContainsKey(id))
					res.Add(identityMap[id]);
				else {
					var t = FromReader(reader, id);
					identityMap[id] = t;
					res.Add(t);
				}
			}

			reader.Close();
			return res;
		}

		protected void Insert(T t, string defClause, string valuesClause) {
			var db = Database.GetInstance();
			db.Statement($"INSERT INTO {table} ({defClause}) VALUES ({valuesClause})");
			var reader = db.Select("SELECT LAST_INSERT_ID()");
			reader.Read();
			t.ID = reader.GetInt32(0);
			identityMap[t.ID] = t;
			reader.Close();
		}

		protected Mapper(string table) => this.table = table.Column();
		protected MapperRegistry Mappers => MapperRegistry.GetInstance();
		protected void Update(T t, string setClause) => Database.GetInstance().Statement($"UPDATE {table} SET {setClause} WHERE id = {t.ID}");

		public void Delete(T t) {
			identityMap.Remove(t.ID);
			Database.GetInstance().Statement($"DELETE FROM {table} WHERE id = {t.ID}");
		}

		public void DeleteCustom(string queryPart) {
			var items = FindCustom(queryPart);

			foreach(var i in items)
				identityMap.Remove(i.ID);

			Database.GetInstance().Statement($"DELETE FROM {table} {queryPart}");
		}

		public T Find(int id) {
			if(identityMap.ContainsKey(id))
				return identityMap[id];

			var reader = DataReader($"SELECT * FROM {table} WHERE id = {id}");
			reader.Read();
			var res = FromReader(reader, id);
			identityMap[id] = res;
			reader.Close();
			return res;
		}

		public List<T> FindAll() => SelectList($"SELECT * FROM {table}");
		public List<T> FindComplex(string query) => SelectList(query);
		public List<T> FindCustom(string queryPart) => SelectList($"SELECT * FROM {table} {queryPart}");
		public abstract T FromReader(MySqlDataReader r, int id);
	}
}
