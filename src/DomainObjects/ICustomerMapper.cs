using System.Collections.Generic;

namespace DomainObjects {
	public interface ICustomerMapper {
		public Customer Find(int id);
		public List<Customer> FindAll();
		public List<Customer> FindAllCashiers();
		public List<Customer> FindAllWarehousemen();
		public List<Customer> FindWithBorrowing();
		public void Update(Customer customer);
	}
}
