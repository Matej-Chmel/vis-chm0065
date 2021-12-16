namespace DomainObjects {
	public class Penalty : DomainObject {
		public int Amount { get; set; }
		public Borrowing Borrowing => Mappers.BorrowingMapper.Find(BorrowingID);
		public int BorrowingID { get; set; }
		public Customer Customer => Mappers.CustomerMapper.Find(CustomerID);
		public int CustomerID { get; set; }
		public string Description { get; set; }
		public bool Paid { get; set; } = false;

		public void Create() => Mappers.PenaltyMapper.Insert(this);

		public void CreateForDamage() {
			Amount = 50;
			Create();
		}

		public void CreateForLosing() {
			Amount = 500;
			Description = "Ztraceno.";
			Create();
		}

		public void CreateForReturningLate() {
			Amount = 10;
			Description = "Vráceno pozdě.";
			Create();
		}

		public void Pay() {
			Paid = true;
			Mappers.PenaltyMapper.Update(this);
		}
	}
}
