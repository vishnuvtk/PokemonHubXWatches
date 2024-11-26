using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PokemonHubXWatches.Data;
using PokemonHubXWatches.Interfaces;
using PokemonHubXWatches.Models;

namespace PokemonHubXWatches.Services
{
    public class BuildService : IBuildService
    {
        private readonly ApplicationDbContext _context;

        public BuildService(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves all builds from the database.
        /// </summary>
        /// <returns>A list of builds.</returns>
        public async Task<IEnumerable<Build>> ListBuilds()
        {
            return await _context.Builds
                .Include(b => b.Pokemon) // Include related Pokémon
                .Include(b => b.HeldItems) // Include related Held Items
                .ToListAsync();
        }

        /// <summary>
        /// Finds a specific build by its ID.
        /// </summary>
        /// <param name="id">The build ID.</param>
        /// <returns>The build if found, otherwise null.</returns>
        public async Task<Build> FindBuild(int id)
        {
            return await _context.Builds
                .Include(b => b.Pokemon) // Include related Pokémon
                .Include(b => b.HeldItems) // Include related Held Items
                .FirstOrDefaultAsync(b => b.BuildId == id);
        }

        /// <summary>
        /// Creates a new build.
        /// </summary>
        /// <param name="build">The build to create.</param>
        /// <returns>The created build.</returns>
        public async Task<Build> CreateBuild(Build build)
        {
            // Attach related HeldItems and Pokémon to avoid duplicate entries
            foreach (var heldItem in build.HeldItems)
            {
                _context.Attach(heldItem);
            }

            _context.Attach(build.Pokemon);

            _context.Builds.Add(build);
            await _context.SaveChangesAsync();
            return build;
        }

        /// <summary>
        /// Updates an existing build.
        /// </summary>
        /// <param name="id">The build ID.</param>
        /// <param name="build">The updated build details.</param>
        /// <returns>True if successful, otherwise false.</returns>
        public async Task<bool> UpdateBuild(int id, Build build)
        {
            var existingBuild = await _context.Builds
                .Include(b => b.HeldItems)
                .FirstOrDefaultAsync(b => b.BuildId == id);

            if (existingBuild == null) return false;

            // Update basic properties
            existingBuild.PokemonUpdatedHP = build.PokemonUpdatedHP;
            existingBuild.PokemonUpdatedAttack = build.PokemonUpdatedAttack;
            existingBuild.PokemonUpdatedDefense = build.PokemonUpdatedDefense;
            existingBuild.PokemonUpdatedSpAttack = build.PokemonUpdatedSpAttack;
            existingBuild.PokemonUpdatedSpDefense = build.PokemonUpdatedSpDefense;
            existingBuild.PokemonUpdatedCDR = build.PokemonUpdatedCDR;

            // Update related Pokémon
            existingBuild.Pokemon = build.Pokemon;
            existingBuild.PokemonId = build.PokemonId;

            // Update HeldItems (replace the collection)
            existingBuild.HeldItems.Clear();
            foreach (var heldItem in build.HeldItems)
            {
                existingBuild.HeldItems.Add(await _context.HeldItems.FindAsync(heldItem.HeldItemId));
            }

            await _context.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Deletes a build by ID.
        /// </summary>
        /// <param name="id">The build ID.</param>
        /// <returns>True if successful, otherwise false.</returns>
        public async Task<bool> DeleteBuild(int id)
        {
            var build = await _context.Builds.FindAsync(id);
            if (build == null) return false;

            _context.Builds.Remove(build);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
