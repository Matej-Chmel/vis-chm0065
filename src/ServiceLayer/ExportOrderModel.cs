using DomainObjects;

namespace ServiceLayer {
	public class ExportOrderModel : CustomerModel {
		public static bool ReservationLimitReached(int reservationCount) => reservationCount >= 3;

		public void AddItem(int stockID, bool copy) {
			var order = Order();

			if(order is null) {
				order = new ExportOrder() { CustomerID = CustomerID };
				order.Create();
			}

			order.Add(new OrderItem() { Copy = copy, StockID = stockID });
		}

		public bool AddressMissing() => Order().HasDelivery && Customer().AddressMissing;
		public void Confirm() => Order().Confirm();
		public void DeleteItem(int orderItemID) => Order().Delete(Mappers().OrderItemMapper.Find(orderItemID));

		public void EnsureCorrectDelivery() {
			if(!PickUpPossible() && !Order().HasDelivery)
				Order().ChangeDelivery(true);
		}

		public ExportOrderModel() {}
		public ExportOrderModel(int customerID) : base(customerID) {}
		public bool HasOrder() => Order() is not null;
		public bool HasPenalties() => Mappers().PenaltyMapper.FindFor(Customer()).Count > 0;
		public Order Order() => Mappers().OrderMapper.FindActiveFor(Customer());
		public bool PickUpPossible() => Order().PickUpPossible();

		public int ReservationCount() {
			var customer = Customer();
			var borrowingCount = Mappers().BorrowingMapper.FindFor(customer).Count;
			var confirmedItemsCount = Mappers().OrderItemMapper.FindConfirmedFor(customer).Count;
			var unconfirmedItemsCount = Order()?.Items.Count ?? 0;
			return borrowingCount + confirmedItemsCount + unconfirmedItemsCount;
		}
	}
}
