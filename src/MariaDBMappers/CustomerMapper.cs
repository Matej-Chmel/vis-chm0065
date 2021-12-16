using System;
using System.Collections.Generic;
using DomainObjects;
using MySql.Data.MySqlClient;

namespace MariaDBMappers {
	public class CustomerMapper : Mapper<Customer>, ICustomerMapper {
		public CustomerMapper() : base("Customer") {}
		public List<Customer> FindAllCashiers() => FindCustom($@"WHERE `job` = {"Pokladní".Field()}");
		public List<Customer> FindAllWarehousemen() => FindCustom($@"WHERE `job` = {"Skladník".Field()}");

		public List<Customer> FindWithBorrowing() => FindComplex($@"
			SELECT DISTINCT c.*
			FROM `customer` c
			JOIN `borrowing` b ON b.`customer` = c.id
			WHERE
				b.`lost` = FALSE AND
				b.`returned` = FALSE
		");

		public override Customer FromReader(MySqlDataReader r, int id) => new() {
			ID = id,
			Address = r.IsDBNull(r.GetOrdinal("address")) ? null : Convert.ToString(r["address"]),
			BranchID = r.IsDBNull(r.GetOrdinal("branch")) ? null : Convert.ToInt32(r["branch"]),
			Job = r.IsDBNull(r.GetOrdinal("job")) ? null : Convert.ToString(r["job"]),
			Username = Convert.ToString(r["username"])
		};

		public void Update(Customer customer) => Update(customer, $"`address` = {customer.Address.Field()}");
	}
}
