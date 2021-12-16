using DomainObjects;

namespace ServiceLayer {
	public class CustomerModel : ServiceModel {
		public int CustomerID { get; set; }

		public Customer Customer() => Mappers().CustomerMapper.Find(CustomerID);
		public CustomerModel() {}
		public CustomerModel(int customerID) => CustomerID = customerID;
	}
}
