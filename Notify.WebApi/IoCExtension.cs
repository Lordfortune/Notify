using Autofac;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Notify.Bll;
using Notify.Dal.Mysql;
using Notify.Dal.Repositories;

namespace Notify.WebApi
{
	public static class IoCExtension
	{
		public static IServiceCollection ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddDbContext<NotifyDbContext>(o =>
				o.UseMySql(configuration.GetConnectionString("NotifyConnectionString"), builder =>
				{
					builder.MigrationsAssembly("Notify.Dal.MySql.Migrations");
				}));

			return services;
		}

		public static ContainerBuilder ConfigureIoC(this ContainerBuilder builder, IConfiguration configuration)
		{
			builder.RegisterType<NotifyListenersManager>()
				.As<NotifyListenersManager>()
				.As<IHostedService>()
				.SingleInstance();

			builder.RegisterType<NotificationTypeRepository>().As<INotificatorTypeRepository>().InstancePerLifetimeScope();
			builder.RegisterType<NotificatorRepository>().As<INotificatorRepository>().InstancePerLifetimeScope();
			
			return builder;
		}
	}
}
