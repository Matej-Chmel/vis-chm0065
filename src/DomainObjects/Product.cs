namespace DomainObjects {
	public class Product : DomainObject {
		public string Author { get; set; }
		public string Filename { get; set; }
		public string Name { get; set; }

		public void Create() => Mappers.ProductMapper.Insert(this);

		public void FileUploaded(string filename) {
			Filename = filename;
			Mappers.ProductMapper.Update(this);
		}

		public string FullName => $"{Author} - {Name}";
		public bool HasFile => Filename is not null;
	}
}
