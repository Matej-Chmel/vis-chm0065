using System.Collections.Generic;
using DomainObjects;

namespace ServiceLayer {
	public class ExportProcessModel : EmployeeModel {
		List<OrderItem> items;

		public int OrderItemID { get; set; }
		public int Stage { get; set; }
		public int StockID { get; set; }

		public OrderItem ActiveOrderItem() => Mappers().OrderItemMapper.Find(OrderItemID);
		public void ChangeLocation() => Stock().ChangeLocation(HasDelivery() ? "Výdejní místo" : "Pokladna");
		public void Confirm() => ActiveOrderItem().MakeReady();
		public bool Copy() => ActiveOrderItem().Copy;
		public int CreateCopy() => ActiveOrderItem().CreateCopy().ID;
		public bool Empty() => Items().Count == 0;
		public OrderItem First() => Items()[0];
		public ExportProcessModel() {}
		public ExportProcessModel(int employeeID) : base(employeeID) {}

		public ExportProcessModel(int employeeID, int orderItemID) : base(employeeID) {
			OrderItemID = orderItemID;
			StockID = ActiveOrderItem().StockID;
		}

		public ExportProcessModel(int employeeID, int orderItemID, int copiedStockID) : base(employeeID) {
			OrderItemID = orderItemID;
			StockID = copiedStockID;
		}

		public bool HasDelivery() => ActiveOrderItem().HasDelivery;

		public List<OrderItem> Items() {
			if(items is null)
				items = Mappers().OrderItemMapper.FindForExport(Employee());
			return items;
		}

		public Stock Stock() => Mappers().StockMapper.Find(StockID);
	}
}
