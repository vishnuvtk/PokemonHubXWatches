using System.Collections.Generic;
using System.Linq;
using PokemonHubXWatches.Data;
using PokemonHubXWatches.Models;
using PokemonHubXWatches.Interfaces;

namespace PokemonHubXWatches.Services
{
    public class HeldItemService : IHeldItemService
    {
        private readonly ApplicationDbContext _context;

        public HeldItemService(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<HeldItemDTO> GetAllHeldItems()
        {
            return _context.HeldItems.Select(item => new HeldItemDTO
            {
                HeldItemId = item.HeldItemId,
                HeldItemName = item.HeldItemName,
                HeldItemHP = item.HeldItemHP,
                HeldItemAttack = item.HeldItemAttack,
                HeldItemDefense = item.HeldItemDefense,
                HeldItemSpAttack = item.HeldItemSpAttack,
                HeldItemSpDefense = item.HeldItemSpDefense,
                HeldItemCDR = item.HeldItemCDR,
                HeldItemImage = item.HeldItemImage
            }).ToList();
        }

        public HeldItemDTO GetHeldItemById(int id)
        {
            var item = _context.HeldItems.Find(id);
            if (item == null) return null;

            return new HeldItemDTO
            {
                HeldItemId = item.HeldItemId,
                HeldItemName = item.HeldItemName,
                HeldItemHP = item.HeldItemHP,
                HeldItemAttack = item.HeldItemAttack,
                HeldItemDefense = item.HeldItemDefense,
                HeldItemSpAttack = item.HeldItemSpAttack,
                HeldItemSpDefense = item.HeldItemSpDefense,
                HeldItemCDR = item.HeldItemCDR,
                HeldItemImage = item.HeldItemImage
            };
        }

        public HeldItemDTO AddHeldItem(HeldItemDTO heldItem)
        {
            var entity = new HeldItem
            {
                HeldItemName = heldItem.HeldItemName,
                HeldItemHP = heldItem.HeldItemHP,
                HeldItemAttack = heldItem.HeldItemAttack,
                HeldItemDefense = heldItem.HeldItemDefense,
                HeldItemSpAttack = heldItem.HeldItemSpAttack,
                HeldItemSpDefense = heldItem.HeldItemSpDefense,
                HeldItemCDR = heldItem.HeldItemCDR,
                HeldItemImage = heldItem.HeldItemImage
            };

            _context.HeldItems.Add(entity);
            _context.SaveChanges();

            heldItem.HeldItemId = entity.HeldItemId;
            return heldItem;
        }

        public bool UpdateHeldItem(HeldItemDTO heldItem)
        {
            var entity = _context.HeldItems.Find(heldItem.HeldItemId);
            if (entity == null) return false;

            entity.HeldItemName = heldItem.HeldItemName;
            entity.HeldItemHP = heldItem.HeldItemHP;
            entity.HeldItemAttack = heldItem.HeldItemAttack;
            entity.HeldItemDefense = heldItem.HeldItemDefense;
            entity.HeldItemSpAttack = heldItem.HeldItemSpAttack;
            entity.HeldItemSpDefense = heldItem.HeldItemSpDefense;
            entity.HeldItemCDR = heldItem.HeldItemCDR;
            entity.HeldItemImage = heldItem.HeldItemImage;

            _context.HeldItems.Update(entity);
            _context.SaveChanges();

            return true;
        }

        public bool DeleteHeldItem(int id)
        {
            var heldItem = _context.HeldItems.Find(id);
            if (heldItem == null) return false;

            _context.HeldItems.Remove(heldItem);
            _context.SaveChanges();

            return true;
        }
    }
}
