using System;
using System.Xml.Linq;

namespace XMLGateway {
	public static class Extensions {
		public static int ElementID(this XElement e) => e.ElementIntAttr("id");
		public static int ElementIntAttr(this XElement e, string name) => Convert.ToInt32(e.Attribute(name).Value);
	}
}
