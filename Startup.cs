using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Cupcakes.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Cupcakes.Repositories;

namespace Cupcakes
{
	public class Startup
	{
		private IConfiguration _configuration;
		public Startup(IConfiguration configuration)
		{
			_configuration = configuration;
		}
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddTransient<ICupcakeRepository, CupcakeRepository>();
			services.AddDbContext<CupcakeContext>(options =>
				options.UseSqlServer(_configuration.GetConnectionString("DefaultConnection")));
			services.AddHttpContextAccessor();
			services.AddControllersWithViews();
			services.AddRazorPages();
		}
		public void Configure(IApplicationBuilder app, CupcakeContext cupcakeContext)
		{
			cupcakeContext.Database.EnsureDeleted();///error
			cupcakeContext.Database.EnsureCreated();
			app.UseStaticFiles();
			app.UseRouting();
			app.UseEndpoints(endpoints => {
				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller=Cupcake}/{action=Index}/{id?}");
				endpoints.MapRazorPages();
			});
		}
	}


}
