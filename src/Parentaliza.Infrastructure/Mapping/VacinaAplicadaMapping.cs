using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Parentaliza.Domain.Entidades;

namespace Parentaliza.Infrastructure.Mapping;

public class VacinaAplicadaMapping : IEntityTypeConfiguration<VacinaAplicada>
{
    public void Configure(EntityTypeBuilder<VacinaAplicada> builder)
    {
        builder.HasKey(v => v.Id);

        builder.Property(v => v.BebeNascidoId)
               .IsRequired();

        builder.Property(v => v.VacinaSusId)
               .IsRequired();

        builder.Property(v => v.DataAplicacao)
               .HasColumnType("datetime");

        builder.Property(v => v.Aplicada)
               .IsRequired()
               .HasColumnType("bit");

        builder.Property(v => v.Observacoes)
               .HasColumnType("varchar(500)");

        builder.Property(v => v.Lote)
               .HasColumnType("varchar(50)");

        builder.Property(v => v.LocalAplicacao)
               .HasColumnType("varchar(100)");

        // Relacionamentos
        builder.HasOne(v => v.BebeNascido)
               .WithMany()
               .HasForeignKey(v => v.BebeNascidoId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(v => v.VacinaSus)
               .WithMany()
               .HasForeignKey(v => v.VacinaSusId)
               .OnDelete(DeleteBehavior.Restrict);

        // Índice para evitar duplicatas
        builder.HasIndex(v => new { v.BebeNascidoId, v.VacinaSusId })
               .IsUnique();

        builder.ToTable("VacinasAplicadas");
    }
}

