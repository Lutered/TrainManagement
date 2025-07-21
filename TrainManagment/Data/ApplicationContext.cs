using Microsoft.EntityFrameworkCore;
using TrainManagment.Data.Entities;

namespace TrainManagment.Data
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Item> Items { get; set; }
        public DbSet<ItemQuantity> ItemQuantities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Item>()
                        .HasIndex(u => u.UniqueNumber)
                        .IsUnique();

            modelBuilder.Entity<ItemQuantity>().HasKey(x => x.ItemId);

            modelBuilder.Entity<ItemQuantity>()
                .ToTable(x =>
                    x.HasCheckConstraint("CK_Quantity_Positive", "Quantity > 0")
                );

            base.OnModelCreating(modelBuilder);
        }
    }
}
