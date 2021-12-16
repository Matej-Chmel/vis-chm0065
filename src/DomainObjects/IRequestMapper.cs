using System.Collections.Generic;

namespace DomainObjects {
	public interface IRequestMapper {
		public List<Request> FindAll();
		public Request FindFor(Product p);
		public void Insert(Request r);
		public void Update(Request r);
	}
}
