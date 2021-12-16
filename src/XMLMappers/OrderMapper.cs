using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using DomainObjects;

namespace XMLMappers {
	public class OrderMapper : Mapper<Order>, IOrderMapper {
		readonly ExportOrderMapper exportOrderMapper;
		readonly ImportOrderMapper importOrderMapper;

		public Order FindActiveFor(Customer customer) {
			var orders = FindWhere(o => o is ExportOrder && o.CustomerID == customer.ID && !o.Confirmed);

			return orders.Count == 0
				? null
				: orders.Count == 1
				? orders[0]
				: throw new Exception($"Customer {customer.Username} has multiple opened orders.");
		}

		public Order FindFor(OrderItem item) => Find(item.OrderID);

		public List<Order> FindProcessed() {
			var allOrders = FindAll().Where(o => o is ExportOrder && !o.Completed && o.Confirmed);
			var res = new List<Order>();

			foreach(var o in allOrders) {
				var items = o.Items;

				if(items.All(i => i.Ready))
					res.Add(o);
			}

			return res;
		}

		public List<Order> FindUnpaid(Customer customer) => FindWhere(
			o => o is ExportOrder && o.CustomerID == customer.ID && o.Confirmed && o.Online && !o.Paid
		);

		public override Order FromElement(XElement e) {
			var type = e.StringValue("type");

			return type == "Import"
				? importOrderMapper.FromElement(e)
				: type == "Export"
				? exportOrderMapper.FromElement(e)
				: throw new Exception($"Order type {type} is not implemented.");
		}

		public OrderMapper() : base("order") {
			exportOrderMapper = new ExportOrderMapper();
			importOrderMapper = new ImportOrderMapper();
		}

		public override void ToElement(Order t, XElement e) {
			switch(t) {
				case ExportOrder exportOrder:
					exportOrderMapper.ToElement(exportOrder, e);
					break;
				case ImportOrder importOrder:
					importOrderMapper.ToElement(importOrder, e);
					break;
				default:
					throw new Exception("Unable to cast Order as ExportOrder or ImportOrder.");
			}
		}
	}
}
