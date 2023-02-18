using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace COMPTOIR.Migrations
{
    public partial class lastupdate01 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdateDate",
                table: "Tickets",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastUpdatedBy",
                table: "Tickets",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_LastUpdatedBy",
                table: "Tickets",
                column: "LastUpdatedBy");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_AspNetUsers_LastUpdatedBy",
                table: "Tickets",
                column: "LastUpdatedBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_AspNetUsers_LastUpdatedBy",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_LastUpdatedBy",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "LastUpdateDate",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "LastUpdatedBy",
                table: "Tickets");
        }
    }
}
