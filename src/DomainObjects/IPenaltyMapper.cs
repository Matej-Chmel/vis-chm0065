using System.Collections.Generic;

namespace DomainObjects {
	public interface IPenaltyMapper {
		public void DeleteFor(Borrowing b);
		public Penalty Find(int id);
		public List<Penalty> FindFor(Borrowing b);
		public List<Penalty> FindFor(Customer c);
		public void Insert(Penalty p);
		public void Update(Penalty p);
	}
}
