using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Venu.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Foriegnkeyoption : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Catogories",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Catogories", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ISBN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Author = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ListPrice = table.Column<double>(type: "float", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Price50 = table.Column<double>(type: "float", nullable: false),
                    Price100 = table.Column<double>(type: "float", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CatogoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.id);
                    table.ForeignKey(
                        name: "FK_Products_Catogories_CatogoryId",
                        column: x => x.CatogoryId,
                        principalTable: "Catogories",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Catogories",
                columns: new[] { "id", "DisplayOrder", "Name" },
                values: new object[,]
                {
                    { 1, 1, "Test" },
                    { 2, 2, "Test1" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "id", "Author", "CatogoryId", "Description", "ISBN", "ImageUrl", "ListPrice", "Price", "Price100", "Price50", "Title" },
                values: new object[,]
                {
                    { 1, "Venu", 1, "Test", "SWD7868", "", 20.0, 30.0, 100.0, 60.0, "KingKong" },
                    { 2, "Venu1", 1, "Test1", "SWD7869", "", 30.0, 40.0, 100.0, 70.0, "KingKong1" },
                    { 3, "Venu2", 1, "Test1", "SWD7870", "", 30.0, 40.0, 100.0, 70.0, "KingKong2" },
                    { 4, "Venu3", 2, "Test1", "SWD7871", "", 30.0, 40.0, 100.0, 70.0, "KingKong3" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_CatogoryId",
                table: "Products",
                column: "CatogoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Catogories");
        }
    }
}
