namespace DomainObjects {
	public class OrderItem : DomainObject {
		const int PRICE_OF_BORROWING = 65;
		const int PRICE_OF_COPY = 95;

		public bool Copy { get; set; } = false;
		public Order Order => Mappers.OrderMapper.Find(OrderID);
		public int OrderID { get; set; }
		public bool Ready { get; set; } = false;
		public Stock Stock => Mappers.StockMapper.Find(StockID);
		public int StockID { get; set; }

		public Branch Branch => Stock.Branch;

		public void CreateBorrowing(Customer customer) {
			if(Copy)
				return;

			var borrowing = new Borrowing(customer.ID, ID);
			borrowing.Create();
			Stock.Borrow(borrowing);
		}

		public Stock CreateCopy() {
			var copy = Stock.CreateCopy();
			StockID = copy.ID;
			Mappers.OrderItemMapper.Update(this);
			return copy;
		}

		public void DenyImport() {
			MakeReady();
			Stock.DenyImport();
		}

		public bool HasDelivery => Mappers.OrderMapper.FindFor(this).HasDelivery;
		public bool HasFile => Stock.Product.HasFile;

		public void MakeReady() {
			Ready = true;
			Mappers.OrderItemMapper.Update(this);
		}

		public int Price => Copy ? PRICE_OF_COPY : PRICE_OF_BORROWING;
	}
}
