using Microsoft.EntityFrameworkCore;
using TrainManagement.Data.Entities;
using TrainManagement.Helpers;
using TrainManagement.Interfaces;
using TrainManagement.Params;

namespace TrainManagement.Data.Repositories
{
    public class ComponentRepository(
        ApplicationContext _context
        ) : IComponentRepository
    {
        public async Task<PagedList<Component>> GetComponentsAsync(ComponentParams componentParams, CancellationToken cancellationToken)
        {
            IQueryable<Component> query = _context.Components;

            if (!string.IsNullOrEmpty(componentParams.Name)) 
                query = query.Where(x => EF.Functions.Like(x.Name, $"%{componentParams.Name}%"));

            if (componentParams.MinQuantity is not null)
                query = query.Where(x => x.Quantity >= componentParams.MinQuantity);

            if (componentParams.MaxQuantity is not null)
                query = query.Where(x => x.Quantity <= componentParams.MaxQuantity);

            query = componentParams.OrderBy switch
            {
                "name" => query.OrderBy(x => x.Name),
                "quantity" => query.OrderByDescending(x => x.Quantity),
                _ => query.OrderBy(x => x.Id)
            };

            return await PagedList<Component>.CreateAsync(
                query.AsNoTracking(), 
                componentParams.PageNumber, 
                componentParams.PageSize,
                cancellationToken);
        }
        public async Task<Component?> GetComponentAsync(int id, CancellationToken cancellationToken)
                => await _context
                        .Components
                        .FindAsync(id, cancellationToken);

        public async Task<Component?> GetComponentAsync(string uniqueNumber, CancellationToken cancellationToken)
              => await _context
                      .Components
                      .FirstOrDefaultAsync(x => x.UniqueNumber == uniqueNumber, cancellationToken);

        public async Task<bool> IsExistsAsync(int id, CancellationToken cancellationToken)
        => await _context
                .Components
                .AnyAsync(x => x.Id == id, cancellationToken);
        public async Task<bool> IsExistsAsync(string uniqueNumber, CancellationToken cancellationToken)
                => await _context
                        .Components
                        .AnyAsync(x => x.UniqueNumber == uniqueNumber, cancellationToken);

      
        public async Task CreateItemAsync(Component item, CancellationToken cancellationToken)
         => await _context.Components.AddAsync(item, cancellationToken);

        public void RemoveComponent(Component item)
            => _context.Components.Remove(item);

        public async Task<bool> SaveChangesAsync()
            => await _context.SaveChangesAsync() > 0;
    }
}
