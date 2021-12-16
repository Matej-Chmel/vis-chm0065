namespace ServiceLayer {
	public class PaymentResultModel : ServiceModel {
		public int CustomerID { get; set; }
		public bool PayingOrder { get; set; }
		public bool Success { get; set; }

		public PaymentResultModel() {}

		public PaymentResultModel(bool success, bool payingOrder) {
			Success = success;
			PayingOrder = payingOrder;
		}

		public PaymentResultModel(bool success, int customerID) : this(success, false) => CustomerID = customerID;
	}
}
