using System;
using System.Collections.Generic;
using DomainObjects;
using MySql.Data.MySqlClient;

namespace MariaDBMappers {
	public class PenaltyMapper : Mapper<Penalty>, IPenaltyMapper {
		public void DeleteFor(Borrowing b) => DeleteCustom($"WHERE `borrowing` = {b.ID}");
		public List<Penalty> FindFor(Borrowing b) => FindCustom($"WHERE `borrowing` = {b.ID}");
		public List<Penalty> FindFor(Customer c) => FindCustom($"WHERE `customer` = {c.ID} AND `paid` = FALSE");

		public override Penalty FromReader(MySqlDataReader r, int id) => new() {
			ID = id,
			Amount = Convert.ToInt32(r["amount"]),
			BorrowingID = Convert.ToInt32(r["borrowing"]),
			CustomerID = Convert.ToInt32(r["customer"]),
			Description = Convert.ToString(r["description"]),
			Paid = Convert.ToBoolean(r["paid"])
		};

		public void Insert(Penalty p) => Insert(
			p,
			"`amount`, `borrowing`, `customer`, `description`",
			$"{p.Amount}, {p.BorrowingID}, {p.CustomerID}, {p.Description.Field()}"
		);

		public PenaltyMapper() : base("Penalty") {}
		public void Update(Penalty p) => Update(p, $"`paid` = {p.Paid}");
	}
}
