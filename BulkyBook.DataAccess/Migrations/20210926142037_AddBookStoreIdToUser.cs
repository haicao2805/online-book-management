using Microsoft.EntityFrameworkCore.Migrations;

namespace BulkyBook.DataAccess.Migrations
{
    public partial class AddBookStoreIdToUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BookStoreId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_BookStoreId",
                table: "AspNetUsers",
                column: "BookStoreId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_BookStore_BookStoreId",
                table: "AspNetUsers",
                column: "BookStoreId",
                principalTable: "BookStore",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_BookStore_BookStoreId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_BookStoreId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "BookStoreId",
                table: "AspNetUsers");
        }
    }
}
