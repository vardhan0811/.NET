using System;
using System.Collections.Generic;
using MVC1.Models;
using Microsoft.EntityFrameworkCore;

namespace MVC1.Data;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<PriceChangeLog> PriceChangeLog { get; set; }

    public virtual DbSet<ProductPriceAudit> ProductPriceAudit { get; set; }

    public virtual DbSet<Products> Products { get; set; }

    public virtual DbSet<ReorderLog> ReorderLog { get; set; }

    public virtual DbSet<StockAudit> StockAudit { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=CursorConcept;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PriceChangeLog>(entity =>
        {
            entity.HasKey(e => e.LogId).HasName("PK__PriceCha__5E54864830931AE6");

            entity.Property(e => e.ChangedAt).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.NewPrice).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.OldPrice).HasColumnType("decimal(10, 2)");
        });

        modelBuilder.Entity<ProductPriceAudit>(entity =>
        {
            entity.HasKey(e => e.AuditId).HasName("PK__ProductP__A17F2398498E0CB2");

            entity.Property(e => e.ChangedAt).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.ChangedBy).HasMaxLength(128);
            entity.Property(e => e.NewPrice).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.OldPrice).HasColumnType("decimal(10, 2)");
        });

        modelBuilder.Entity<Products>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__Products__B40CC6CDD20C07C1");

            entity.ToTable(tb =>
                {
                    tb.HasTrigger("trg_ProductPriceAudit");
                    tb.HasTrigger("trg_StockAudit_And_Validate");
                });

            entity.Property(e => e.Category)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.ProductName)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ReorderLog>(entity =>
        {
            entity.HasKey(e => e.LogId).HasName("PK__ReorderL__5E548648BF533364");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.Message)
                .HasMaxLength(200)
                .IsUnicode(false);
        });

        modelBuilder.Entity<StockAudit>(entity =>
        {
            entity.HasKey(e => e.AuditId).HasName("PK__StockAud__A17F2398DB8753CC");

            entity.Property(e => e.ChangedAt).HasDefaultValueSql("(sysdatetime())");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
