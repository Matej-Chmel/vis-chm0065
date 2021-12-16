namespace DomainObjects {
	public class Borrowing : DomainObject {
		public Customer Customer => Mappers.CustomerMapper.Find(CustomerID);
		public int CustomerID { get; set; }
		public bool Late { get; set; } = false;
		public bool Lost { get; set; } = false;
		public OrderItem OrderItem => Mappers.OrderItemMapper.Find(OrderItemID);
		public int OrderItemID { get; set; }
		public bool Returned { get; set; } = false;

		public Borrowing() {}

		public Borrowing(int customerID, int orderItemID) {
			CustomerID = customerID;
			OrderItemID = orderItemID;
			Late = (OrderItemID & 1) == 0;
		}

		public void AddDamage(string damage) => InitPenalty(damage).CreateForDamage();
		public void Create() => Mappers.BorrowingMapper.Insert(this);

		public Penalty InitPenalty(string description = null) => new() {
			BorrowingID = ID,
			CustomerID = CustomerID,
			Description = description
		};

		public void Lose() {
			Mappers.PenaltyMapper.DeleteFor(this);
			Lost = true;
			Mappers.BorrowingMapper.Update(this);
			InitPenalty().CreateForLosing();
		}

		public void ReturnToWarehouse(Branch branch) {
			Returned = true;
			Mappers.BorrowingMapper.Update(this);
			OrderItem.Stock.ReturnToWarehouse(branch);

			if(Late)
				InitPenalty().CreateForReturningLate();
		}

		public bool Waiting => !Lost && !Returned;
	}
}
