using System;

namespace DomainObjects {
	public class ExportOrder : Order {
		public override int Amount { get; set; } = 0;
		public override bool Completed { get; set; } = false;
		public override int CustomerID { get; set; }
		public override bool HasDelivery { get; set; } = true;
		public override bool Online { get; set; } = false;
		public override bool Paid { get; set; } = false;

		public override void Add(OrderItem item) {
			Amount += item.Price;
			base.Add(item);
			Mappers.OrderMapper.Update(this);
			item.Stock.SetReserved(true);
		}

		public override void ChangeDelivery(bool hasDelivery) {
			HasDelivery = hasDelivery;
			Mappers.OrderMapper.Update(this);
		}

		public override void ChangePaymentMethod(bool online) {
			Online = online;
			Mappers.OrderMapper.Update(this);
		}

		public override void Complete() {
			var customer = Customer;

			foreach(var i in Items)
				i.CreateBorrowing(customer);

			Completed = true;
			Mappers.OrderMapper.Update(this);
		}

		public override void Delete() {
			foreach(var item in Items) {
				item.Stock.SetReserved(false);
				Mappers.OrderItemMapper.Delete(item);
			}

			Mappers.OrderMapper.Delete(this);
		}

		public override void Delete(OrderItem item) {
			Amount -= item.Price;
			item.Stock.SetReserved(false);
			Mappers.OrderMapper.Update(this);
			Mappers.OrderItemMapper.Delete(item);

			if(Items.Count == 0)
				Mappers.OrderMapper.Delete(this);
		}

		public override void Pay() {
			Paid = true;
			Mappers.OrderMapper.Update(this);
		}

		public override bool PickUpPossible() {
			var items = Items;

			if(items.Count == 0)
				throw new Exception("Order contains no items.");

			var branchID = items[0].Branch.ID;

			for(var i = 1; i < items.Count; i++)
				if(items[i].Branch.ID != branchID)
					return false;
			return true;
		}
	}
}
