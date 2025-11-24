using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Parentaliza.Domain.Entidades;

namespace Parentaliza.Infrastructure.Mapping;

public class ControleFraldaMapping : IEntityTypeConfiguration<ControleFralda>
{
    public void Configure(EntityTypeBuilder<ControleFralda> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.BebeNascidoId)
               .IsRequired();

        builder.Property(c => c.HoraTroca)
               .IsRequired()
               .HasColumnType("datetime");

        builder.Property(c => c.TipoFralda)
               .HasColumnType("varchar(50)");

        builder.Property(c => c.Observacoes)
               .HasColumnType("varchar(500)");

        // Relacionamento
        builder.HasOne(c => c.BebeNascido)
               .WithMany()
               .HasForeignKey(c => c.BebeNascidoId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.ToTable("ControlesFralda");
    }
}
