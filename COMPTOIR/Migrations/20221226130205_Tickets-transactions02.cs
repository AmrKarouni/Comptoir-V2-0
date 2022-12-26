using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace COMPTOIR.Migrations
{
    public partial class Ticketstransactions02 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Tickets_TicketParentId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_TicketParentId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "TicketParentId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "IsChild",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "Tickets");

            migrationBuilder.AlterColumn<double>(
                name: "Count",
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
            migrationBuilder.AddColumn<int>(
                name: "TicketParentId",
                table: "Transactions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsChild",
                table: "Tickets",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ParentId",
                table: "Tickets",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Count",
                table: "TicketRecipes",
                type: "float",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_TicketParentId",
                table: "Transactions",
                column: "TicketParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Tickets_TicketParentId",
                table: "Transactions",
                column: "TicketParentId",
                principalTable: "Tickets",
                principalColumn: "Id");
        }
    }
}
