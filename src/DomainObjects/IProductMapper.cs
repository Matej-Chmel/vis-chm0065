using System.Collections.Generic;

namespace DomainObjects {
	public interface IProductMapper {
		public Product Find(int id);
		public List<Product> FindAll();
		public List<Product> FindForCatalog();
		public void Insert(Product p);
		public void Update(Product p);
	}
}
