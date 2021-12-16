using System;
using System.Collections.Generic;
using DomainObjects;

namespace ServiceLayer {
	public class LoginModel : ServiceModel {
		public int CustomerID { get; set; }
		public int Reason { get; set; }

		public List<Customer> Customers() {
			var m = Mappers().CustomerMapper;

			return Reason is 0 or 5 or 6
				? m.FindAll()
				: Reason is 1 or 2 or 4
				? m.FindAllWarehousemen()
				: Reason == 3
				? m.FindAllCashiers()
				: throw new Exception($"Unknown login reason {Reason}.");
		}

		public LoginModel() {}
		public LoginModel(int reason) => Reason = reason;
	}
}
