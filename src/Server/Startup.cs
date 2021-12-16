using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ServiceLayer;

namespace Server {
	public class Startup {
		public IConfiguration Configuration { get; }

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
			if(env.IsDevelopment())
				app.UseDeveloperExceptionPage();
			else
				app.UseExceptionHandler("/Home/Error");

			app.UseStaticFiles();
			app.UseRouting();
			app.UseAuthorization();

			app.UseEndpoints(endpoints => endpoints.MapControllerRoute(
				name: "default", pattern: "{controller=Home}/{action=Menu}/{id?}"
			));

			var service = StartupService.GetInstance();
			service.SetXMLDir(Path.GetFullPath(Path.Combine(env.ContentRootPath, @"..\..\databases\XML")));
			service.ChooseXML();
		}

		public void ConfigureServices(IServiceCollection services) => services.AddControllersWithViews();
		public Startup(IConfiguration configuration) => Configuration = configuration;
	}
}
