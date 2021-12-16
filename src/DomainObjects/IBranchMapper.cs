using System.Collections.Generic;

namespace DomainObjects {
	public interface IBranchMapper {
		public Branch Find(int id);
		public List<Branch> FindAll();
	}
}
