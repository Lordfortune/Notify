using Microsoft.EntityFrameworkCore;
using Notify.Dal.Models;
using Notify.Common.Enums;
using Notify.Dal.Models.Email;
using Notify.Dal.Models.Telegram;

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

		public DbSet<ContactDal> Contacts { get; set; }
		public DbSet<NotificatorDal> Notificators { get; set; }
		public DbSet<NotificationTypeDal> NotificationTypes { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			#region Common

			modelBuilder.Entity<PersonDal>(builder =>
			{
				builder.HasKey(x => x.Id);
				builder.HasIndex(x => x.IsActive);

				builder
					.HasMany(x => x.Contacts)
					.WithOne(x => x.Person)
					.HasForeignKey(x => x.PersonId)
					.HasPrincipalKey(x => x.Id)
					.OnDelete(DeleteBehavior.Restrict);
			});
			modelBuilder.Entity<ContactDal>(builder =>
			{
				builder.HasKey(x => x.Id);
				builder.HasIndex(x => x.TypeId);
				builder.HasIndex(x => x.PersonId);
				builder.HasIndex(x => x.IsActive);

				builder.HasOne(x => x.Person)
					.WithMany(x => x.Contacts)
					.HasForeignKey(x => x.PersonId)
					.HasPrincipalKey(x => x.Id)
					.OnDelete(DeleteBehavior.Restrict);

				builder.HasOne(x => x.Type)
					.WithMany(x => x.Contacts)
					.HasForeignKey(x => x.TypeId)
					.HasPrincipalKey(x => x.Id)
					.OnDelete(DeleteBehavior.Restrict);

				builder
					.HasMany(x => x.NotificatorContacts)
					.WithOne(x => x.Contact)
					.HasForeignKey(x => x.ContactId)
					.HasPrincipalKey(x => x.Id)
					.OnDelete(DeleteBehavior.Restrict);

				builder.HasDiscriminator(x => x.TypeId)
					.HasValue<TelegramContactDal>(NotificationTypeEnum.Telegram)
					.HasValue<EmailContactDal>(NotificationTypeEnum.Email);
			});
			modelBuilder.Entity<NotificatorDal>(builder =>
			{
				builder.HasKey(x => x.Id);
				builder.HasIndex(x => x.Slug);
				builder.HasIndex(x => x.TypeId);
				builder.HasIndex(x => x.IsActive);

				builder
					.HasOne(x => x.Type)
					.WithMany(x => x.Notificators)
					.HasForeignKey(x => x.TypeId)
					.HasPrincipalKey(x => x.Id)
					.OnDelete(DeleteBehavior.Restrict);

				builder
					.HasMany(x => x.NotificatorContacts)
					.WithOne(x => x.Notificator)
					.HasForeignKey(x => x.NotificatorId)
					.HasPrincipalKey(x => x.Id)
					.OnDelete(DeleteBehavior.Restrict);

				builder.HasDiscriminator(x => x.TypeId)
					.HasValue<TelegramNotificatorDal>(NotificationTypeEnum.Telegram)
					.HasValue<EmailNotificatorDal>(NotificationTypeEnum.Email);
			});
			modelBuilder.Entity<NotificationDal>(builder =>
			{
				builder.HasKey(x => x.Id);
				builder.HasIndex(x => x.StatusId);
				builder.HasIndex(x => x.NotificatorContactId);

				builder
					.HasOne(x => x.Type)
					.WithMany(x => x.Notifications)
					.HasForeignKey(x => x.TypeId)
					.HasPrincipalKey(x => x.Id)
					.OnDelete(DeleteBehavior.Restrict);

				builder
					.HasOne(x => x.Status)
					.WithMany(x => x.Notifications)
					.HasForeignKey(x => x.StatusId)
					.HasPrincipalKey(x => x.Id)
					.OnDelete(DeleteBehavior.Restrict);

				builder
					.HasOne(x => x.NotificatorContact)
					.WithMany(x => x.Notifications)
					.HasForeignKey(x => x.NotificatorContactId)
					.HasPrincipalKey(x => x.Id)
					.OnDelete(DeleteBehavior.Restrict);

				builder.HasDiscriminator(x => x.TypeId)
					.HasValue<TelegramNotificationDal>(NotificationTypeEnum.Telegram)
					.HasValue<EmailNotificationDal>(NotificationTypeEnum.Email);
			});
			modelBuilder.Entity<NotificationTypeDal>(builder =>
			{
				builder.HasKey(x => x.Id);
				builder.HasIndex(x => x.Slug);

				builder
					.HasMany(x => x.Notifications)
					.WithOne(x => x.Type)
					.HasForeignKey(x => x.TypeId)
					.HasPrincipalKey(x => x.Id)
					.OnDelete(DeleteBehavior.Restrict);

				builder
					.HasMany(x => x.Notificators)
					.WithOne(x => x.Type)
					.HasForeignKey(x => x.TypeId)
					.HasPrincipalKey(x => x.Id)
					.OnDelete(DeleteBehavior.Restrict);

				builder
					.HasMany(x => x.Contacts)
					.WithOne(x => x.Type)
					.HasForeignKey(x => x.TypeId)
					.HasPrincipalKey(x => x.Id)
					.OnDelete(DeleteBehavior.Restrict);

				builder.HasData(
					new NotificationTypeDal { Id = NotificationTypeEnum.Email, Name = "Email", Slug = "email" },
					new NotificationTypeDal { Id = NotificationTypeEnum.Telegram, Name = "Telegram", Slug = "telegram" });
			});
			modelBuilder.Entity<NotificationStatusDal>(builder =>
			{
				builder.HasKey(x => x.Id);

				builder
					.HasMany(x => x.Notifications)
					.WithOne(x => x.Status)
					.HasForeignKey(x => x.StatusId)
					.HasPrincipalKey(x => x.Id)
					.OnDelete(DeleteBehavior.Restrict);

				builder.HasData(new NotificationStatusDal { Id = NotificationStatusEnum.Created, Name = "Created", Slug = "created", Description = "Создано" });
				builder.HasData(new NotificationStatusDal { Id = NotificationStatusEnum.Processing, Name = "Processing", Slug = "processing", Description = "В обработке" });
				builder.HasData(new NotificationStatusDal { Id = NotificationStatusEnum.Sent, Name = "Sent", Slug = "sent", Description = "Отправлено" });
				builder.HasData(new NotificationStatusDal { Id = NotificationStatusEnum.Retry, Name = "Retry", Slug = "retry", Description = "Повторить" });
				builder.HasData(new NotificationStatusDal { Id = NotificationStatusEnum.Error, Name = "Error", Slug = "error", Description = "Ошибка" });
			});
			modelBuilder.Entity<NotificatorContactDal>(builder =>
			{
				builder.HasKey(x => x.Id);
				builder.HasIndex(x => x.ContactId);
				builder.HasIndex(x => x.NotificatorId);
				builder.HasIndex(x => x.IsActive);

				builder
					.HasOne(x => x.Contact)
					.WithMany(x => x.NotificatorContacts)
					.HasForeignKey(x => x.ContactId)
					.HasPrincipalKey(x => x.Id)
					.OnDelete(DeleteBehavior.Restrict);

				builder
					.HasOne(x => x.Notificator)
					.WithMany(x => x.NotificatorContacts)
					.HasForeignKey(x => x.NotificatorId)
					.HasPrincipalKey(x => x.Id)
					.OnDelete(DeleteBehavior.Restrict);

				builder
					.HasMany(x => x.Notifications)
					.WithOne(x => x.NotificatorContact)
					.HasForeignKey(x => x.NotificatorContactId)
					.HasPrincipalKey(x => x.Id)
					.OnDelete(DeleteBehavior.Restrict);
			});

			#endregion

			#region Email

			modelBuilder.Entity<EmailContactDal>(builder =>
			{
				builder.HasIndex(x => x.Email);
			});
			modelBuilder.Entity<EmailNotificatorDal>(builder =>
			{
				builder.HasOne(x => x.Settings)
					.WithOne(x => x.Notificator)
					.HasForeignKey<EmailNotificatorDal>(x => x.SettingsId)
					.HasPrincipalKey<EmailNotificatorSettingsDal>(x => x.Id);
			});
			modelBuilder.Entity<EmailNotificationDal>(builder =>
			{
			});
			modelBuilder.Entity<EmailNotificatorSettingsDal>(builder =>
			{
				builder.HasKey(x => x.Id);
				builder.HasIndex(x => x.IsActive);

				builder.HasOne(x => x.Notificator)
					.WithOne(x => x.Settings)
					.HasForeignKey<EmailNotificatorDal>(x => x.SettingsId)
					.HasPrincipalKey<EmailNotificatorSettingsDal>(x => x.Id);
			});

			#endregion

			#region Telegram

			modelBuilder.Entity<TelegramChatTypeDal>(builder =>
			{
				builder.HasKey(x => x.Id);
				builder.HasIndex(x => x.Slug);

				builder
					.HasMany(x => x.Chats)
					.WithOne(x => x.ChatType)
					.HasForeignKey(x => x.ChatTypeId)
					.HasPrincipalKey(x => x.Id)
					.OnDelete(DeleteBehavior.Restrict);

				builder.HasData(
					new TelegramChatTypeDal { Id = TelegramChatTypeEnum.Private, Name = "Private", Slug = "private" },
					new TelegramChatTypeDal { Id = TelegramChatTypeEnum.Group, Name = "Group", Slug = "group" },
					new TelegramChatTypeDal { Id = TelegramChatTypeEnum.Channel, Name = "Channel", Slug = "channel" },
					new TelegramChatTypeDal { Id = TelegramChatTypeEnum.Supergroup, Name = "Supergroup", Slug = "supergroup" });
			});
			modelBuilder.Entity<TelegramContactDal>(builder =>
			{
				builder.HasIndex(x => x.ChatId);
				builder.HasIndex(x => x.TelegramId);

				builder.HasOne(x => x.Chat)
					.WithOne(x => x.Contact)
					.HasForeignKey<TelegramContactDal>(x => x.ChatId)
					.HasPrincipalKey<TelegramChatDal>(x => x.Id)
					.OnDelete(DeleteBehavior.Restrict);
			});
			modelBuilder.Entity<TelegramChatDal>(builder =>
			{
				builder.HasIndex(x => x.TelegramId);
				builder.HasIndex(x => x.ChatTypeId);

				builder
					.HasOne(x => x.ChatType)
					.WithMany(x => x.Chats)
					.HasForeignKey(x => x.ChatTypeId)
					.HasPrincipalKey(x => x.Id)
					.OnDelete(DeleteBehavior.Restrict);

				builder.HasDiscriminator(x => x.ChatTypeId)
					.HasValue<TelegramPrivateChatDal>(TelegramChatTypeEnum.Private)
					.HasValue<TelegramGroupChatDal>(TelegramChatTypeEnum.Group)
					.HasValue<TelegramGroupChatDal>(TelegramChatTypeEnum.Channel)
					.HasValue<TelegramGroupChatDal>(TelegramChatTypeEnum.Supergroup);
			});
			modelBuilder.Entity<TelegramPrivateChatDal>(builder =>
			{
				builder.HasIndex(x => x.ContactPrivateId);
				builder.HasIndex(x => x.Username);
			});
			modelBuilder.Entity<TelegramNotificatorDal>(builder =>
			{
				builder.HasOne(x => x.Settings)
					.WithOne(x => x.Notificator)
					.HasForeignKey<TelegramNotificatorDal>(x => x.SettingsId)
					.HasPrincipalKey<TelegramNotificatorSettingsDal>(x => x.Id);
			});
			modelBuilder.Entity<TelegramNotificationDal>(builder =>
			{
			});
			modelBuilder.Entity<TelegramNotificatorSettingsDal>(builder =>
			{
				builder.HasKey(x => x.Id);
				builder.HasIndex(x => x.IsActive);

				builder.HasOne(x => x.Notificator)
					.WithOne(x => x.Settings)
					.HasForeignKey<TelegramNotificatorDal>(x => x.SettingsId)
					.HasPrincipalKey<TelegramNotificatorSettingsDal>(x => x.Id);
			});

			#endregion

		}
	}
}
