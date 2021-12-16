using DomainObjects;

namespace ServiceLayer {
	public class EmployeeModel : CustomerModel {
		public int EmployeeID {
			get => CustomerID;
			set => CustomerID = value;
		}

		public Customer Employee() => Customer();
		public EmployeeModel() {}
		public EmployeeModel(int employeeID) : base(employeeID) {}
	}
}
