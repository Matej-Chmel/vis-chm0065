using System.Xml.Linq;
using DomainObjects;

namespace XMLMappers {
	public class ExportOrderMapper : Mapper<ExportOrder> {
		public ExportOrderMapper() : base("order") {}

		public override ExportOrder FromElement(XElement e) => new() {
			Amount = e.IntValue("amount"),
			Completed = e.BoolValue("completed"),
			Confirmed = e.BoolValue("confirmed"),
			CustomerID = e.IntValue("customer"),
			HasDelivery = e.BoolValue("hasDelivery"),
			Online = e.BoolValue("online"),
			Paid = e.BoolValue("paid")
		};

		public override void ToElement(ExportOrder t, XElement e) => e
			.AddValue("amount", t.Amount)
			.AddValue("completed", t.Completed)
			.AddValue("confirmed", t.Confirmed)
			.AddValue("customer", t.CustomerID)
			.AddValue("hasDelivery", t.HasDelivery)
			.AddValue("online", t.Online)
			.AddValue("paid", t.Paid)
			.AddValue("type", "Export");
	}
}
