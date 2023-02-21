using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace COMPTOIR.Migrations
{
    public partial class customerfix01 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Addresses05",
                table: "Customers",
                newName: "Address05");

            migrationBuilder.RenameColumn(
                name: "Addresses04",
                table: "Customers",
                newName: "Address04");

            migrationBuilder.RenameColumn(
                name: "Addresses03",
                table: "Customers",
                newName: "Address03");

            migrationBuilder.RenameColumn(
                name: "Addresses02",
                table: "Customers",
                newName: "Address02");

            migrationBuilder.RenameColumn(
                name: "Addresses01",
                table: "Customers",
                newName: "Address01");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Address05",
                table: "Customers",
                newName: "Addresses05");

            migrationBuilder.RenameColumn(
                name: "Address04",
                table: "Customers",
                newName: "Addresses04");

            migrationBuilder.RenameColumn(
                name: "Address03",
                table: "Customers",
                newName: "Addresses03");

            migrationBuilder.RenameColumn(
                name: "Address02",
                table: "Customers",
                newName: "Addresses02");

            migrationBuilder.RenameColumn(
                name: "Address01",
                table: "Customers",
                newName: "Addresses01");
        }
    }
}
