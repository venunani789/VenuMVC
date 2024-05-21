using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Venu.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class king : Migration
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

            migrationBuilder.InsertData(
                table: "Catogories",
                columns: new[] { "id", "DisplayOrder", "Name" },
                values: new object[] { 1, 1, "Test" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Catogories");
        }
    }
}
