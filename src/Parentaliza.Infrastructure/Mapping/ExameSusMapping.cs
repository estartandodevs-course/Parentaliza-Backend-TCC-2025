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
               .HasColumnType("varchar(200)");

        builder.Property(e => e.Descricao)
               .HasColumnType("varchar(1000)");

        builder.Property(e => e.CategoriaFaixaEtaria)
               .HasColumnType("varchar(100)");

        builder.Property(e => e.IdadeMinMesesExame)
               .HasColumnType("int");

        builder.Property(e => e.IdadeMaxMesesExame)
               .HasColumnType("int");

        builder.ToTable("ExameSus");
    }
}
