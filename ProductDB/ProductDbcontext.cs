using Microsoft.EntityFrameworkCore;
using ProductDB.Entitys;

namespace ProductDB
{
    public class ProductDbcontext : DbContext
    {
        public DbSet<ProductEntitys> Products { get; set; }

        public ProductDbcontext(DbContextOptions<ProductDbcontext> options)
    : base(options)
        {
        }

       
    }
}
