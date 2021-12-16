using System.Collections.Generic;
using DomainObjects;

namespace ServiceLayer {
	public class DeliveryModel : ServiceModel {
		List<Order> orders;

		public DeliveryModel() {}
		public DeliveryModel(int orderID) => Mappers().OrderMapper.Find(orderID).Complete();
		public bool Empty() => Orders().Count == 0;

		public List<Order> Orders() {
			if(orders is null)
				orders = Mappers().OrderMapper.FindProcessed();
			return orders;
		}
	}
}
