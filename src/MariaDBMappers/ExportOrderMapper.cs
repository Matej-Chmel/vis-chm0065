using System;
using DomainObjects;
using MySql.Data.MySqlClient;

namespace MariaDBMappers {
	public class ExportOrderMapper : Mapper<ExportOrder> {
		public ExportOrderMapper() : base("order") {}

		public override ExportOrder FromReader(MySqlDataReader r, int id) => new() {
			ID = id,
			Amount = Convert.ToInt32(r["amount"]),
			Completed = Convert.ToBoolean(r["completed"]),
			Confirmed = Convert.ToBoolean(r["confirmed"]),
			CustomerID = Convert.ToInt32(r["customer"]),
			HasDelivery = Convert.ToBoolean(r["has_delivery"]),
			Online = Convert.ToBoolean(r["online"]),
			Paid = Convert.ToBoolean(r["paid"])
		};

		public void Insert(ExportOrder o) => Insert(
			o,
			"`amount`, `completed`, `customer`, `has_delivery`, `online`, `paid`, `type`",
			$"0, {o.Completed}, {o.CustomerID}, {o.HasDelivery}, FALSE, FALSE, {"Export".Field()}"
		);

		public void Update(ExportOrder o) => Update(
			o,
			$"`amount` = {o.Amount}, `completed` = {o.Completed}, `confirmed` = {o.Confirmed}, `has_delivery` = {o.HasDelivery}, `online` = {o.Online}, `paid` = {o.Paid}"
		);
	}
}
