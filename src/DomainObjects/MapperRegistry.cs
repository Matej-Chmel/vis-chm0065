namespace DomainObjects {
	public class MapperRegistry {
		static MapperRegistry instance = null;

		MapperRegistry() {}

		public static MapperRegistry GetInstance() {
			if(instance is null)
				instance = new MapperRegistry();
			return instance;
		}

		public IBorrowingMapper BorrowingMapper { get; set; }
		public IBranchMapper BranchMapper { get; set; }
		public ICustomerMapper CustomerMapper { get; set; }
		public IOrderItemMapper OrderItemMapper { get; set; }
		public IOrderMapper OrderMapper { get; set; }
		public IPenaltyMapper PenaltyMapper { get; set; }
		public IProductMapper ProductMapper { get; set; }
		public IRequestMapper RequestMapper { get; set; }
		public IStockMapper StockMapper { get; set; }
	}
}
