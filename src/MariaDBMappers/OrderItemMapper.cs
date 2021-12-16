using System;
using System.Collections.Generic;
using DomainObjects;
using MySql.Data.MySqlClient;

namespace MariaDBMappers {
	public class OrderItemMapper : Mapper<OrderItem>, IOrderItemMapper {
		public List<OrderItem> FindConfirmedFor(Customer customer) => FindComplex($@"
			SELECT i.*
			FROM `order_item` i
			JOIN `order` o ON i.`order` = o.id
			WHERE
				o.`confirmed` = TRUE AND
				o.`completed` = FALSE AND
				o.`customer` = {customer.ID}
		");

		public List<OrderItem> FindForExport(Customer employee) => FindComplex($@"
			SELECT i.*
			FROM `order_item` i
			JOIN `order` o ON i.`order` = o.id
			JOIN `stock` s ON i.`stock` = s.id
			WHERE
				o.`type` = {"Export".Field()} AND
				o.`confirmed` = TRUE AND (
					o.`online` = FALSE OR
					o.`paid` = TRUE
				) AND
				i.`ready` = FALSE AND
				s.`branch` = {employee.BranchID}
		");

		public List<OrderItem> FindForImport(Customer employee) => FindComplex($@"
			SELECT i.*
			FROM `order_item` i
			JOIN `order` o ON i.`order` = o.id
			JOIN `stock` s ON i.`stock` = s.id
			WHERE
				o.`type`= {"Import".Field()} AND
				o.`confirmed` = TRUE AND
				i.`ready` = FALSE AND
				s.`branch` = {employee.BranchID}
		");

		public List<OrderItem> FindForOrder(Order order) => FindCustom($"WHERE `order` = {order.ID}");

		public override OrderItem FromReader(MySqlDataReader r, int id) => new() {
			ID = id,
			Copy = Convert.ToBoolean(r["copy"]),
			OrderID = Convert.ToInt32(r["order"]),
			Ready = Convert.ToBoolean(r["ready"]),
			StockID = Convert.ToInt32(r["stock"])
		};

		public void Insert(OrderItem item) => Insert(
			item,
			"`copy`, `order`, `ready`, `stock`",
			$"{item.Copy}, {item.OrderID}, {item.Ready}, {item.StockID}"
		);

		public OrderItemMapper() : base("Order_Item") { }
		public void Update(OrderItem item) => Update(item, $"`ready` = {item.Ready}");
	}
}
