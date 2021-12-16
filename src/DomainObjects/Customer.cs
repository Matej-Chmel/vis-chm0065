namespace DomainObjects {
	public class Customer : DomainObject {
		public string Address { get; set; }
		public Branch Branch => BranchID is null ? null : Mappers.BranchMapper.Find((int)BranchID);
		public int? BranchID { get; set; }
		public string Job { get; set; }
		public string Username { get; set; }

		public bool AddressMissing => Address is null;

		public void ChangeAddress(string address) {
			Address = address;
			Mappers.CustomerMapper.Update(this);
		}
	}
}
