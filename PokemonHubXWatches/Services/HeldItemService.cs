using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PokemonHubXWatches.Data;
using PokemonHubXWatches.Interfaces;
using PokemonHubXWatches.Models;

namespace PokemonHubXWatches.Services
{
    public class HeldItemService : IHeldItemService
    {
        private readonly ApplicationDbContext _context;

        public HeldItemService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<HeldItem>> ListHeldItems()
        {
            return await _context.HeldItems.ToListAsync();
        }

        public async Task<HeldItem> FindHeldItem(int id)
        {
            return await _context.HeldItems.FindAsync(id);
        }

        public async Task<HeldItem> CreateHeldItem(HeldItem heldItem)
        {
            _context.HeldItems.Add(heldItem);
            await _context.SaveChangesAsync();
            return heldItem;
        }

        public async Task<bool> UpdateHeldItem(int id, HeldItem heldItem)
        {
            var existingItem = await _context.HeldItems.FindAsync(id);
            if (existingItem == null) return false;

            // Update properties
            existingItem.HeldItemName = heldItem.HeldItemName;
            existingItem.HeldItemHP = heldItem.HeldItemHP;
            existingItem.HeldItemAttack = heldItem.HeldItemAttack;
            existingItem.HeldItemDefense = heldItem.HeldItemDefense;
            existingItem.HeldItemSpAttack = heldItem.HeldItemSpAttack;
            existingItem.HeldItemSpDefense = heldItem.HeldItemSpDefense;
            existingItem.HeldItemCDR = heldItem.HeldItemCDR;
            existingItem.HeldItemImage = heldItem.HeldItemImage;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteHeldItem(int id)
        {
            var heldItem = await _context.HeldItems.FindAsync(id);
            if (heldItem == null) return false;

            _context.HeldItems.Remove(heldItem);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
