using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using DomainObjects;

namespace XMLMappers {
	public class CustomerMapper : Mapper<Customer>, ICustomerMapper {
		public CustomerMapper() : base("customer") {}
		public List<Customer> FindAllCashiers() => FindWhere(c => c.Job == "Pokladní");
		public List<Customer> FindAllWarehousemen() => FindWhere(c => c.Job == "Skladník");

		public List<Customer> FindWithBorrowing() {
			var borrowings = Mappers.BorrowingMapper.FindAll().Where(b => b.Waiting);
			return FindAll().Where(c => borrowings.Any(b => b.CustomerID == c.ID)).ToList();
		}

		public override Customer FromElement(XElement e) => new() {
			Address = e.StringValue("address"),
			BranchID = e.IntNullableValue("branch"),
			Job = e.StringValue("job"),
			Username = e.StringValue("username")
		};

		public override void ToElement(Customer t, XElement e) => e
			.AddValue("address", t.Address)
			.AddValue("branch", t.BranchID)
			.AddValue("job", t.Job)
			.AddValue("username", t.Username);
	}
}
