using DomainObjects;
using MySql.Data.MySqlClient;

namespace MariaDBMappers {
	public class BranchMapper : Mapper<Branch>, IBranchMapper {
		public BranchMapper() : base("Branch") {}
		public override Branch FromReader(MySqlDataReader r, int id) => new(id);
	}
}
