using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AnimalAdventure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeColumnNameAnimals : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Food",
                table: "Animals",
                newName: "Description");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Animals",
                newName: "Food");
        }
    }
}
