using System.Collections.Generic;
using DomainObjects;

namespace ServiceLayer {
	public class ImportProcessModel : EmployeeModel {
		List<OrderItem> items = null;

		public string Location { get; set; }
		public int OrderItemID { get; set; }

		public void AcceptImport() => ActiveOrderItem().MakeReady();
		public OrderItem ActiveOrderItem() => Mappers().OrderItemMapper.Find(OrderItemID);
		public void ChangeLocation() => ActiveOrderItem().Stock.ChangeLocation(Location);
		public void DenyImport() => ActiveOrderItem().DenyImport();
		public bool Empty() => Items().Count == 0;
		public void FileUploaded(string filename) => ActiveOrderItem().Stock.Product.FileUploaded(filename);
		public OrderItem First() => Items()[0];
		public ImportProcessModel() {}
		public ImportProcessModel(int employeeID) : base(employeeID) {}
		public ImportProcessModel(ImportProcessModel m) : base(m.EmployeeID) => OrderItemID = m.OrderItemID;

		public List<OrderItem> Items() {
			if(items is null)
				items = Mappers().OrderItemMapper.FindForImport(Mappers().CustomerMapper.Find(EmployeeID));
			return items;
		}

		public bool ShouldSkipScan() => ActiveOrderItem().HasFile;
	}
}
