using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using DomainObjects;

namespace XMLMappers {
	public class ProductMapper : Mapper<Product>, IProductMapper {
		public List<Product> FindForCatalog() {
			var requests = Mappers.RequestMapper.FindAll();
			return FindWhere(p => requests.All(r => r.ProductID != p.ID || r.Accepted));
		}

		public override Product FromElement(XElement e) => new() {
			Author = e.StringValue("author"),
			Filename = e.StringValue("filename"),
			Name = e.StringValue("name")
		};

		public ProductMapper() : base("product") {}

		public override void ToElement(Product t, XElement e) => e
			.AddValue("author", t.Author)
			.AddValue("filename", t.Filename)
			.AddValue("name", t.Name);
	}
}
