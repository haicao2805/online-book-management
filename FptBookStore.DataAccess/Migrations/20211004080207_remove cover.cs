using Microsoft.EntityFrameworkCore.Migrations;

namespace FptBookStore.DataAccess.Migrations
{
    public partial class removecover : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_CoverType_CoverTypeId",
                table: "Product");

            migrationBuilder.DropTable(
                name: "CoverType");

            migrationBuilder.DropIndex(
                name: "IX_Product_CoverTypeId",
                table: "Product");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CoverType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoverType", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Product_CoverTypeId",
                table: "Product",
                column: "CoverTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_CoverType_CoverTypeId",
                table: "Product",
                column: "CoverTypeId",
                principalTable: "CoverType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
