using System.Collections.Generic;

namespace DomainObjects {
	public interface IBorrowingMapper {
		public Borrowing Find(int id);
		public List<Borrowing> FindAll();
		public List<Borrowing> FindFor(Customer customer);
		public void Insert(Borrowing b);
		public void Update(Borrowing b);
	}
}
