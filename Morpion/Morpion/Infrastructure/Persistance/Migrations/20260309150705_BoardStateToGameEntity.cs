using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Morpion.Migrations
{
    /// <inheritdoc />
    public partial class BoardStateToGameEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BoardState",
                table: "Games",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BoardState",
                table: "Games");
        }
    }
}
