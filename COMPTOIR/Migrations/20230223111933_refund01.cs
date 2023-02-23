using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace COMPTOIR.Migrations
{
    public partial class refund01 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RefTicketNumber",
                table: "Tickets",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RefundTicketId",
                table: "Tickets",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefundTicketNumber",
                table: "Tickets",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RefTicketNumber",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "RefundTicketId",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "RefundTicketNumber",
                table: "Tickets");
        }
    }
}
