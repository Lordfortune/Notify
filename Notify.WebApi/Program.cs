using System;
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
		public static void Main(string[] args)
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
		}

		public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
			 WebHost.CreateDefaultBuilder(args)
				  .UseStartup<Startup>();
	}
}
