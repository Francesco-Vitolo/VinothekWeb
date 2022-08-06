using Microsoft.EntityFrameworkCore;
using VinothekManagerWeb.Models;

namespace VinothekManagerWeb.Data
{
    public class VinothekDbContext : DbContext
    {
        public VinothekDbContext(DbContextOptions<VinothekDbContext> options) : base(options)
        {
        }

        public DbSet<ProductModel> Product { get; set; }
        public DbSet<ProducerModel> Producer { get; set; }
        public DbSet<ImageModel> Image { get; set; }
    }
}