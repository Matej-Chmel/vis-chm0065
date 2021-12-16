namespace MariaDBMappers {
	public static class Extensions {
		public static string Column(this string s) => '`' + s + '`';
		public static string Field(this string s) => '"' + s + '"';
	}
}
