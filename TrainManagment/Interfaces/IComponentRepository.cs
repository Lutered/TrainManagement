using TrainManagement.Data.Entities;
using TrainManagement.Helpers;
using TrainManagement.Params;

namespace TrainManagement.Interfaces
{
    public interface IComponentRepository
    {
        public Task<PagedList<Component>> GetComponentsAsync(ComponentParams componentParams, CancellationToken cancellationToken);
        public Task<Component?> GetComponentAsync(int id, CancellationToken cancellationToken);
        public Task<Component?> GetComponentAsync(string uniqueNumber, CancellationToken cancellationToken);
        public Task<bool> IsExistsAsync(int id, CancellationToken cancellationToken);
        public Task<bool> IsExistsAsync(string uniqueNumber, CancellationToken cancellationToken);
        public Task CreateItemAsync(Component item, CancellationToken cancellationToken);
        public void RemoveComponent(Component item);
        //public Task AddQuantityAsync(int itemId, int quality);
        //public  Task RemoveQuantityAsync(int itemId);
        public Task<bool> SaveChangesAsync();
    }
}
