using Microsoft.EntityFrameworkCore;
using PieShop.Data;

namespace PieShop.Models
{
    public class BethanysPieShopDbContext : PieShopDbContext
    {
        public BethanysPieShopDbContext(DbContextOptions<BethanysPieShopDbContext> option) : base(option)
        {


        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Pie> Pies { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set;}

       /* protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=BethanysPieShop;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pie>(entity =>
            {
                entity.HasKey(e => e.PieId);

                entity.HasIndex(e => e.CategoryId, "IX_Pies_CategoryId");

                entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.Category).WithMany(p => p.Pies).HasForeignKey(d => d.CategoryId);
            });

            modelBuilder.Entity<ShoppingCartItem>(entity =>
            {
                entity.HasKey(e => e.ShoppingCartItemsId);

                entity.HasIndex(e => e.Pie.PieId, "IX_ShoppingCartItems_PieId");

                entity.HasOne(d => d.Pie).WithMany(p => p.ShoppingCartItems).HasForeignKey(d => d.PieId);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);*/

    }
}
