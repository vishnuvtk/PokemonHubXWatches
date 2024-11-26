using System.Collections.Generic;
using System.Linq;
using PokemonHubXWatches.Data;
using PokemonHubXWatches.Models;
using PokemonHubXWatches.Interfaces;

namespace PokemonHubXWatches.Services

{
    public class PokemonService : IPokemonService
    {
        private readonly ApplicationDbContext _context;

        public PokemonService(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<PokemonDTO> GetAllPokemon()
        {
            return _context.Pokemons.Select(p => new PokemonDTO
            {
                PokemonId = p.PokemonId,
                PokemonName = p.PokemonName,
                PokemonRole = p.PokemonRole,
                PokemonStyle = p.PokemonStyle,
                PokemonHP = p.PokemonHP,
                PokemonAttack = p.PokemonAttack,
                PokemonDefense = p.PokemonDefense,
                PokemonSpAttack = p.PokemonSpAttack,
                PokemonSpDefense = p.PokemonSpDefense,
                PokemonCDR = p.PokemonCDR,
                PokemonImage = p.PokemonImage
            }).ToList();
        }

        public PokemonDTO GetPokemonById(int id)
        {
            var pokemon = _context.Pokemons.Find(id);
            if (pokemon == null) return null;

            return new PokemonDTO
            {
                PokemonId = pokemon.PokemonId,
                PokemonName = pokemon.PokemonName,
                PokemonRole = pokemon.PokemonRole,
                PokemonStyle = pokemon.PokemonStyle,
                PokemonHP = pokemon.PokemonHP,
                PokemonAttack = pokemon.PokemonAttack,
                PokemonDefense = pokemon.PokemonDefense,
                PokemonSpAttack = pokemon.PokemonSpAttack,
                PokemonSpDefense = pokemon.PokemonSpDefense,
                PokemonCDR = pokemon.PokemonCDR,
                PokemonImage = pokemon.PokemonImage
            };
        }

        public PokemonDTO AddPokemon(PokemonDTO pokemon)
        {
            var entity = new Pokemon
            {
                PokemonName = pokemon.PokemonName,
                PokemonRole = pokemon.PokemonRole,
                PokemonStyle = pokemon.PokemonStyle,
                PokemonHP = pokemon.PokemonHP,
                PokemonAttack = pokemon.PokemonAttack,
                PokemonDefense = pokemon.PokemonDefense,
                PokemonSpAttack = pokemon.PokemonSpAttack,
                PokemonSpDefense = pokemon.PokemonSpDefense,
                PokemonCDR = pokemon.PokemonCDR,
                PokemonImage = pokemon.PokemonImage
            };

            _context.Pokemons.Add(entity);
            _context.SaveChanges();

            pokemon.PokemonId = entity.PokemonId;
            return pokemon;
        }

        public bool UpdatePokemon(PokemonDTO pokemon)
        {
            var entity = _context.Pokemons.Find(pokemon.PokemonId);
            if (entity == null) return false;

            entity.PokemonName = pokemon.PokemonName;
            entity.PokemonRole = pokemon.PokemonRole;
            entity.PokemonStyle = pokemon.PokemonStyle;
            entity.PokemonHP = pokemon.PokemonHP;
            entity.PokemonAttack = pokemon.PokemonAttack;
            entity.PokemonDefense = pokemon.PokemonDefense;
            entity.PokemonSpAttack = pokemon.PokemonSpAttack;
            entity.PokemonSpDefense = pokemon.PokemonSpDefense;
            entity.PokemonCDR = pokemon.PokemonCDR;
            entity.PokemonImage = pokemon.PokemonImage;

            _context.Pokemons.Update(entity);
            _context.SaveChanges();

            return true;
        }

        public bool DeletePokemon(int id)
        {
            var pokemon = _context.Pokemons.Find(id);
            if (pokemon == null) return false;

            _context.Pokemons.Remove(pokemon);
            _context.SaveChanges();

            return true;
        }
    }
}
