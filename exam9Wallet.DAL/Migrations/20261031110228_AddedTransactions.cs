using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace exam9Wallet.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddedTransactions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vacancies_AspNetUsers_ReceiverId",
                table: "Vacancies");

            migrationBuilder.DropForeignKey(
                name: "FK_Vacancies_AspNetUsers_SenderId",
                table: "Vacancies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Vacancies",
                table: "Vacancies");

            migrationBuilder.RenameTable(
                name: "Vacancies",
                newName: "Transactions");

            migrationBuilder.RenameIndex(
                name: "IX_Vacancies_SenderId",
                table: "Transactions",
                newName: "IX_Transactions_SenderId");

            migrationBuilder.RenameIndex(
                name: "IX_Vacancies_ReceiverId",
                table: "Transactions",
                newName: "IX_Transactions_ReceiverId");

            migrationBuilder.AlterColumn<int>(
                name: "SenderId",
                table: "Transactions",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Transactions",
                table: "Transactions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_AspNetUsers_ReceiverId",
                table: "Transactions",
                column: "ReceiverId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_AspNetUsers_SenderId",
                table: "Transactions",
                column: "SenderId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_AspNetUsers_ReceiverId",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_AspNetUsers_SenderId",
                table: "Transactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Transactions",
                table: "Transactions");

            migrationBuilder.RenameTable(
                name: "Transactions",
                newName: "Vacancies");

            migrationBuilder.RenameIndex(
                name: "IX_Transactions_SenderId",
                table: "Vacancies",
                newName: "IX_Vacancies_SenderId");

            migrationBuilder.RenameIndex(
                name: "IX_Transactions_ReceiverId",
                table: "Vacancies",
                newName: "IX_Vacancies_ReceiverId");

            migrationBuilder.AlterColumn<int>(
                name: "SenderId",
                table: "Vacancies",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Vacancies",
                table: "Vacancies",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Vacancies_AspNetUsers_ReceiverId",
                table: "Vacancies",
                column: "ReceiverId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vacancies_AspNetUsers_SenderId",
                table: "Vacancies",
                column: "SenderId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
