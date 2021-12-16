namespace DomainObjects {
	public class Request : DomainObject {
		public bool Accepted { get; set; }
		public Customer Customer => Mappers.CustomerMapper.Find(CustomerID);
		public int CustomerID { get; set; }
		public Product Product => Mappers.ProductMapper.Find(ProductID);
		public int ProductID { get; set; }

		public void Accept() {
			Accepted = true;
			Mappers.RequestMapper.Update(this);
		}

		public void Create() => Mappers.RequestMapper.Insert(this);
	}
}
