using Microsoft.EntityFrameworkCore;
using TrainManagment.Data.Entities;

namespace TrainManagment.Interfaces
{
    public interface IItemRepository
    {
        public Task<bool> IsExistsAsync(int id);
        public Task<Item?> GetItemAsync(int id);
        public Task<bool> IsExistsAsync(string uniqueNumber);
        public Task<Item?> GetItemAsync(string uniqueNumber);
        public Task CreateItemAsync(Item item);
        public void RemoveItem(Item item);
        public Task AddQuantityAsync(int itemId, int quality);
        public  Task RemoveQuantityAsync(int itemId);
        public Task<bool> SaveChangesAsync();
    }
}
