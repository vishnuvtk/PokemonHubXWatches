using System.Collections.Generic;
using System.Linq;
using PokemonHubXWatches.Data;
using PokemonHubXWatches.Models;
using PokemonHubXWatches.Interfaces;

namespace PokemonHubXWatches.Services
{
    public class BuildService : IBuildService
    {
        private readonly ApplicationDbContext _context;

        public BuildService(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<BuildDTO> GetAllBuilds()
        {
            return _context.Builds.Select(build => new BuildDTO
            {
                BuildId = build.BuildId,
                PokemonUpdatedHP = build.PokemonUpdatedHP,
                PokemonUpdatedAttack = build.PokemonUpdatedAttack,
                PokemonUpdatedDefense = build.PokemonUpdatedDefense,
                PokemonUpdatedSpAttack = build.PokemonUpdatedSpAttack,
                PokemonUpdatedSpDefense = build.PokemonUpdatedSpDefense,
                PokemonUpdatedCDR = build.PokemonUpdatedCDR,
                PokemonId = build.PokemonId
            }).ToList();
        }

        public BuildDTO GetBuildById(int id)
        {
            var build = _context.Builds.Find(id);
            if (build == null) return null;

            return new BuildDTO
            {
                BuildId = build.BuildId,
                PokemonUpdatedHP = build.PokemonUpdatedHP,
                PokemonUpdatedAttack = build.PokemonUpdatedAttack,
                PokemonUpdatedDefense = build.PokemonUpdatedDefense,
                PokemonUpdatedSpAttack = build.PokemonUpdatedSpAttack,
                PokemonUpdatedSpDefense = build.PokemonUpdatedSpDefense,
                PokemonUpdatedCDR = build.PokemonUpdatedCDR,
                PokemonId = build.PokemonId
            };
        }

        public BuildDTO CreateBuild(BuildDTO build)
        {
            var entity = new Build
            {
                PokemonId = build.PokemonId,
                PokemonUpdatedHP = build.PokemonUpdatedHP,
                PokemonUpdatedAttack = build.PokemonUpdatedAttack,
                PokemonUpdatedDefense = build.PokemonUpdatedDefense,
                PokemonUpdatedSpAttack = build.PokemonUpdatedSpAttack,
                PokemonUpdatedSpDefense = build.PokemonUpdatedSpDefense,
                PokemonUpdatedCDR = build.PokemonUpdatedCDR
            };

            _context.Builds.Add(entity);
            _context.SaveChanges();

            build.BuildId = entity.BuildId;
            return build;
        }

        public bool UpdateBuild(BuildDTO build)
        {
            var entity = _context.Builds.Find(build.BuildId);
            if (entity == null) return false;

            entity.PokemonUpdatedHP = build.PokemonUpdatedHP;
            entity.PokemonUpdatedAttack = build.PokemonUpdatedAttack;
            entity.PokemonUpdatedDefense = build.PokemonUpdatedDefense;
            entity.PokemonUpdatedSpAttack = build.PokemonUpdatedSpAttack;
            entity.PokemonUpdatedSpDefense = build.PokemonUpdatedSpDefense;
            entity.PokemonUpdatedCDR = build.PokemonUpdatedCDR;

            _context.Builds.Update(entity);
            _context.SaveChanges();

            return true;
        }

        public bool DeleteBuild(int id)
        {
            var build = _context.Builds.Find(id);
            if (build == null) return false;

            _context.Builds.Remove(build);
            _context.SaveChanges();

            return true;
        }

        public BuildDTO CalculateUpdatedStats(int pokemonId, List<int> heldItemIds)
        {
            var pokemon = _context.Pokemons.Find(pokemonId);
            if (pokemon == null) return null;

            var heldItems = _context.HeldItems.Where(item => heldItemIds.Contains(item.HeldItemId)).ToList();

            var updatedBuild = new BuildDTO
            {
                PokemonUpdatedHP = pokemon.PokemonHP + heldItems.Sum(i => i.HeldItemHP),
                PokemonUpdatedAttack = pokemon.PokemonAttack + heldItems.Sum(i => i.HeldItemAttack),
                PokemonUpdatedDefense = pokemon.PokemonDefense + heldItems.Sum(i => i.HeldItemDefense),
                PokemonUpdatedSpAttack = pokemon.PokemonSpAttack + heldItems.Sum(i => i.HeldItemSpAttack),
                PokemonUpdatedSpDefense = pokemon.PokemonSpDefense + heldItems.Sum(i => i.HeldItemSpDefense),
                PokemonUpdatedCDR = pokemon.PokemonCDR + heldItems.Sum(i => i.HeldItemCDR),
                PokemonId = pokemonId
            };

            return updatedBuild;
        }
    }
}
