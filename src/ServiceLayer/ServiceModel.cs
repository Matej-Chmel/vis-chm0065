using DomainObjects;

namespace ServiceLayer {
	public class ServiceModel {
		protected MapperRegistry Mappers() => StartupService.GetInstance().Mappers;
		public bool MariaDBActive() => StartupService.GetInstance().MariaDBActive;
		public ServiceModel() {}
	}
}
