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
               .HasConversion<int>()
               .HasColumnType("int");

        builder.Property(bn => bn.TipoSanguineo)
               .IsRequired()
               .HasConversion<int>()
               .HasColumnType("int");

        builder.Property(bn => bn.IdadeMeses)
               .IsRequired()
               .HasColumnType("int");

        builder.Property(bn => bn.Peso)
               .IsRequired()
               .HasColumnType("decimal(10,2)");

        builder.Property(bn => bn.Altura)
               .IsRequired()
               .HasColumnType("decimal(10,2)");

        builder.Property(bn => bn.ResponsavelId)
               .IsRequired();

        // Relacionamento
        builder.HasOne(bn => bn.Responsavel)
               .WithMany()
               .HasForeignKey(bn => bn.ResponsavelId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.ToTable("BebeNascido");
    }
}