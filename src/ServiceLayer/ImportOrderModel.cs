using System.Collections.Generic;
using DomainObjects;

namespace ServiceLayer {
	public class ImportOrderModel : ServiceModel {
		public int BranchID { get; set; }
		public int ImportOrderID { get; set; }
		public Product Product { get; set; }
		public int ProductID { get; set; }

		public void AddItem() {
			Order order;

			if(ImportOrderID < 0) {
				order = new ImportOrder();
				order.Create();
				ImportOrderID = order.ID;
			} else
				order = Order();

			var stock = new Stock(BranchID, ProductID);
			stock.Create();
			order.Add(new OrderItem() { StockID = stock.ID });
		}

		public List<Branch> Branches() => Mappers().BranchMapper.FindAll();
		public void Confirm() => Order().Confirm();
		public ImportOrderModel() => ImportOrderID = -1;
		public ImportOrderModel(ImportOrderModel m) => ImportOrderID = m.ImportOrderID;
		public List<OrderItem> Items() => Order().Items;
		public Order Order() => Mappers().OrderMapper.Find(ImportOrderID);
		public List<Product> Products() => Mappers().ProductMapper.FindAll();
	}
}
