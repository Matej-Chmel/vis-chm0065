using DomainObjects;

namespace ServiceLayer {
	public class ConfirmedOrderModel : CustomerModel {
		public int OrderID { get; set; }

		public ConfirmedOrderModel() {}
		public ConfirmedOrderModel(int customerID, int orderID) : base(customerID) => OrderID = orderID;
		public Order Order() => Mappers().OrderMapper.Find(OrderID);
	}
}
