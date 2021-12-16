using System.Collections.Generic;
using DomainObjects;

namespace ServiceLayer {
	public class StockReturnModel : EmployeeModel {
		public string Location { get; set; }
		public int StockID { get; set; }

		public bool Empty() => Stocks().Count == 0;
		public Stock First() => Stocks()[0];
		public void ReturnToLocation() => Mappers().StockMapper.Find(StockID).ReturnToLocation(Location);
		public StockReturnModel() {}
		public StockReturnModel(int employeeID) : base(employeeID) {}
		List<Stock> stocks;

		public List<Stock> Stocks() {
			if(stocks is null)
				stocks = Mappers().StockMapper.FindReturned(Employee());
			return stocks;
		}
	}
}
