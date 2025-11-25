using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Parentaliza.Domain.Entidades;

namespace Parentaliza.Infrastructure.Mapping;

public class ExameRealizadoMapping : IEntityTypeConfiguration<ExameRealizado>
{
    public void Configure(EntityTypeBuilder<ExameRealizado> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.BebeNascidoId)
               .IsRequired();

        builder.Property(e => e.ExameSusId)
               .IsRequired();

        builder.Property(e => e.DataRealizacao)
               .HasColumnType("datetime");

        builder.Property(e => e.Realizado)
               .IsRequired()
               .HasColumnType("bit");

        builder.Property(e => e.Observacoes)
               .HasColumnType("varchar(500)");

        // Relacionamentos
        builder.HasOne(e => e.BebeNascido)
               .WithMany()
               .HasForeignKey(e => e.BebeNascidoId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.ExameSus)
               .WithMany()
               .HasForeignKey(e => e.ExameSusId)
               .OnDelete(DeleteBehavior.Restrict);

        // Índice para evitar duplicatas
        builder.HasIndex(e => new { e.BebeNascidoId, e.ExameSusId })
               .IsUnique();

        builder.ToTable("ExamesRealizados");
    }
}

