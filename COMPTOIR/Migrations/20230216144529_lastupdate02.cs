using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace COMPTOIR.Migrations
{
    public partial class lastupdate02 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_AspNetUsers_LastUpdatedBy",
                table: "Tickets");

            migrationBuilder.RenameColumn(
                name: "LastUpdatedBy",
                table: "Tickets",
                newName: "LastUpdateBy");

            migrationBuilder.RenameIndex(
                name: "IX_Tickets_LastUpdatedBy",
                table: "Tickets",
                newName: "IX_Tickets_LastUpdateBy");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_AspNetUsers_LastUpdateBy",
                table: "Tickets",
                column: "LastUpdateBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_AspNetUsers_LastUpdateBy",
                table: "Tickets");

            migrationBuilder.RenameColumn(
                name: "LastUpdateBy",
                table: "Tickets",
                newName: "LastUpdatedBy");

            migrationBuilder.RenameIndex(
                name: "IX_Tickets_LastUpdateBy",
                table: "Tickets",
                newName: "IX_Tickets_LastUpdatedBy");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_AspNetUsers_LastUpdatedBy",
                table: "Tickets",
                column: "LastUpdatedBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
