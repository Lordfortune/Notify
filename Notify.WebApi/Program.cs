using System;
using System.IO;
using System.Threading.Tasks;
using Autofac.Extensions.DependencyInjection;
using FT.Extending;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Notify.Dal.Mysql;

namespace Notify.WebApi
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			var host = CreateWebHostBuilder(args).Build();

			using (var scope = host.Services.CreateScope())
			{
				try
				{
					var context = scope.ServiceProvider.GetRequiredService<NotifyDbContext>();
					context.Database.Migrate();
				}
				catch (Exception ex)
				{
					Console.WriteLine($"Error on migration: {ex.GetFullTextWithInner()}");
				}
			}

			host.Run();

			while (true)
			{
				await Task.Delay(30000);
			}
		}

		public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
			 WebHost.CreateDefaultBuilder(args)
				 .ConfigureServices(services => services.AddAutofac())
				 .UseContentRoot(Directory.GetCurrentDirectory())
				 .UseStartup<Startup>();
	}
}
