using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PokemonHubXWatches.Data.Migrations
{
    /// <inheritdoc />
    public partial class pokeitembuild : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HeldItems",
                columns: table => new
                {
                    HeldItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HeldItemName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HeldItemHP = table.Column<int>(type: "int", nullable: false),
                    HeldItemAttack = table.Column<int>(type: "int", nullable: false),
                    HeldItemDefense = table.Column<int>(type: "int", nullable: false),
                    HeldItemSpAttack = table.Column<int>(type: "int", nullable: false),
                    HeldItemSpDefense = table.Column<int>(type: "int", nullable: false),
                    HeldItemCDR = table.Column<int>(type: "int", nullable: false),
                    HeldItemImage = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HeldItems", x => x.HeldItemId);
                });

            migrationBuilder.CreateTable(
                name: "Pokemons",
                columns: table => new
                {
                    PokemonId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PokemonName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PokemonRole = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PokemonStyle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PokemonHP = table.Column<int>(type: "int", nullable: false),
                    PokemonAttack = table.Column<int>(type: "int", nullable: false),
                    PokemonDefense = table.Column<int>(type: "int", nullable: false),
                    PokemonSpAttack = table.Column<int>(type: "int", nullable: false),
                    PokemonSpDefense = table.Column<int>(type: "int", nullable: false),
                    PokemonCDR = table.Column<int>(type: "int", nullable: false),
                    PokemonImage = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pokemons", x => x.PokemonId);
                });

            migrationBuilder.CreateTable(
                name: "Builds",
                columns: table => new
                {
                    BuildId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PokemonUpdatedHP = table.Column<int>(type: "int", nullable: false),
                    PokemonUpdatedAttack = table.Column<int>(type: "int", nullable: false),
                    PokemonUpdatedDefense = table.Column<int>(type: "int", nullable: false),
                    PokemonUpdatedSpAttack = table.Column<int>(type: "int", nullable: false),
                    PokemonUpdatedSpDefense = table.Column<int>(type: "int", nullable: false),
                    PokemonUpdatedCDR = table.Column<int>(type: "int", nullable: false),
                    PokemonId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Builds", x => x.BuildId);
                    table.ForeignKey(
                        name: "FK_Builds_Pokemons_PokemonId",
                        column: x => x.PokemonId,
                        principalTable: "Pokemons",
                        principalColumn: "PokemonId",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateTable(
                name: "BuildHeldItem",
                columns: table => new
                {
                    BuildsBuildId = table.Column<int>(type: "int", nullable: false),
                    HeldItemsHeldItemId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuildHeldItem", x => new { x.BuildsBuildId, x.HeldItemsHeldItemId });
                    table.ForeignKey(
                        name: "FK_BuildHeldItem_Builds_BuildsBuildId",
                        column: x => x.BuildsBuildId,
                        principalTable: "Builds",
                        principalColumn: "BuildId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BuildHeldItem_HeldItems_HeldItemsHeldItemId",
                        column: x => x.HeldItemsHeldItemId,
                        principalTable: "HeldItems",
                        principalColumn: "HeldItemId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BuildHeldItem_HeldItemsHeldItemId",
                table: "BuildHeldItem",
                column: "HeldItemsHeldItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Builds_PokemonId",
                table: "Builds",
                column: "PokemonId");

            migrationBuilder.CreateIndex(
                name: "IX_HeldItemPokemon_PokemonsPokemonId",
                table: "HeldItemPokemon",
                column: "PokemonsPokemonId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BuildHeldItem");

            migrationBuilder.DropTable(
                name: "HeldItemPokemon");

            migrationBuilder.DropTable(
                name: "Builds");

            migrationBuilder.DropTable(
                name: "HeldItems");

            migrationBuilder.DropTable(
                name: "Pokemons");
        }
    }
}
