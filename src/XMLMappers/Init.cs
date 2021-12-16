using XMLGateway;

namespace XMLMappers {
	public class Init {
		public static void SetPath(string path) => Database.GetInstance().DirPath = path;
	}
}
