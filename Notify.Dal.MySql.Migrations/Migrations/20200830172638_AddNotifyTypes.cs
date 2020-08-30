using Microsoft.EntityFrameworkCore.Migrations;

namespace Notify.Dal.MySql.Migrations.Migrations
{
    public partial class AddNotifyTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "NotificatorTypes",
                columns: new[] { "Id", "Name", "Slug" },
                values: new object[] { 2, "Email", "email" });

            migrationBuilder.InsertData(
                table: "NotificatorTypes",
                columns: new[] { "Id", "Name", "Slug" },
                values: new object[] { 1, "Telegram", "telegram" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "NotificatorTypes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "NotificatorTypes",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
