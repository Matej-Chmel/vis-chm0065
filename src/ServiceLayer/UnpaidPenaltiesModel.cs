using System.Collections.Generic;
using DomainObjects;

namespace ServiceLayer {
	public class UnpaidPenaltiesModel : CustomerModel {
		List<Penalty> penalties;

		public bool FromDetail { get; set; }

		public bool Empty() => Penalties().Count == 0;

		public List<Penalty> Penalties() {
			if(penalties is null)
				penalties = Mappers().PenaltyMapper.FindFor(Customer());
			return penalties;
		}

		public UnpaidPenaltiesModel() {}
		public UnpaidPenaltiesModel(int customerID) : this(customerID, false) {}
		public UnpaidPenaltiesModel(int customerID, bool fromDetail) : base(customerID) => FromDetail = fromDetail;
	}
}
