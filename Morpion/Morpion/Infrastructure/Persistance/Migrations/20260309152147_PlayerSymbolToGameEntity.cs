using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Morpion.Migrations
{
    /// <inheritdoc />
    public partial class PlayerSymbolToGameEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<char>(
                name: "Player1Symbol",
                table: "Games",
                type: "character(1)",
                nullable: true);

            migrationBuilder.AddColumn<char>(
                name: "Player2Symbol",
                table: "Games",
                type: "character(1)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Player1Symbol",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "Player2Symbol",
                table: "Games");
        }
    }
}
