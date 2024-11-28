using PokemonHubXWatches.Models;
using Microsoft.EntityFrameworkCore;
using PokemonHubXWatches.Data;

namespace PokemonHubXWatches.Services
{
    public class WatchService
    {
        private readonly ApplicationDbContext _context;

        public WatchService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Create a new watch
        public async Task<WatchDTO> CreateWatch(WatchDTO watchDTO)
        {
            var watch = new Watch
            {
                Name = watchDTO.Name,
                Price = watchDTO.Price,
                Description = watchDTO.Description
            };

            _context.Watches.Add(watch);
            await _context.SaveChangesAsync();

            watchDTO.WatchID = watch.WatchID;
            return watchDTO;
        }

        // Get a watch by ID
        public async Task<WatchDTO> GetWatch(int id)
        {
            var watch = await _context.Watches
                .Where(w => w.WatchID == id)
                .Select(w => new WatchDTO
                {
                    WatchID = w.WatchID,
                    Name = w.Name,
                    Price = w.Price,
                    Description = w.Description
                })
                .FirstOrDefaultAsync();

            return watch;
        }

        // Get all watches
        public async Task<IEnumerable<WatchDTO>> GetAllWatches()
        {
            var watches = await _context.Watches
                .Select(w => new WatchDTO
                {
                    WatchID = w.WatchID,
                    Name = w.Name,
                    Price = w.Price,
                    Description = w.Description
                })
                .ToListAsync();

            return watches;
        }

        // Delete a watch by ID
        public async Task<bool> DeleteWatch(int id)
        {
            var watch = await _context.Watches.FindAsync(id);
            if (watch == null) return false;

            _context.Watches.Remove(watch);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
