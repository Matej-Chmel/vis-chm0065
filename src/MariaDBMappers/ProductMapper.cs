using System;
using System.Collections.Generic;
using DomainObjects;
using MySql.Data.MySqlClient;

namespace MariaDBMappers {
	public class ProductMapper : Mapper<Product>, IProductMapper {
		public List<Product> FindForCatalog() => FindComplex(@"
			SELECT p.*
			FROM `product` p
			LEFT JOIN `request` r ON r.`product` = p.id
			WHERE
				r.id IS NULL OR
				r.`accepted` = TRUE
		");

		public override Product FromReader(MySqlDataReader r, int id) => new() {
			ID = id,
			Author = Convert.ToString(r["author"]),
			Filename = r.IsDBNull(r.GetOrdinal("filename")) ? null : Convert.ToString(r["filename"]),
			Name = Convert.ToString(r["name"])
		};

		public void Insert(Product p) => Insert(
			p,
			"`author`, `filename`, `name`",
			$"{p.Author.Field()}, {p.Filename?.Field() ?? "NULL"}, {p.Name.Field()}"
		);

		public ProductMapper() : base("Product") {}

		public void Update(Product p) => Update(
			p, $"`author` = {p.Author.Field()}, `filename` = {p.Filename?.Field() ?? "NULL"}, `name` = {p.Name.Field()}"
		);
	}
}
