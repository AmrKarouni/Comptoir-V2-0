using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace COMPTOIR.Migrations
{
    public partial class tickettaxes04 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Discounts_DiscountId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_DiscountId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "DiscountId",
                table: "Transactions");

            migrationBuilder.AddColumn<double>(
                name: "Discount",
                table: "Transactions",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsCash",
                table: "Transactions",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discount",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "IsCash",
                table: "Transactions");

            migrationBuilder.AddColumn<int>(
                name: "DiscountId",
                table: "Transactions",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_DiscountId",
                table: "Transactions",
                column: "DiscountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Discounts_DiscountId",
                table: "Transactions",
                column: "DiscountId",
                principalTable: "Discounts",
                principalColumn: "Id");
        }
    }
}
