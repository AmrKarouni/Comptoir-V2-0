using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace COMPTOIR.Migrations
{
    public partial class tickettaxes01 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TaxTicket");

            migrationBuilder.RenameColumn(
                name: "IsCancelled",
                table: "Tickets",
                newName: "IsCanceled");

            migrationBuilder.AddColumn<DateTime>(
                name: "CancelDate",
                table: "Tickets",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CanceledBy",
                table: "Tickets",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TicketTaxes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rate = table.Column<double>(type: "float", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    TicketId = table.Column<int>(type: "int", nullable: true),
                    TaxId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketTaxes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TicketTaxes_Taxes_TaxId",
                        column: x => x.TaxId,
                        principalTable: "Taxes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TicketTaxes_Tickets_TicketId",
                        column: x => x.TicketId,
                        principalTable: "Tickets",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_CanceledBy",
                table: "Tickets",
                column: "CanceledBy");

            migrationBuilder.CreateIndex(
                name: "IX_TicketTaxes_TaxId",
                table: "TicketTaxes",
                column: "TaxId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketTaxes_TicketId",
                table: "TicketTaxes",
                column: "TicketId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_AspNetUsers_CanceledBy",
                table: "Tickets",
                column: "CanceledBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_AspNetUsers_CanceledBy",
                table: "Tickets");

            migrationBuilder.DropTable(
                name: "TicketTaxes");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_CanceledBy",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "CancelDate",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "CanceledBy",
                table: "Tickets");

            migrationBuilder.RenameColumn(
                name: "IsCanceled",
                table: "Tickets",
                newName: "IsCancelled");

            migrationBuilder.CreateTable(
                name: "TaxTicket",
                columns: table => new
                {
                    TaxesId = table.Column<int>(type: "int", nullable: false),
                    TicketsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaxTicket", x => new { x.TaxesId, x.TicketsId });
                    table.ForeignKey(
                        name: "FK_TaxTicket_Taxes_TaxesId",
                        column: x => x.TaxesId,
                        principalTable: "Taxes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TaxTicket_Tickets_TicketsId",
                        column: x => x.TicketsId,
                        principalTable: "Tickets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TaxTicket_TicketsId",
                table: "TaxTicket",
                column: "TicketsId");
        }
    }
}
