using System.Collections.Generic;
using DomainObjects;

namespace ServiceLayer {
	public class DetailModel : CustomerModel {
		public int ProductID { get; set; }

		public DetailModel() {}
		public DetailModel(int customerID, int productID) : base(customerID) => ProductID = productID;
		public bool Empty() => Stocks().Count == 0;
		public Product Product() => Mappers().ProductMapper.Find(ProductID);
		public List<Stock> Stocks() => Mappers().StockMapper.FindFor(Product());
	}
}
