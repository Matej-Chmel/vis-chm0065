using System.Collections.Generic;
using DomainObjects;

namespace ServiceLayer {
	public class UnpaidOrdersModel : CustomerModel {
		List<Order> orders;

		public bool Empty() => Orders().Count == 0;

		public List<Order> Orders() {
			if(orders is null)
				orders = Mappers().OrderMapper.FindUnpaid(Customer());
			return orders;
		}

		public UnpaidOrdersModel() {}
		public UnpaidOrdersModel(int customerID) : base(customerID) {}
	}
}
