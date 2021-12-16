namespace ServiceLayer {
	public class AddressModel : CustomerModel {
		public string Address { get; set; }

		public AddressModel() {}
		public AddressModel(int customerID) : base(customerID) {}
		public void ChangeAddress() => Customer().ChangeAddress(Address);
	}
}
