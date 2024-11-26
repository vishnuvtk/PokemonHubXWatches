using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PokemonHubXWatches.Data;
using PokemonHubXWatches.Interfaces;
using PokemonHubXWatches.Models;

namespace PokemonHubXWatches.Services
{
    public class PokemonService : IPokemonService
    {
        private readonly ApplicationDbContext _context;

        public PokemonService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Pokemon>> ListPokemons()
        {
            return await _context.Pokemons.ToListAsync();
        }

        public async Task<Pokemon> FindPokemon(int id)
        {
            return await _context.Pokemons.FindAsync(id);
        }

        public async Task<Pokemon> CreatePokemon(Pokemon pokemon)
        {
            _context.Pokemons.Add(pokemon);
            await _context.SaveChangesAsync();
            return pokemon;
        }

        public async Task<bool> UpdatePokemon(int id, Pokemon pokemon)
        {
            if (id != pokemon.PokemonId) return false;

            var existingPokemon = await _context.Pokemons.FindAsync(id);
            if (existingPokemon == null) return false;

            // Update Pokémon properties
            existingPokemon.PokemonName = pokemon.PokemonName;
            existingPokemon.PokemonRole = pokemon.PokemonRole;
            existingPokemon.PokemonStyle = pokemon.PokemonStyle;
            existingPokemon.PokemonHP = pokemon.PokemonHP;
            existingPokemon.PokemonAttack = pokemon.PokemonAttack;
            existingPokemon.PokemonDefense = pokemon.PokemonDefense;
            existingPokemon.PokemonSpAttack = pokemon.PokemonSpAttack;
            existingPokemon.PokemonSpDefense = pokemon.PokemonSpDefense;
            existingPokemon.PokemonCDR = pokemon.PokemonCDR;
            existingPokemon.PokemonImage = pokemon.PokemonImage;

            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }
        }


        public async Task<bool> DeletePokemon(int id)
        {
            var pokemon = await _context.Pokemons.FindAsync(id);
            if (pokemon == null) return false;

            _context.Pokemons.Remove(pokemon);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
