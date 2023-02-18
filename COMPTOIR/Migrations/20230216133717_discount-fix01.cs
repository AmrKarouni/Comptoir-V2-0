using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace COMPTOIR.Migrations
{
    public partial class discountfix01 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Discounts_DiscountId",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_DiscountId",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "DiscountId",
                table: "Tickets");

            migrationBuilder.AddColumn<double>(
                name: "Discount",
                table: "Tickets",
                type: "float",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discount",
                table: "Tickets");

            migrationBuilder.AddColumn<int>(
                name: "DiscountId",
                table: "Tickets",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_DiscountId",
                table: "Tickets",
                column: "DiscountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Discounts_DiscountId",
                table: "Tickets",
                column: "DiscountId",
                principalTable: "Discounts",
                principalColumn: "Id");
        }
    }
}
