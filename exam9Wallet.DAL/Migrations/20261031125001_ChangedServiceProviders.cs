using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace exam9Wallet.DAL.Migrations
{
    /// <inheritdoc />
    public partial class ChangedServiceProviders : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ServiceProviders",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Акнет" },
                    { 2, "Газпром" },
                    { 3, "Северэлектро" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ServiceProviders",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ServiceProviders",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ServiceProviders",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
