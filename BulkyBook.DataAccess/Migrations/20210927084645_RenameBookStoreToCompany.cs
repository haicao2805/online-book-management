using Microsoft.EntityFrameworkCore.Migrations;

namespace BulkyBook.DataAccess.Migrations
{
    public partial class RenameBookStoreToCompany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_BookStore_BookStoreId",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "BookStoreId",
                table: "AspNetUsers",
                newName: "CompanyId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUsers_BookStoreId",
                table: "AspNetUsers",
                newName: "IX_AspNetUsers_CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_BookStore_CompanyId",
                table: "AspNetUsers",
                column: "CompanyId",
                principalTable: "BookStore",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_BookStore_CompanyId",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "CompanyId",
                table: "AspNetUsers",
                newName: "BookStoreId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUsers_CompanyId",
                table: "AspNetUsers",
                newName: "IX_AspNetUsers_BookStoreId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_BookStore_BookStoreId",
                table: "AspNetUsers",
                column: "BookStoreId",
                principalTable: "BookStore",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
