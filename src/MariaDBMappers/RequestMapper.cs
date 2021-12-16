using System;
using DomainObjects;
using MySql.Data.MySqlClient;

namespace MariaDBMappers {
	public class RequestMapper : Mapper<Request>, IRequestMapper {
		public Request FindFor(Product p) {
			var requests = FindCustom($"WHERE `product` = {p.ID}");

			return requests.Count == 0
				? null
				: requests.Count == 1
				? requests[0]
				: throw new Exception($"Product {p.FullName} has multiple requests created for it.");
		}

		public override Request FromReader(MySqlDataReader r, int id) => new() {
			ID = id,
			Accepted = Convert.ToBoolean(r["accepted"]),
			CustomerID = Convert.ToInt32(r["customer"]),
			ProductID = Convert.ToInt32(r["product"])
		};

		public void Insert(Request r) => Insert(r, "`customer`, `product`", $"{r.CustomerID}, {r.ProductID}");
		public RequestMapper() : base("Request") {}
		public void Update(Request r) => Update(r, $"`accepted` = {r.Accepted}");
	}
}
