using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Parentaliza.Domain.Entidades;

namespace Parentaliza.Infrastructure.Mapping;

public class VacinaSusMapping : IEntityTypeConfiguration<VacinaSus>
{
    public void Configure(EntityTypeBuilder<VacinaSus> builder)
    {
        builder.HasKey(v => v.Id);

        builder.Property(v => v.NomeVacina)
               .IsRequired()
               .HasColumnType("varchar(200)");

        builder.Property(v => v.Descricao)
               .HasColumnType("varchar(1000)");

        builder.Property(v => v.CategoriaFaixaEtaria)
               .HasColumnType("varchar(100)");

        builder.Property(v => v.IdadeMinMesesVacina)
               .HasColumnType("int");

        builder.Property(v => v.IdadeMaxMesesVacina)
               .HasColumnType("int");

        builder.ToTable("VacinaSus");
    }
}
