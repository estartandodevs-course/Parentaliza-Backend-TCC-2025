using Microsoft.EntityFrameworkCore;
using Parentaliza.Domain.Entidades;

namespace Parentaliza.API.Infrastructure;

public class ParentalizaDbContext : DbContext
{
    public ParentalizaDbContext(DbContextOptions<ParentalizaDbContext> options) : base(options)
    {
    }

    public DbSet<OrderEntity> Orders => Set<OrderEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<OrderEntity>(entity =>
        {
            entity.ToTable("orders");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id)
                .HasMaxLength(128)
                .IsRequired();
            entity.Property(e => e.CustomerId)
                .HasMaxLength(128)
                .IsRequired();
            entity.Property(e => e.TotalAmount)
                .HasColumnType("decimal(18,2)")
                .IsRequired();
            entity.Property(e => e.OrderDate)
                .IsRequired();
            entity.Property(e => e.CreatedAt)
                .IsRequired();
            entity.Property(e => e.UpdatedAt)
                .IsRequired(false);

            // Map Items as JSON text column
            entity.Property(e => e.Items)
                .HasConversion(
                    v => System.Text.Json.JsonSerializer.Serialize(v, (System.Text.Json.JsonSerializerOptions?)null),
                    v => System.Text.Json.JsonSerializer.Deserialize<List<string>>(v, (System.Text.Json.JsonSerializerOptions?)null) ?? new List<string>())
                .HasColumnType("longtext");
        });
    }
}


