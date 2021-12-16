using System.Collections.Generic;
using System.Xml.Linq;
using DomainObjects;

namespace XMLMappers {
	public class PenaltyMapper : Mapper<Penalty>, IPenaltyMapper {
		public void DeleteFor(Borrowing b) => DeleteWhere(p => p.BorrowingID == b.ID);
		public List<Penalty> FindFor(Borrowing b) => FindWhere(p => p.BorrowingID == b.ID);
		public List<Penalty> FindFor(Customer c) => FindWhere(p => p.CustomerID == c.ID && !p.Paid);

		public override Penalty FromElement(XElement e) => new() {
			Amount = e.IntValue("amount"),
			BorrowingID = e.IntValue("borrowing"),
			CustomerID = e.IntValue("customer"),
			Description = e.StringValue("description"),
			Paid = e.BoolValue("paid")
		};

		public PenaltyMapper() : base("penalty") {}

		public override void ToElement(Penalty t, XElement e) => e
			.AddValue("amount", t.Amount)
			.AddValue("borrowing", t.BorrowingID)
			.AddValue("customer", t.CustomerID)
			.AddValue("description", t.Description)
			.AddValue("paid", t.Paid);
	}
}
