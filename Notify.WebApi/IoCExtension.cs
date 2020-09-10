using Autofac;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Notify.Bll;
using Notify.Bll.Interfaces;
using Notify.Bll.Mappers;
using Notify.Bll.Storages;
using Notify.Dal.Models;
using Notify.Dal.Models.Telegram;
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
			builder
				.ConfigureStorages(configuration)
				.ConfigureServices(configuration)
				.ConfigureBll(configuration)
				.ConfigureDal(configuration);

			return builder;
		}

		public static ContainerBuilder ConfigureStorages(this ContainerBuilder builder, IConfiguration configuration)
		{
			builder.RegisterType<FailedRequestStorage>().SingleInstance();
			builder.RegisterType<NotificatorsStorage>().SingleInstance();

			return builder;
		}

		public static ContainerBuilder ConfigureServices(this ContainerBuilder builder, IConfiguration configuration)
		{
			builder.RegisterType<NotificatorsManagerService>()
				.As<NotificatorsManagerService>()
				.As<IHostedService>()
				.SingleInstance();

			builder.RegisterType<RequestProcessingService>()
				.As<RequestProcessingService>()
				.As<IHostedService>()
				.SingleInstance();

			return builder;
		}

		public static ContainerBuilder ConfigureBll(this ContainerBuilder builder, IConfiguration configuration)
		{
			builder.RegisterType<NotificationRequestManager>().As<INotificationRequestManager>().InstancePerLifetimeScope();
			builder.RegisterType<NotificationRequestProcessor>().As<INotificationRequestProcessor>().InstancePerLifetimeScope();

			return builder;
		}

		public static ContainerBuilder ConfigureDal(this ContainerBuilder builder, IConfiguration configuration)
		{
			builder.RegisterType<ClientRepository>().As<IClientRepository>().InstancePerLifetimeScope();
			builder.RegisterType<ContactRepository>().As<IContactRepository>().InstancePerLifetimeScope();
			builder.RegisterType<NotificatorRepository>().As<INotificatorRepository>().InstancePerLifetimeScope();
			builder.RegisterType<NotificationRepository>().As<INotificationRepository>().InstancePerLifetimeScope();
			builder.RegisterType<NotificationTypeRepository>().As<INotificatorTypeRepository>().InstancePerLifetimeScope();
			builder.RegisterType<NotificatorContactRepository>().As<INotificatorContactRepository>().InstancePerLifetimeScope();
			builder.RegisterType<NotificationRequestRepository>().As<INotificationRequestRepository>().InstancePerLifetimeScope();

			return builder;
		}
	}
}
