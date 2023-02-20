using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace COMPTOIR.Migrations
{
    public partial class ticketrelation01 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TicketRecipes_Tickets_TicketId",
                table: "TicketRecipes");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketTaxes_Tickets_TicketId",
                table: "TicketTaxes");

            migrationBuilder.AlterColumn<int>(
                name: "TicketId",
                table: "TicketTaxes",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TicketId",
                table: "TicketRecipes",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TicketRecipes_Tickets_TicketId",
                table: "TicketRecipes",
                column: "TicketId",
                principalTable: "Tickets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TicketTaxes_Tickets_TicketId",
                table: "TicketTaxes",
                column: "TicketId",
                principalTable: "Tickets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TicketRecipes_Tickets_TicketId",
                table: "TicketRecipes");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketTaxes_Tickets_TicketId",
                table: "TicketTaxes");

            migrationBuilder.AlterColumn<int>(
                name: "TicketId",
                table: "TicketTaxes",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "TicketId",
                table: "TicketRecipes",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_TicketRecipes_Tickets_TicketId",
                table: "TicketRecipes",
                column: "TicketId",
                principalTable: "Tickets",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TicketTaxes_Tickets_TicketId",
                table: "TicketTaxes",
                column: "TicketId",
                principalTable: "Tickets",
                principalColumn: "Id");
        }
    }
}
