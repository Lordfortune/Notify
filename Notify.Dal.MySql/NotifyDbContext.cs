using Microsoft.EntityFrameworkCore;
using Notify.Dal.Models;
using Notify.Common.Enums;

namespace Notify.Dal.Mysql
{
	public class NotifyDbContext : DbContext
	{
		public NotifyDbContext(DbContextOptions<NotifyDbContext> options)
			: base(options)
		{
		}

		public NotifyDbContext(string connectionString)
		{
			_connectionString = connectionString;
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if (string.IsNullOrWhiteSpace(_connectionString))
			{
				return;
			}

			optionsBuilder
				.UseMySQL(_connectionString);
		}

		private readonly string _connectionString;

		public DbSet<NotificatorDal> Notificators { get; set; }
		public DbSet<NotificatorTypeDal> NotificatorTypes { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.Entity<NotificatorTypeDal>(builder =>
			{
				builder.HasKey(x => x.Id);
				builder.HasIndex(x => x.Slug);

				builder.HasData(
					new NotificatorTypeDal { Id = NotificationTypeEnum.Email, Name = "Email", Slug = "email" }, 
					new NotificatorTypeDal { Id = NotificationTypeEnum.Telegram, Name = "Telegram", Slug = "telegram" });
			});

			modelBuilder.Entity<NotificatorDal>(builder =>
			{
				builder.HasKey(x => x.Id);
				builder.HasIndex(x => x.Slug);
				builder.HasIndex(x => x.TypeId);

				builder.HasDiscriminator(x => x.TypeId)
					.HasValue<TelegramNotificatorDal>(NotificationTypeEnum.Telegram)
					.HasValue<EmailNotificatorDal>(NotificationTypeEnum.Email);
			});

			modelBuilder.Entity<TelegramNotificatorDal>(builder =>
			{
				builder.HasOne(x => x.Settings)
					.WithOne(x => x.Notificator)
					.HasForeignKey<TelegramNotificatorDal>(x => x.SettingsId)
					.HasPrincipalKey<TelegramNotificatorSettingsDal>(x => x.Id);
			});

			modelBuilder.Entity<EmailNotificatorDal>(builder =>
			{
				builder.HasOne(x => x.Settings)
					.WithOne(x => x.Notificator)
					.HasForeignKey<EmailNotificatorDal>(x => x.SettingsId)
					.HasPrincipalKey<EmailNotificatorSettingsDal>(x => x.Id);
			});
		}
	}
}
