using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Parentaliza.Domain.Entidades;

namespace Parentaliza.Infrastructure.Mapping;

public class BebeNascidoMapping : IEntityTypeConfiguration<BebeNascido>
{
    public void Configure(EntityTypeBuilder<BebeNascido> builder)
    {
        builder.HasKey(bn => bn.Id);

        builder.Property(bn => bn.Nome)
               .IsRequired()
               .HasColumnType("varchar(80)");

        builder.Property(bn => bn.DataNascimento)
               .IsRequired()
               .HasColumnType("datetime");

        builder.Property(bn => bn.Sexo)
               .IsRequired()
               .HasColumnType("varchar(80)");

        builder.Property(bn => bn.TipoSanguineo)
               .IsRequired()
               .HasColumnType("varchar(80)");

        builder.Property(bn => bn.IdadeMeses)
               .IsRequired()
               .HasColumnType("varchar(80)");

        builder.Property(bn => bn.Peso)
               .IsRequired()
               .HasColumnType("varchar(80)");

        builder.Property(bn => bn.Altura)
               .IsRequired()
               .HasColumnType("varchar(80)");

        builder.ToTable("BebeNascido");
    }
}