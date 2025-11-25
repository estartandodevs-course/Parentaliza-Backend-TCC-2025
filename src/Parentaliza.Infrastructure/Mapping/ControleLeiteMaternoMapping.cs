using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Parentaliza.Domain.Entidades;

namespace Parentaliza.Infrastructure.Mapping;

public class ControleLeiteMaternoMapping : IEntityTypeConfiguration<ControleLeiteMaterno>
{
    public void Configure(EntityTypeBuilder<ControleLeiteMaterno> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.BebeNascidoId)
               .IsRequired();

        builder.Property(c => c.Cronometro)
               .IsRequired()
               .HasColumnType("datetime");

        builder.Property(c => c.LadoDireito)
               .HasColumnType("varchar(50)");

        builder.Property(c => c.LadoEsquerdo)
               .HasColumnType("varchar(50)");

        // Relacionamento
        builder.HasOne(c => c.BebeNascido)
               .WithMany()
               .HasForeignKey(c => c.BebeNascidoId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.ToTable("ControlesLeiteMaterno");
    }
}
