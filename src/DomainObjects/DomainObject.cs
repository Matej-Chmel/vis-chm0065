using System;

namespace DomainObjects {
	public abstract class DomainObject {
		int id;
		bool inserted;

		DomainObject(int id, bool inserted) {
			this.id = id;
			this.inserted = inserted;
			Mappers = MapperRegistry.GetInstance();
		}

		protected DomainObject() : this(0, false) {}
		protected DomainObject(int id) : this(id, true) {}
		protected MapperRegistry Mappers;

		public int ID {
			get => id;
			set {
				if(inserted)
					throw new Exception("Cannot insert twice.");

				id = value;
				inserted = true;
			}
		}
	}
}
