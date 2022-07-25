using Microsoft.EntityFrameworkCore;
using VinothekManagerWeb.Models;

namespace VinothekManagerWeb.Data
{
    public class VinothekDbContext : DbContext
    {
        public VinothekDbContext(DbContextOptions<VinothekDbContext> options) : base(options)
        {
        }

        public DbSet<ProductModel> Products{ get; set; }
        public DbSet<ProducerModel> Producer { get; set; }
    }
}
