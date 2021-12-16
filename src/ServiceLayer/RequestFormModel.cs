using DomainObjects;

namespace ServiceLayer {
	public class RequestFormModel : CustomerModel {
		public string Author { get; set; }
		public string Name { get; set; }

		public void AddRequest() {
			var product = new Product() { Author = Author, Name = Name };
			product.Create();
			new Request() { CustomerID = CustomerID, ProductID = product.ID }.Create();
		}

		public RequestFormModel() {}
		public RequestFormModel(int customerID) : base(customerID) {}
	}
}
