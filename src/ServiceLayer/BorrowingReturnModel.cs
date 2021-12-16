using System.Collections.Generic;
using System.Linq;
using DomainObjects;

namespace ServiceLayer {
	public class BorrowingReturnModel : EmployeeModel {
		List<Customer> customersWithBorrowing;

		public int ActiveCustomerID { get; set; }
		public int BorrowingID { get; set; }
		public string DamageDescription { get; set; }

		public Customer ActiveCustomer() => Mappers().CustomerMapper.Find(ActiveCustomerID);
		public void AddDamage() => Borrowing().AddDamage(DamageDescription);
		public Borrowing Borrowing() => Mappers().BorrowingMapper.Find(BorrowingID);
		public bool BorrowingChosen() => BorrowingID != 0;
		public BorrowingReturnModel() {}
		public BorrowingReturnModel(int employeeID) : base(employeeID) {}
		public BorrowingReturnModel(int employeeID, int customerID) : base(employeeID) => SetActiveCustomerID(customerID);

		public BorrowingReturnModel(int employeeID, int customerID, int borrowingID) : base(employeeID) {
			SetActiveCustomerID(customerID);
			BorrowingID = borrowingID;
		}

		public List<Borrowing> Borrowings() => Mappers().BorrowingMapper.FindFor(ActiveCustomer());
		public bool CustomerActive() => ActiveCustomerID != 0;

		public List<Customer> CustomersWithBorrowing() {
			if(customersWithBorrowing is null)
				customersWithBorrowing = Mappers().CustomerMapper.FindWithBorrowing();
			return customersWithBorrowing;
		}

		public List<string> DamageOptions() => new() { "Ohnuté rohy.", "Poničená vazba.", "Popsané strany.", "Znečišteno." };
		public bool Empty() => CustomersWithBorrowing().Count == 0;
		public void Lose() => Borrowing().Lose();
		public List<Penalty> Penalties() => Mappers().PenaltyMapper.FindFor(Borrowing());
		public void ReturnToWarehouse() => Borrowing().ReturnToWarehouse(Employee().Branch);
		public void SetActiveCustomerID(int customerID) => ActiveCustomerID = CustomersWithBorrowing().Any(c => c.ID == customerID) ? customerID : 0;
	}
}
