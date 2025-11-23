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
               .HasColumnType("varchar(80)");

        builder.Property(v => v.Descricao)
               .IsRequired()
               .HasColumnType("varchar(80)");

        builder.Property(v => v.CategoriaFaixaEtaria)
               .IsRequired()
               .HasColumnType("varchar(80)");

        builder.Property(v => v.IdadeMinMesesVacina)
               .IsRequired()
               .HasColumnType("varchar(80)");

        builder.Property(v => v.IdadeMaxMesesVacina)
               .IsRequired()
               .HasColumnType("varchar(80)");

        builder.ToTable("VacinaSus");
    }
}
