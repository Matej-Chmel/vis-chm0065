using System.Collections.Generic;
using DomainObjects;

namespace ServiceLayer {
	public class CatalogModel : CustomerModel {
		public int ProductID { get; set; }

		public CatalogModel() {}
		public CatalogModel(int customerID) : base(customerID) {}
		public List<Product> Products() => Mappers().ProductMapper.FindForCatalog();
	}
}
