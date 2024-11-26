using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PokemonHubXWatches.Data.Migrations
{
    /// <inheritdoc />
    public partial class pokemon : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HeldItemPokemon");

            migrationBuilder.AlterColumn<string>(
                name: "PokemonName",
                table: "Pokemons",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "HeldItemId",
                table: "Pokemons",
                type: "int",
                nullable: true);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                name: "PokemonName",
                table: "Pokemons",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.CreateTable(
                name: "HeldItemPokemon",
                columns: table => new
                {
                    HeldItemsHeldItemId = table.Column<int>(type: "int", nullable: false),
                    PokemonsPokemonId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HeldItemPokemon", x => new { x.HeldItemsHeldItemId, x.PokemonsPokemonId });
                    table.ForeignKey(
                        name: "FK_HeldItemPokemon_HeldItems_HeldItemsHeldItemId",
                        column: x => x.HeldItemsHeldItemId,
                        principalTable: "HeldItems",
                        principalColumn: "HeldItemId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HeldItemPokemon_Pokemons_PokemonsPokemonId",
                        column: x => x.PokemonsPokemonId,
                        principalTable: "Pokemons",
                        principalColumn: "PokemonId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HeldItemPokemon_PokemonsPokemonId",
                table: "HeldItemPokemon",
                column: "PokemonsPokemonId");
        }
    }
}
