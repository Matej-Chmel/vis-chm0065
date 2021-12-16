using System.Xml.Linq;
using DomainObjects;

namespace XMLMappers {
	public class ImportOrderMapper : Mapper<ImportOrder> {
		public override ImportOrder FromElement(XElement e) => new() { Confirmed = e.BoolValue("confirmed") };

		public ImportOrderMapper() : base("order") {}

		public override void ToElement(ImportOrder t, XElement e) => e
			.AddValue("confirmed", t.Confirmed)
			.AddValue("type", "Import");
	}
}
