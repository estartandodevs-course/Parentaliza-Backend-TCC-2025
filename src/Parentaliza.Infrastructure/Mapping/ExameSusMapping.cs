using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Parentaliza.Domain.Entidades;

namespace Parentaliza.Infrastructure.Mapping;

public class ExameSusMapping : IEntityTypeConfiguration<ExameSus>
{
    public void Configure(EntityTypeBuilder<ExameSus> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.NomeExame)
               .IsRequired()
               .HasColumnType("varchar(80)");

        builder.Property(e => e.CategoriaFaixaEtaria)
               .IsRequired()
               .HasColumnType("varchar(80)");

        builder.Property(e => e.IdadeMinMesesExame)
               .IsRequired()
               .HasColumnType("varchar(80)");

        builder.Property(e => e.IdadeMaxMesesExame)
               .IsRequired()
               .HasColumnType("varchar(80)");

        builder.ToTable("ExameSus");
    }
}
