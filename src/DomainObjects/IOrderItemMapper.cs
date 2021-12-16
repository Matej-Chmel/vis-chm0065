using System.Collections.Generic;

namespace DomainObjects {
	public interface IOrderItemMapper {
		public void Delete(OrderItem item);
		public OrderItem Find(int id);
		public List<OrderItem> FindConfirmedFor(Customer customer);
		public List<OrderItem> FindForExport(Customer employee);
		public List<OrderItem> FindForImport(Customer employee);
		public List<OrderItem> FindForOrder(Order order);
		public void Insert(OrderItem item);
		public void Update(OrderItem item);
	}
}
