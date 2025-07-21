using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using TrainManagment.Data.Entities;

namespace TrainManagment.Data
{
    public class Seed
    {
        public static async Task SeedData(ApplicationContext context, string seedDataUrl)
        {
            if (string.IsNullOrEmpty(seedDataUrl)) return;
            if (await context.Items.AnyAsync()) return;

            var seedData = await File.ReadAllTextAsync(seedDataUrl);

            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            var items = JsonSerializer.Deserialize<List<Item>>(seedData, options);

            if (items == null || items.Count == 0) return;

            foreach (var item in items)
                await context.Items.AddAsync(item);
            
            await context.SaveChangesAsync();
        }
    }
}
