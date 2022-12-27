using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace COMPTOIR.Migrations
{
    public partial class Ticketstransactions04 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPaid",
                table: "Tickets",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<double>(
                name: "TotalAmount",
                table: "Tickets",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "TotalPaidAmount",
                table: "Tickets",
                type: "float",
                nullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "UnitPrice",
                table: "TicketRecipes",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPaid",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "TotalAmount",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "TotalPaidAmount",
                table: "Tickets");

            migrationBuilder.AlterColumn<double>(
                name: "UnitPrice",
                table: "TicketRecipes",
                type: "float",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");
        }
    }
}
