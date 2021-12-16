using System.Collections.Generic;
using DomainObjects;

namespace ServiceLayer {
	public class RequestsModel : ServiceModel {
		List<Request> requests;

		public int CustomerID { get; set; } = -1;

		public Customer Customer() => Mappers().CustomerMapper.Find(CustomerID);
		public bool Empty() => Requests().Count == 0;
		public bool LoggedIn() => CustomerID > 0;

		public List<Request> Requests() {
			if(requests is null)
				requests = Mappers().RequestMapper.FindAll();
			return requests;
		}

		public RequestsModel() {}
		public RequestsModel(int customerID) => CustomerID = customerID;
	}
}
