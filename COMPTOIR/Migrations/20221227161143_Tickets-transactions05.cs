using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace COMPTOIR.Migrations
{
    public partial class Ticketstransactions05 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TicketRecipes_Places_PlaceId",
                table: "TicketRecipes");

            migrationBuilder.DropIndex(
                name: "IX_TicketRecipes_PlaceId",
                table: "TicketRecipes");

            migrationBuilder.DropColumn(
                name: "PlaceId",
                table: "TicketRecipes");

            migrationBuilder.AlterColumn<double>(
                name: "UnitPrice",
                table: "TransactionProducts",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "UnitCost",
                table: "RecipeProducts",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "UnitPrice",
                table: "TransactionProducts",
                type: "float",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AddColumn<int>(
                name: "PlaceId",
                table: "TicketRecipes",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "UnitCost",
                table: "RecipeProducts",
                type: "float",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.CreateIndex(
                name: "IX_TicketRecipes_PlaceId",
                table: "TicketRecipes",
                column: "PlaceId");

            migrationBuilder.AddForeignKey(
                name: "FK_TicketRecipes_Places_PlaceId",
                table: "TicketRecipes",
                column: "PlaceId",
                principalTable: "Places",
                principalColumn: "Id");
        }
    }
}
