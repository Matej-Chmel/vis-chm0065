using System;
using System.Collections.Generic;
using DomainObjects;
using MySql.Data.MySqlClient;

namespace MariaDBMappers {
	public class OrderMapper : Mapper<Order>, IOrderMapper {
		const string ORDER_CAST_ERROR = "Unable to cast Order as ExportOrder or ImportOrder.";

		readonly ExportOrderMapper exportOrderMapper = new();
		readonly ImportOrderMapper importOrderMapper = new();

		public Order FindActiveFor(Customer customer) {
			var orders = FindCustom($"WHERE `customer` = {customer.ID} AND `confirmed` = FALSE");

			return orders.Count == 0
				? null
				: orders.Count == 1
				? orders[0]
				: throw new Exception($"Customer {customer.Username} has multiple opened orders.");
		}

		public Order FindFor(OrderItem item) => Find(item.OrderID);

		public List<Order> FindProcessed() => FindCustom($@"
			WHERE
				`type` = {"Export".Field()} AND
				`completed` = FALSE AND
				`confirmed` = TRUE AND
				id NOT IN (
					SELECT `order`
					FROM `order_item`
					WHERE `ready` = FALSE
				)
		");

		public List<Order> FindUnpaid(Customer customer) => FindCustom($@"
			WHERE
				`type` = {"Export".Field()} AND
				`customer` = {customer.ID} AND
				`confirmed` = TRUE AND
				`online` = TRUE AND
				`paid` = FALSE
		");

		public override Order FromReader(MySqlDataReader r, int id) {
			var type = Convert.ToString(r["type"]);

			return type == "Import"
				? importOrderMapper.FromReader(r, id)
				: type == "Export"
				? exportOrderMapper.FromReader(r, id)
				: throw new Exception($"Order type {type} is not implemented.");
		}

		public void Insert(Order o) {
			switch(o) {
				case ExportOrder exportOrder:
					exportOrderMapper.Insert(exportOrder);
					break;
				case ImportOrder importOrder:
					importOrderMapper.Insert(importOrder);
					break;
				default:
					throw new Exception(ORDER_CAST_ERROR);
			}
		}

		public OrderMapper() : base("Order") {}

		public void Update(Order o) {
			switch(o) {
				case ExportOrder exportOrder:
					exportOrderMapper.Update(exportOrder);
					break;
				case ImportOrder importOrder:
					importOrderMapper.Update(importOrder);
					break;
				default:
					throw new Exception(ORDER_CAST_ERROR);
			}
		}
	}
}
