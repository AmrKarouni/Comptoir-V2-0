using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace COMPTOIR.Migrations
{
    public partial class refund03 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_AspNetUsers_LastUpdateBy",
                table: "Tickets");

            migrationBuilder.RenameColumn(
                name: "LastUpdateBy",
                table: "Tickets",
                newName: "RefundedBy");

            migrationBuilder.RenameIndex(
                name: "IX_Tickets_LastUpdateBy",
                table: "Tickets",
                newName: "IX_Tickets_RefundedBy");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_AspNetUsers_RefundedBy",
                table: "Tickets",
                column: "RefundedBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_AspNetUsers_LastUpdatedBy",
                table: "Tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_AspNetUsers_RefundedBy",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_LastUpdatedBy",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "LastUpdatedBy",
                table: "Tickets");

            migrationBuilder.RenameColumn(
                name: "RefundedBy",
                table: "Tickets",
                newName: "LastUpdateBy");

            migrationBuilder.RenameIndex(
                name: "IX_Tickets_RefundedBy",
                table: "Tickets",
                newName: "IX_Tickets_LastUpdateBy");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_AspNetUsers_LastUpdateBy",
                table: "Tickets",
                column: "LastUpdateBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
