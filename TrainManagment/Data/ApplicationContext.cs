using Microsoft.EntityFrameworkCore;
using TrainManagement.Data.Entities;

namespace TrainManagement.Data
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions options) : base(options)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        public DbSet<Component> Components { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Component>()
                        .HasIndex(u => u.UniqueNumber)
                        .IsUnique();

            modelBuilder.Entity<Component>()
                .ToTable(x =>
                    x.HasCheckConstraint("CK_Quantity_Positive", "Quantity > 0")
                );

            base.OnModelCreating(modelBuilder);
        }
    }
}
