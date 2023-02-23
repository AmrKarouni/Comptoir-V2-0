using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace COMPTOIR.Migrations
{
    public partial class refund02 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "RefundDate",
                table: "Tickets",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RefundDate",
                table: "Tickets");
        }
    }
}
