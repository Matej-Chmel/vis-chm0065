using System.Collections.Generic;
using System.Xml.Linq;
using DomainObjects;

namespace XMLMappers {
	public class StockMapper : Mapper<Stock>, IStockMapper {
		public List<Stock> FindFor(Product p) => FindWhere(s => s.CatalogReady && s.ProductID == p.ID);
		public List<Stock> FindReturned(Customer employee) => FindWhere(s => s.Returned(employee.Branch));

		public override Stock FromElement(XElement e) => new() {
			BorrowingID = e.IntNullableValue("borrowing"),
			BranchID = e.IntNullableValue("branch"),
			Copy = e.BoolValue("copy"),
			Deleted = e.BoolValue("deleted"),
			Location = e.StringValue("location"),
			ProductID = e.IntValue("product"),
			Reserved = e.BoolValue("reserved")
		};

		public StockMapper() : base("stock") {}

		public override void ToElement(Stock t, XElement e) => e
			.AddValue("borrowing", t.BorrowingID)
			.AddValue("branch", t.BranchID)
			.AddValue("copy", t.Copy)
			.AddValue("deleted", t.Deleted)
			.AddValue("location", t.Location)
			.AddValue("product", t.ProductID)
			.AddValue("reserved", t.Reserved);
	}
}
