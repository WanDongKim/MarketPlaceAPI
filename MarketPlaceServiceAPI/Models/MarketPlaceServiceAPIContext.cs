using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MarketPlaceServiceAPI.Models
{
    public partial class MarketPlaceServiceAPIContext : DbContext
    {
        public MarketPlaceServiceAPIContext()
        {
        }

        public MarketPlaceServiceAPIContext(DbContextOptions<MarketPlaceServiceAPIContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Discount> Discount { get; set; }
        public virtual DbSet<Market> Market { get; set; }
        public virtual DbSet<Product> Product { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.CategoryId).HasColumnName("Category_id");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Discount>(entity =>
            {
                entity.Property(e => e.DiscountId).HasColumnName("Discount_id");

                entity.Property(e => e.OfferAsPercent).HasColumnName("Offer_as_percent");
            });

            modelBuilder.Entity<Market>(entity =>
            {
                entity.Property(e => e.MarketId).HasColumnName("Market_id");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.ProductId).HasColumnName("Product_id");

                entity.Property(e => e.CategoryId).HasColumnName("Category_id");

                entity.Property(e => e.DiscountId).HasColumnName("Discount_id");

                entity.Property(e => e.MarketId).HasColumnName("Market_id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PictureId)
                    .HasColumnName("Picture_id")
                    .HasMaxLength(100)
                    .IsUnicode(false);

            });
        }
    }
}
