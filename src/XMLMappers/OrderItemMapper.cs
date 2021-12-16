using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using DomainObjects;

namespace XMLMappers {
	public class OrderItemMapper : Mapper<OrderItem>, IOrderItemMapper {
		List<OrderItem> FromOrders(IEnumerable<Order> orders, Customer employee) {
			var res = new List<OrderItem>();

			foreach(var o in orders)
				res.AddRange(Mappers.OrderItemMapper.FindForOrder(o).Where(i => !i.Ready && i.Stock.BranchID == employee.BranchID));

			return res;
		}

		public List<OrderItem> FindConfirmedFor(Customer customer) {
			var orders = Mappers.OrderMapper.FindAll().Where(
				o => o is ExportOrder && o.CustomerID == customer.ID && o.Confirmed && !o.Completed
			);
			var res = new List<OrderItem>();

			foreach(var o in orders)
				res.AddRange(o.Items);

			return res;
		}

		public List<OrderItem> FindForExport(Customer employee) => FromOrders(
			Mappers.OrderMapper.FindAll().Where(o => o is ExportOrder && o.Confirmed && (!o.Online || o.Paid)),
			employee
		);

		public List<OrderItem> FindForImport(Customer employee) => FromOrders(
			Mappers.OrderMapper.FindAll().Where(o => o is ImportOrder && o.Confirmed),
			employee
		);

		public List<OrderItem> FindForOrder(Order order) => FindWhere(i => i.OrderID == order.ID);

		public override OrderItem FromElement(XElement e) => new() {
			Copy = e.BoolValue("copy"),
			OrderID = e.IntValue("order"),
			Ready = e.BoolValue("ready"),
			StockID = e.IntValue("stock")
		};

		public OrderItemMapper() : base("orderItem") {}

		public override void ToElement(OrderItem t, XElement e) => e
			.AddValue("copy", t.Copy)
			.AddValue("order", t.OrderID)
			.AddValue("ready", t.Ready)
			.AddValue("stock", t.StockID);
	}
}
