using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Notify.Dal.Mysql;
using Notify.Dal.Repositories;

namespace Notify.WebApi
{
	public static class IoCExtension
	{
		public static IServiceCollection ConfigureIoC(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddDbContext<NotifyDbContext>(o => 
				o.UseMySql(configuration.GetConnectionString("NotifyConnectionString"), builder =>
				{
					builder.MigrationsAssembly("Notify.Dal.MySql.Migrations");
				}));

			services
				.AddScoped<INotificatorTypeRepository, NotificationTypeRepository>()
				.AddScoped<INotificatorRepository, NotificatorRepository>();

			return services;
		}
	}
}
