using System.Collections.Generic;

namespace DomainObjects {
	public interface IStockMapper {
		public Stock Find(int id);
		public List<Stock> FindFor(Product p);
		public List<Stock> FindReturned(Customer employee);
		public void Insert(Stock stock);
		public void Update(Stock stock);
	}
}
