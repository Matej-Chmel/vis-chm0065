using DomainObjects;
using MySql.Data.MySqlClient;

namespace MariaDBMappers {
	public class ImportOrderMapper : Mapper<ImportOrder> {
		public ImportOrderMapper() : base("Order") {}
		public override ImportOrder FromReader(MySqlDataReader r, int id) => new() { ID = id };
		public void Insert(ImportOrder order) => Insert(order, "type".Column(), "Import".Field());
		public void Update(ImportOrder order) => Update(order, $"`confirmed` = {order.Confirmed}");
	}
}
