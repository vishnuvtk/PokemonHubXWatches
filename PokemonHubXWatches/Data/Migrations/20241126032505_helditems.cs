using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PokemonHubXWatches.Data.Migrations
{
    /// <inheritdoc />
    public partial class helditems : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pokemons_HeldItems_HeldItemId",
                table: "Pokemons");

            migrationBuilder.DropIndex(
                name: "IX_Pokemons_HeldItemId",
                table: "Pokemons");

            migrationBuilder.DropColumn(
                name: "HeldItemId",
                table: "Pokemons");

            migrationBuilder.AlterColumn<string>(
                name: "HeldItemName",
                table: "HeldItems",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HeldItemId",
                table: "Pokemons",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "HeldItemName",
                table: "HeldItems",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.CreateIndex(
                name: "IX_Pokemons_HeldItemId",
                table: "Pokemons",
                column: "HeldItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pokemons_HeldItems_HeldItemId",
                table: "Pokemons",
                column: "HeldItemId",
                principalTable: "HeldItems",
                principalColumn: "HeldItemId");
        }
    }
}
