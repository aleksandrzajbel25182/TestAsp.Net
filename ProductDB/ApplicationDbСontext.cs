using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ProductDB.Entitys;

namespace ProductDB
{
    public class ApplicationDbСontext : DbContext
    {
        public DbSet<Phone> Phones { get; set; }

        public ApplicationDbСontext(DbContextOptions<ApplicationDbСontext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Phone>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Brand).IsRequired();
                entity.Property(e => e.Model).IsRequired();
                entity.Property(e => e.Price).IsRequired();
            });

            modelBuilder.Entity<Phone>().HasData(
               new Phone { Id = 1, Brand = "Apple", Model = "iPhone 12", Price = 999.99m },
               new Phone { Id = 2, Brand = "Samsung", Model = "Galaxy S21", Price = 899.99m },
               new Phone { Id = 3, Brand = "Google", Model = "Pixel 5", Price = 699.99m },
               new Phone { Id = 4, Brand = "OnePlus", Model = "8T", Price = 749.99m },
               new Phone { Id = 5, Brand = "Xiaomi", Model = "Mi 11", Price = 599.99m }
           );
        }

    }
}
