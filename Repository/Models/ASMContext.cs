using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Repository.Models
{
    public partial class ASMContext : DbContext
    {
        public ASMContext()
        {
        }

        public ASMContext(DbContextOptions<ASMContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Asset> Assets { get; set; }
        public virtual DbSet<AssetType> AssetTypes { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Asset>(entity =>
            {
                entity.ToTable("Asset");

                entity.Property(e => e.AssetId)
                    .ValueGeneratedNever()
                    .HasColumnName("AssetID");

                entity.Property(e => e.AssetName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.AssetPath).HasMaxLength(500);

                entity.Property(e => e.AssetTypeId).HasColumnName("AssetTypeID");

                entity.Property(e => e.MainAssetId).HasColumnName("MainAssetID");

                entity.HasOne(d => d.AssetType)
                    .WithMany(p => p.Assets)
                    .HasForeignKey(d => d.AssetTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Asset__AssetType__30F848ED");
            });

            modelBuilder.Entity<AssetType>(entity =>
            {
                entity.ToTable("AssetType");

                entity.Property(e => e.AssetTypeId)
                    .ValueGeneratedNever()
                    .HasColumnName("AssetTypeID");

                entity.Property(e => e.AssetDesc)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
