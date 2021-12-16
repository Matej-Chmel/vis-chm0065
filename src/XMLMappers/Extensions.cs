using System;
using System.Xml.Linq;
using XMLGateway;

namespace XMLMappers {
	public static class Extensions {
		public static XElement AddValue(this XElement e, string name, bool value) {
			e.Add(new XElement(name, value));
			return e;
		}

		public static XElement AddValue(this XElement e, string name, int value) {
			e.Add(new XElement(name, value));
			return e;
		}

		public static XElement AddValue(this XElement e, string name, int? value) {
			if(value.HasValue)
				e.Add(new XElement(name, value));
			return e;
		}

		public static XElement AddValue(this XElement e, string name, string value) {
			if(value is not null)
				e.Add(new XElement(name, value));
			return e;
		}

		public static bool BoolValue(this XElement e, string name) => Convert.ToBoolean(e.Element(name).Value);
		public static int ID(this XElement e) => e.ElementID();
		public static int IntAttr(this XElement e, string name) => e.ElementIntAttr(name);

		public static int? IntNullableValue(this XElement e, string name) {
			var child = e.Element(name);
			return child is null ? null : Convert.ToInt32(child.Value);
		}

		public static int IntValue(this XElement e, string name) => Convert.ToInt32(e.Element(name).Value);
		public static string StringValue(this XElement e, string name) => e.Element(name)?.Value;
	}
}
