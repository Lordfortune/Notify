using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Notify.Dal.MySql.Migrations.Migrations
{
    public partial class InitialNotify : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmailNotificatorSettings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Port = table.Column<int>(nullable: false),
                    Host = table.Column<string>(nullable: true),
                    UserName = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    SenderName = table.Column<string>(nullable: true),
                    SenderAddress = table.Column<string>(nullable: true),
                    DefaultSubject = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailNotificatorSettings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NotificationStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Slug = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NotificationTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Slug = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    MiddleName = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TelegramChatTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Slug = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramChatTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TelegramNotificatorSettings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Token = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramNotificatorSettings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TelegramChatDal",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TelegramId = table.Column<long>(nullable: false),
                    ChatTypeId = table.Column<int>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    ContactPrivateId = table.Column<int>(nullable: true),
                    Username = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramChatDal", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TelegramChatDal_TelegramChatTypes_ChatTypeId",
                        column: x => x.ChatTypeId,
                        principalTable: "TelegramChatTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Notificators",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Slug = table.Column<string>(nullable: true),
                    TypeId = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    EmailSettingsId = table.Column<int>(nullable: true),
                    TelegramSettingsId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notificators", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notificators_EmailNotificatorSettings_EmailSettingsId",
                        column: x => x.EmailSettingsId,
                        principalTable: "EmailNotificatorSettings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Notificators_NotificationTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "NotificationTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Notificators_TelegramNotificatorSettings_TelegramSettingsId",
                        column: x => x.TelegramSettingsId,
                        principalTable: "TelegramNotificatorSettings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    PersonId = table.Column<int>(nullable: true),
                    TypeId = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    ChatId = table.Column<int>(nullable: true),
                    TelegramId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contacts_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Contacts_NotificationTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "NotificationTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Contacts_TelegramChatDal_ChatId",
                        column: x => x.ChatId,
                        principalTable: "TelegramChatDal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "NotificatorContacts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ContactId = table.Column<int>(nullable: false),
                    NotificatorId = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificatorContacts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NotificatorContacts_Contacts_ContactId",
                        column: x => x.ContactId,
                        principalTable: "Contacts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_NotificatorContacts_Notificators_NotificatorId",
                        column: x => x.NotificatorId,
                        principalTable: "Notificators",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NotificatorContactId = table.Column<int>(nullable: false),
                    Subject = table.Column<string>(nullable: true),
                    Message = table.Column<string>(nullable: true),
                    TypeId = table.Column<int>(nullable: false),
                    StatusId = table.Column<int>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notifications_NotificatorContacts_NotificatorContactId",
                        column: x => x.NotificatorContactId,
                        principalTable: "NotificatorContacts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Notifications_NotificationStatuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "NotificationStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Notifications_NotificationTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "NotificationTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "NotificationStatuses",
                columns: new[] { "Id", "Description", "Name", "Slug" },
                values: new object[,]
                {
                    { 1, "Создано", "Created", "created" },
                    { 2, "В обработке", "Processing", "processing" },
                    { 3, "Отправлено", "Sent", "sent" },
                    { 4, "Повторить", "Retry", "retry" },
                    { 5, "Ошибка", "Error", "error" }
                });

            migrationBuilder.InsertData(
                table: "NotificationTypes",
                columns: new[] { "Id", "Name", "Slug" },
                values: new object[,]
                {
                    { 2, "Email", "email" },
                    { 1, "Telegram", "telegram" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_IsActive",
                table: "Contacts",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_PersonId",
                table: "Contacts",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_TypeId",
                table: "Contacts",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_Email",
                table: "Contacts",
                column: "Email");

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_ChatId",
                table: "Contacts",
                column: "ChatId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_TelegramId",
                table: "Contacts",
                column: "TelegramId");

            migrationBuilder.CreateIndex(
                name: "IX_EmailNotificatorSettings_IsActive",
                table: "EmailNotificatorSettings",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_NotificatorContactId",
                table: "Notifications",
                column: "NotificatorContactId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_StatusId",
                table: "Notifications",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_TypeId",
                table: "Notifications",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationTypes_Slug",
                table: "NotificationTypes",
                column: "Slug");

            migrationBuilder.CreateIndex(
                name: "IX_NotificatorContacts_ContactId",
                table: "NotificatorContacts",
                column: "ContactId");

            migrationBuilder.CreateIndex(
                name: "IX_NotificatorContacts_IsActive",
                table: "NotificatorContacts",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_NotificatorContacts_NotificatorId",
                table: "NotificatorContacts",
                column: "NotificatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Notificators_EmailSettingsId",
                table: "Notificators",
                column: "EmailSettingsId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Notificators_IsActive",
                table: "Notificators",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_Notificators_Slug",
                table: "Notificators",
                column: "Slug");

            migrationBuilder.CreateIndex(
                name: "IX_Notificators_TypeId",
                table: "Notificators",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Notificators_TelegramSettingsId",
                table: "Notificators",
                column: "TelegramSettingsId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Persons_IsActive",
                table: "Persons",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramChatDal_ChatTypeId",
                table: "TelegramChatDal",
                column: "ChatTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramChatDal_TelegramId",
                table: "TelegramChatDal",
                column: "TelegramId");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramChatDal_ContactPrivateId",
                table: "TelegramChatDal",
                column: "ContactPrivateId");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramChatDal_Username",
                table: "TelegramChatDal",
                column: "Username");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramChatTypes_Slug",
                table: "TelegramChatTypes",
                column: "Slug");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramNotificatorSettings_IsActive",
                table: "TelegramNotificatorSettings",
                column: "IsActive");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "NotificatorContacts");

            migrationBuilder.DropTable(
                name: "NotificationStatuses");

            migrationBuilder.DropTable(
                name: "Contacts");

            migrationBuilder.DropTable(
                name: "Notificators");

            migrationBuilder.DropTable(
                name: "Persons");

            migrationBuilder.DropTable(
                name: "TelegramChatDal");

            migrationBuilder.DropTable(
                name: "EmailNotificatorSettings");

            migrationBuilder.DropTable(
                name: "NotificationTypes");

            migrationBuilder.DropTable(
                name: "TelegramNotificatorSettings");

            migrationBuilder.DropTable(
                name: "TelegramChatTypes");
        }
    }
}
