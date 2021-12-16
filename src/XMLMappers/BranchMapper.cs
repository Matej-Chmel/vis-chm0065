using System.Xml.Linq;
using DomainObjects;

namespace XMLMappers {
	public class BranchMapper : Mapper<Branch>, IBranchMapper {
		public BranchMapper() : base("branch") {}
		public override Branch FromElement(XElement e) => new();
		public override void ToElement(Branch t, XElement e) {}
	}
}
