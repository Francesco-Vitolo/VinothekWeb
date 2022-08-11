using Microsoft.EntityFrameworkCore;
using VinothekManagerWeb.Models;

namespace VinothekManagerWeb.Data
{
    public class VinothekDbContext : DbContext
    {
        public VinothekDbContext(DbContextOptions<VinothekDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EventProductModel>().HasKey(x => new { x.EventID, x.ProductId });

            modelBuilder.Entity<EventProductModel>()
               .HasOne(x => x.Event)
               .WithMany(x => x.EventProducts)
               .HasForeignKey(x => x.EventID);

            modelBuilder.Entity<EventProductModel>()
           .HasOne(x => x.Product)
           .WithMany(x => x.EventProducts)
           .HasForeignKey(x => x.ProductId);


            base.OnModelCreating(modelBuilder);
        }

        public DbSet<ProductModel> Product { get; set; }
        public DbSet<ProducerModel> Producer { get; set; }
        public DbSet<ImageModel> Image { get; set; }
        public DbSet<EventModel> Event { get; set; }
        public DbSet<EventProductModel> EventProduct { get; set; }
    }
}