using Microsoft.EntityFrameworkCore;
using TrainManagment.Data.Entities;
using TrainManagment.Interfaces;

namespace TrainManagment.Data.Repositories
{
    public class ItemRepository(
        ApplicationContext _context
        ) : IItemRepository
    {
        public async Task<bool> IsExistsAsync(int id)
                => await _context
                        .Items
                        .AnyAsync(x => x.Id == id);

        public async Task<Item?> GetItemAsync(int id)
                => await _context
                        .Items
                        .FindAsync(id);
        public async Task<bool> IsExistsAsync(string uniqueNumber)
                => await _context
                        .Items
                        .AnyAsync(x => x.UniqueNumber == uniqueNumber);

        public async Task<Item?> GetItemAsync(string uniqueNumber)
                => await _context
                        .Items
                        .FirstOrDefaultAsync(x => x.UniqueNumber == uniqueNumber);
        public async Task CreateItemAsync(Item item)
         => await _context.Items.AddAsync(item);

        public void RemoveItem(Item item)
            => _context.Items.Remove(item);

        public async Task AddQualityAsync(int itemId, int quality)
        {
            if (quality < 0)
                throw new ArgumentOutOfRangeException(nameof(quality));

            var item = await _context.Items.FindAsync(itemId);

            if (item == null)
                throw new NullReferenceException();

            var qualityItem = await _context
                                .ItemQualities
                                .FirstOrDefaultAsync(x => x.ItemId == itemId);

            if (qualityItem == null)
                await _context.ItemQualities.AddAsync(
                        new ItemQuality()
                        {
                            Item = item,
                            Quality = quality
                        });
            else qualityItem.Quality = quality;
        }

        public async Task RemoveQualityAsync(int itemId)
        {
            var qualityItem = await _context.ItemQualities.FirstOrDefaultAsync(x => x.ItemId == itemId);

            if (qualityItem == null) return;

            _context.ItemQualities.Remove(qualityItem);
        }

        public async Task<bool> SaveChangesAsync()
            => await _context.SaveChangesAsync() > 0;
    }
}
