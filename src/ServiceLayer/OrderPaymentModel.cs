using DomainObjects;

namespace ServiceLayer {
	public class OrderPaymentModel : ServiceModel {
		public int OrderID { get; set; }

		public Order Order() => Mappers().OrderMapper.Find(OrderID);
		public OrderPaymentModel() {}
		public OrderPaymentModel(int orderID) => OrderID = orderID;

		public bool ProcessResult(bool success) {
			if(success)
				Order().Pay();
			else
				Order().Delete();
			return success;
		}
	}
}
