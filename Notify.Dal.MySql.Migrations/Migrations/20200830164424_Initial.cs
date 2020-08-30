using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Notify.Dal.MySql.Migrations.Migrations
{
    public partial class Initial : Migration
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
                    DefaultSubject = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailNotificatorSettings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NotificatorTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Slug = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificatorTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TelegramNotificatorSettings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Token = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramNotificatorSettings", x => x.Id);
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
                        name: "FK_Notificators_TelegramNotificatorSettings_TelegramSettingsId",
                        column: x => x.TelegramSettingsId,
                        principalTable: "TelegramNotificatorSettings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Notificators_EmailSettingsId",
                table: "Notificators",
                column: "EmailSettingsId",
                unique: true);

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
                name: "IX_NotificatorTypes_Slug",
                table: "NotificatorTypes",
                column: "Slug");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Notificators");

            migrationBuilder.DropTable(
                name: "NotificatorTypes");

            migrationBuilder.DropTable(
                name: "EmailNotificatorSettings");

            migrationBuilder.DropTable(
                name: "TelegramNotificatorSettings");
        }
    }
}
