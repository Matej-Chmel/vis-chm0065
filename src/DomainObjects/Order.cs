using System;
using System.Collections.Generic;

namespace DomainObjects {
	public abstract class Order : DomainObject {
		public virtual int Amount {
			get => throw new NotImplementedException();
			set => throw new NotImplementedException();
		}

		public virtual bool Completed {
			get => throw new NotImplementedException();
			set => throw new NotImplementedException();
		}

		public bool Confirmed { get; set; }
		public Customer Customer => Mappers.CustomerMapper.Find(CustomerID);

		public virtual int CustomerID {
			get => throw new NotImplementedException();
			set => throw new NotImplementedException();
		}

		public virtual bool HasDelivery {
			get => throw new NotImplementedException();
			set => throw new NotImplementedException();
		}

		public virtual bool Online {
			get => throw new NotImplementedException();
			set => throw new NotImplementedException();
		}

		public virtual bool Paid {
			get => throw new NotImplementedException();
			set => throw new NotImplementedException();
		}

		public List<OrderItem> Items => Mappers.OrderItemMapper.FindForOrder(this);

		public virtual void Add(OrderItem item) {
			item.OrderID = ID;
			Mappers.OrderItemMapper.Insert(item);
		}

		public void Confirm() {
			Confirmed = true;
			Mappers.OrderMapper.Update(this);
		}

		public virtual void ChangeDelivery(bool hasDelivery) => throw new NotImplementedException();
		public virtual void ChangePaymentMethod(bool online) => throw new NotImplementedException();
		public virtual void Complete() => throw new NotImplementedException();
		public void Create() => Mappers.OrderMapper.Insert(this);
		public virtual void Delete() => throw new NotImplementedException();
		public virtual void Delete(OrderItem item) => throw new NotImplementedException();
		public virtual void Pay() => throw new NotImplementedException();
		public virtual bool PickUpPossible() => throw new NotImplementedException();
	}
}
