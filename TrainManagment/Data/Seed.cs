using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using TrainManagement.Data.Entities;

namespace TrainManagement.Data
{
    public class Seed
    {
        public static async Task SeedData(ApplicationContext context, string seedDataUrl)
        {
            if (string.IsNullOrEmpty(seedDataUrl)) return;
            if (await context.Components.AnyAsync()) return;

            var seedData = await File.ReadAllTextAsync(seedDataUrl);

            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            var items = JsonSerializer.Deserialize<List<Component>>(seedData, options);

            if (items == null || items.Count == 0) return;

            foreach (var item in items)
                await context.Components.AddAsync(item);
            
            await context.SaveChangesAsync();
        }
    }
}
