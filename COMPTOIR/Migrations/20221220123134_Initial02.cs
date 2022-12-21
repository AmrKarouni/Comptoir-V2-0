using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace COMPTOIR.Migrations
{
    public partial class Initial02 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Places_PlaceCategories_PlaceCategoryId",
                table: "Places");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "TicketRecipes");

            migrationBuilder.DropColumn(
                name: "PlaceIpAddress",
                table: "TicketRecipes");

            migrationBuilder.DropColumn(
                name: "PlaceName",
                table: "TicketRecipes");

            migrationBuilder.RenameColumn(
                name: "PlaceCategoryId",
                table: "Places",
                newName: "CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Places_PlaceCategoryId",
                table: "Places",
                newName: "IX_Places_CategoryId");

            migrationBuilder.AlterColumn<string>(
                name: "ServedBy",
                table: "Tickets",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "IssuedBy",
                table: "Tickets",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DoneBy",
                table: "Tickets",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DeliveredBy",
                table: "Tickets",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ConfirmedBy",
                table: "Tickets",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CustomerAddress",
                table: "Tickets",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_ConfirmedBy",
                table: "Tickets",
                column: "ConfirmedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_DeliveredBy",
                table: "Tickets",
                column: "DeliveredBy");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_DoneBy",
                table: "Tickets",
                column: "DoneBy");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_IssuedBy",
                table: "Tickets",
                column: "IssuedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_ServedBy",
                table: "Tickets",
                column: "ServedBy");

            migrationBuilder.CreateIndex(
                name: "IX_TicketRecipes_PlaceId",
                table: "TicketRecipes",
                column: "PlaceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Places_PlaceCategories_CategoryId",
                table: "Places",
                column: "CategoryId",
                principalTable: "PlaceCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TicketRecipes_Places_PlaceId",
                table: "TicketRecipes",
                column: "PlaceId",
                principalTable: "Places",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_AspNetUsers_ConfirmedBy",
                table: "Tickets",
                column: "ConfirmedBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_AspNetUsers_DeliveredBy",
                table: "Tickets",
                column: "DeliveredBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_AspNetUsers_DoneBy",
                table: "Tickets",
                column: "DoneBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_AspNetUsers_IssuedBy",
                table: "Tickets",
                column: "IssuedBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_AspNetUsers_ServedBy",
                table: "Tickets",
                column: "ServedBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Places_PlaceCategories_CategoryId",
                table: "Places");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketRecipes_Places_PlaceId",
                table: "TicketRecipes");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_AspNetUsers_ConfirmedBy",
                table: "Tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_AspNetUsers_DeliveredBy",
                table: "Tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_AspNetUsers_DoneBy",
                table: "Tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_AspNetUsers_IssuedBy",
                table: "Tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_AspNetUsers_ServedBy",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_ConfirmedBy",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_DeliveredBy",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_DoneBy",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_IssuedBy",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_ServedBy",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_TicketRecipes_PlaceId",
                table: "TicketRecipes");

            migrationBuilder.DropColumn(
                name: "CustomerAddress",
                table: "Tickets");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "Places",
                newName: "PlaceCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Places_CategoryId",
                table: "Places",
                newName: "IX_Places_PlaceCategoryId");

            migrationBuilder.AlterColumn<string>(
                name: "ServedBy",
                table: "Tickets",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "IssuedBy",
                table: "Tickets",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DoneBy",
                table: "Tickets",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DeliveredBy",
                table: "Tickets",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ConfirmedBy",
                table: "Tickets",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "TicketRecipes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PlaceIpAddress",
                table: "TicketRecipes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PlaceName",
                table: "TicketRecipes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Places_PlaceCategories_PlaceCategoryId",
                table: "Places",
                column: "PlaceCategoryId",
                principalTable: "PlaceCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
