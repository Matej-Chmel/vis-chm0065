namespace ServiceLayer {
	public class LimitReachedModel : CustomerModel {
		public int ReservationCount { get; set; }

		public LimitReachedModel() {}
		public LimitReachedModel(int customerID, int reservationCount) : base(customerID) => ReservationCount = reservationCount;
	}
}
