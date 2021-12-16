using System.Collections.Generic;

namespace DomainObjects {
	public interface IOrderMapper {
		public void Delete(Order o);
		public Order Find(int id);
		public Order FindActiveFor(Customer customer);
		public List<Order> FindAll();
		public Order FindFor(OrderItem item);
		public List<Order> FindProcessed();
		public List<Order> FindUnpaid(Customer customer);
		public void Insert(Order o);
		public void Update(Order o);
	}
}
