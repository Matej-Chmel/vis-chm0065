using DomainObjects;

namespace ServiceLayer {
	public class StartupService {
		static StartupService instance;

		public bool MariaDBActive { get; private set; }

		public static StartupService GetInstance() {
			if(instance is null)
				instance = new StartupService();
			return instance;
		}

		public void ChooseMariaDB() {
			MariaDBActive = true;
			Mappers.BorrowingMapper = new MariaDBMappers.BorrowingMapper();
			Mappers.BranchMapper = new MariaDBMappers.BranchMapper();
			Mappers.CustomerMapper = new MariaDBMappers.CustomerMapper();
			Mappers.OrderItemMapper = new MariaDBMappers.OrderItemMapper();
			Mappers.OrderMapper = new MariaDBMappers.OrderMapper();
			Mappers.PenaltyMapper = new MariaDBMappers.PenaltyMapper();
			Mappers.ProductMapper = new MariaDBMappers.ProductMapper();
			Mappers.RequestMapper = new MariaDBMappers.RequestMapper();
			Mappers.StockMapper = new MariaDBMappers.StockMapper();
		}

		public void ChooseXML() {
			MariaDBActive = false;
			Mappers.BorrowingMapper = new XMLMappers.BorrowingMapper();
			Mappers.BranchMapper = new XMLMappers.BranchMapper();
			Mappers.CustomerMapper = new XMLMappers.CustomerMapper();
			Mappers.OrderItemMapper = new XMLMappers.OrderItemMapper();
			Mappers.OrderMapper = new XMLMappers.OrderMapper();
			Mappers.PenaltyMapper = new XMLMappers.PenaltyMapper();
			Mappers.ProductMapper = new XMLMappers.ProductMapper();
			Mappers.RequestMapper = new XMLMappers.RequestMapper();
			Mappers.StockMapper = new XMLMappers.StockMapper();
		}

		public void FlipStorage() {
			MariaDBActive = !MariaDBActive;

			if(MariaDBActive)
				ChooseMariaDB();
			else
				ChooseXML();
		}

		public MapperRegistry Mappers => MapperRegistry.GetInstance();
		public void SetXMLDir(string dir) => XMLMappers.Init.SetPath(dir);
		StartupService() {}
	}
}
