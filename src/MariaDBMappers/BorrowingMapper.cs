using System;
using System.Collections.Generic;
using DomainObjects;
using MySql.Data.MySqlClient;

namespace MariaDBMappers {
	public class BorrowingMapper : Mapper<Borrowing>, IBorrowingMapper {
		public BorrowingMapper() : base("Borrowing") {}

		public List<Borrowing> FindFor(Customer customer) => FindCustom($@"
			WHERE
				`customer` = {customer.ID} AND
				`lost` = FALSE AND
				`returned` = FALSE
		");

		public override Borrowing FromReader(MySqlDataReader r, int id) => new() {
			ID = id,
			CustomerID = Convert.ToInt32(r["customer"]),
			Late = Convert.ToBoolean(r["late"]),
			Lost = Convert.ToBoolean(r["lost"]),
			OrderItemID = Convert.ToInt32(r["order_item"]),
			Returned = Convert.ToBoolean(r["returned"])
		};

		public void Insert(Borrowing b) => Insert(b, "`customer`, `late`, `order_item`", $"{b.CustomerID}, {b.Late}, {b.OrderItemID}");
		public void Update(Borrowing b) => Update(b, $"`late` = {b.Late}, `lost` = {b.Lost}, `returned` = {b.Returned}");
	}
}
