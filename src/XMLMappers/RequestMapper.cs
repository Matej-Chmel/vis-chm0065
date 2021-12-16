using System;
using System.Xml.Linq;
using DomainObjects;

namespace XMLMappers {
	public class RequestMapper : Mapper<Request>, IRequestMapper {
		public Request FindFor(Product p) {
			var requests = FindWhere(r => r.ProductID == p.ID);

			return requests.Count == 0
				? null
				: requests.Count == 1
				? requests[0]
				: throw new Exception($"Product {p.FullName} has multiple requests created for it.");
		}

		public override Request FromElement(XElement e) => new() {
			CustomerID = e.IntValue("customer"),
			ProductID = e.IntValue("product")
		};

		public RequestMapper() : base("request") {}

		public override void ToElement(Request t, XElement e) => e
			.AddValue("customer", t.CustomerID)
			.AddValue("product", t.ProductID);
	}
}
