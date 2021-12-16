using System.Collections.Generic;
using System.Xml.Linq;
using DomainObjects;

namespace XMLMappers {
	public class BorrowingMapper : Mapper<Borrowing>, IBorrowingMapper {
		public BorrowingMapper() : base("borrowing") {}
		public List<Borrowing> FindFor(Customer customer) => FindWhere(b => b.Waiting && b.CustomerID == customer.ID);

		public override Borrowing FromElement(XElement e) => new() {
			CustomerID = e.IntValue("customer"),
			Late = e.BoolValue("late"),
			Lost = e.BoolValue("lost"),
			OrderItemID = e.IntValue("orderItem"),
			Returned = e.BoolValue("returned")
		};

		public override void ToElement(Borrowing t, XElement e) => e
			.AddValue("customer", t.CustomerID)
			.AddValue("late", t.Late)
			.AddValue("lost", t.Lost)
			.AddValue("orderItem", t.OrderItemID)
			.AddValue("returned", t.Returned);
	}
}
