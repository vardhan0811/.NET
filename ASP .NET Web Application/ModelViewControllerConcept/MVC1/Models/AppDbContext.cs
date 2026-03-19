using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MVC1.Models;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<PriceChangeLog> PriceChangeLogs { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductPriceAudit> ProductPriceAudits { get; set; }

    public virtual DbSet<ReorderLog> ReorderLogs { get; set; }

    public virtual DbSet<StockAudit> StockAudits { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PriceChangeLog>(entity =>
        {
            entity.HasKey(e => e.LogId).HasName("PK__PriceCha__5E54864830931AE6");

            entity.Property(e => e.ChangedAt).HasDefaultValueSql("(sysdatetime())");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__Products__B40CC6CDD20C07C1");

            entity.ToTable(tb =>
                {
                    tb.HasTrigger("trg_ProductPriceAudit");
                    tb.HasTrigger("trg_StockAudit_And_Validate");
                });

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
        });

        modelBuilder.Entity<ProductPriceAudit>(entity =>
        {
            entity.HasKey(e => e.AuditId).HasName("PK__ProductP__A17F2398498E0CB2");

            entity.Property(e => e.ChangedAt).HasDefaultValueSql("(sysdatetime())");
        });

        modelBuilder.Entity<ReorderLog>(entity =>
        {
            entity.HasKey(e => e.LogId).HasName("PK__ReorderL__5E548648BF533364");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysdatetime())");
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
