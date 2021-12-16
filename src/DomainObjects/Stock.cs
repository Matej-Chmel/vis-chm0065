namespace DomainObjects {
	public class Stock : DomainObject {
		public int? BorrowingID { get; set; }
		public Branch Branch => BranchID is null ? null : Mappers.BranchMapper.Find((int)BranchID);
		public int? BranchID { get; set; }
		public bool Copy { get; set; } = false;
		public bool Deleted { get; set; } = false;
		public string Location { get; set; }
		public Product Product => Mappers.ProductMapper.Find(ProductID);
		public int ProductID { get; set; }
		public bool Reserved { get; set; } = false;

		public bool Available => BorrowingID is null && !Reserved;

		public void Borrow(Borrowing b) {
			BorrowingID = b.ID;
			BranchID = null;
			Location = null;
			Mappers.StockMapper.Update(this);
		}

		public bool CatalogReady => !Copy && (BorrowingID.HasValue || Location is not null);

		public void ChangeLocation(string location) {
			Location = location;
			Mappers.StockMapper.Update(this);
		}

		public void Create() {
			Mappers.StockMapper.Insert(this);

			var request = Mappers.RequestMapper.FindFor(Product);

			if(request is not null)
				request.Accept();
		}

		public Stock CreateCopy() {
			var copy = new Stock((int)BranchID, ProductID) { Copy = true };
			copy.Create();

			if(Reserved)
				SetReserved(false);

			return copy;
		}

		public void DenyImport() {
			Deleted = true;
			Mappers.StockMapper.Update(this);
		}

		public bool Returned(Branch branch) => BranchID == branch.ID && Location is null && Reserved;

		public void ReturnToLocation(string location) {
			Location = location;
			Reserved = false;
			Mappers.StockMapper.Update(this);
		}

		public void ReturnToWarehouse(Branch branch) {
			BorrowingID = null;
			BranchID = branch.ID;
			Reserved = true;
			Mappers.StockMapper.Update(this);
		}

		public void SetReserved(bool reserved) {
			Reserved = reserved;
			Mappers.StockMapper.Update(this);
		}

		public Stock() {}

		public Stock(int branchID, int productID) {
			BranchID = branchID;
			ProductID = productID;
		}
	}
}
