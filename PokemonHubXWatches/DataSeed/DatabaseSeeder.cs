using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PokemonHubXWatches.Data;
using PokemonHubXWatches.Models;

namespace PokemonHubXWatches.DataSeed
{
    public static class DatabaseSeeder
    {
        public static void Seed(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                // Apply migrations to ensure the database schema is up-to-date
                context.Database.Migrate();

                // Seed Pokémon
                if (!context.Pokemons.Any())
                {
                    context.Pokemons.AddRange(
                        new Pokemon
                        {
                            PokemonName = "Pikachu",
                            PokemonRole = "Attacker",
                            PokemonStyle = "Ranged",
                            PokemonHP = 100,
                            PokemonAttack = 120,
                            PokemonDefense = 80,
                            PokemonSpAttack = 130,
                            PokemonSpDefense = 70,
                            PokemonCDR = 5,
                            PokemonImage = "/images/pikachu.png"
                        },
                        new Pokemon
                        {
                            PokemonName = "Charizard",
                            PokemonRole = "Attacker",
                            PokemonStyle = "Melee",
                            PokemonHP = 150,
                            PokemonAttack = 140,
                            PokemonDefense = 90,
                            PokemonSpAttack = 150,
                            PokemonSpDefense = 80,
                            PokemonCDR = 8,
                            PokemonImage = "/images/charizard.png"
                        }
                    );
                }

                // Seed Held Items
                if (!context.HeldItems.Any())
                {
                    context.HeldItems.AddRange(
                        new HeldItem
                        {
                            HeldItemName = "Focus Band",
                            HeldItemHP = 30,
                            HeldItemAttack = 10,
                            HeldItemDefense = 20,
                            HeldItemSpAttack = 0,
                            HeldItemSpDefense = 15,
                            HeldItemCDR = 0,
                            HeldItemImage = "/images/focus_band.png"
                        },
                        new HeldItem
                        {
                            HeldItemName = "Scope Lens",
                            HeldItemHP = 0,
                            HeldItemAttack = 25,
                            HeldItemDefense = 10,
                            HeldItemSpAttack = 0,
                            HeldItemSpDefense = 0,
                            HeldItemCDR = 5,
                            HeldItemImage = "/images/scope_lens.png"
                        }
                    );
                }

                // Save changes to the database
                context.SaveChanges();
            }
        }
    }
}
