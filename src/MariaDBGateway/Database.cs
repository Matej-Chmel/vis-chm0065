using MySql.Data.MySqlClient;

namespace MariaDBGateway {
	public class Database {
		static Database instance = null;

		readonly Connection conn = new();

		Database() {}

		public static Database GetInstance() {
			if(instance is null)
				instance = new Database();
			return instance;
		}

		public MySqlDataReader Select(string query) {
			var cmd = conn.Command(query);
			var reader = cmd.ExecuteReader();
			cmd.Dispose();
			return reader;
		}

		public int Statement(string statement) {
			var cmd = conn.Command(statement);
			var rowsAffected = cmd.ExecuteNonQuery();
			cmd.Dispose();
			return rowsAffected;
		}
	}
}
