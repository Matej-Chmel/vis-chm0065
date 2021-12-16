using System.Data;
using MySql.Data.MySqlClient;

namespace MariaDBGateway {
	class Connection {
		static readonly string connStr = "server=localhost;userid=root;database=VIS;password=VISchm0065DB;charset=utf8";

		readonly MySqlConnection conn;

		~Connection() {
			conn.Close();
		}

		public MySqlCommand Command(string query) {
			var cmd = conn.CreateCommand();
			cmd.CommandType = CommandType.Text;
			cmd.CommandTimeout = 300;
			cmd.CommandText = query;
			return cmd;
		}

		public Connection() {
			conn = new MySqlConnection(connStr);
			conn.Open();
		}
	}
}
