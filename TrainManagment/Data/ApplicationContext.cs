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
        public DbSet<ItemQuality> ItemQualities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Item>()
                        .HasIndex(u => u.UniqueNumber)
                        .IsUnique();

            modelBuilder.Entity<ItemQuality>().HasKey(x => x.ItemId);

            modelBuilder.Entity<ItemQuality>()
                .ToTable(x =>
                    x.HasCheckConstraint("CK_Quality_Positive", "Quality > 0")
                );

            base.OnModelCreating(modelBuilder);
        }
    }
}
