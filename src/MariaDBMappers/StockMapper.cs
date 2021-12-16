using System;
using System.Collections.Generic;
using DomainObjects;
using MySql.Data.MySqlClient;

namespace MariaDBMappers {
	public class StockMapper : Mapper<Stock>, IStockMapper {
		public List<Stock> FindFor(Product p) => FindCustom(@$"
			WHERE
				`copy` = FALSE AND
				`product` = {p.ID} AND (
					`borrowing` IS NOT NULL OR
					`location` IS NOT NULL
			);
		");

		public List<Stock> FindReturned(Customer employee) => FindCustom($@"
			WHERE
				`branch` = {employee.BranchID} AND
				`location` IS NULL AND
				`reserved` = TRUE
		");

		public override Stock FromReader(MySqlDataReader r, int id) => new() {
			ID = id,
			BorrowingID = r.IsDBNull(r.GetOrdinal("borrowing")) ? null : Convert.ToInt32(r["borrowing"]),
			BranchID = r.IsDBNull(r.GetOrdinal("branch")) ? null : Convert.ToInt32(r["branch"]),
			Copy = Convert.ToBoolean(r["copy"]),
			Location = r.IsDBNull(r.GetOrdinal("location")) ? null : Convert.ToString(r["location"]),
			ProductID = Convert.ToInt32(r["product"]),
			Reserved = Convert.ToBoolean(r["reserved"])
		};

		public void Insert(Stock stock) => Insert(
			stock,
			"`branch`, `copy`, `location`, `product`",
			$"{stock.BranchID?.ToString() ?? "NULL"}, {stock.Copy}, {stock.Location ?? "NULL"}, {stock.ProductID}"
		);

		public StockMapper() : base("Stock") {}

		public void Update(Stock stock) => Update(
			stock, @$"
				`borrowing` = {stock.BorrowingID?.ToString() ?? "NULL"},
				`branch` = {stock.BranchID?.ToString() ?? "NULL"},
				`deleted` = {stock.Deleted},
				`location` = {stock.Location?.Field() ?? "NULL"},
				`reserved` = {stock.Reserved}
		");
	}
}
