using DomainObjects;

namespace ServiceLayer {
	public class PenaltyPaymentModel : CustomerModel {
		public int PenaltyID { get; set; }

		public Penalty Penalty() => Mappers().PenaltyMapper.Find(PenaltyID);
		public PenaltyPaymentModel() {}
		public PenaltyPaymentModel(int penaltyID, int customerID) : base(customerID) => PenaltyID = penaltyID;

		public bool ProcessResult(bool success) {
			if(success)
				Penalty().Pay();
			return success;
		}
	}
}
