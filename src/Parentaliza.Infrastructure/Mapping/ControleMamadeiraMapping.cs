using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Parentaliza.Domain.Entidades;

namespace Parentaliza.Infrastructure.Mapping;

public class ControleMamadeiraMapping : IEntityTypeConfiguration<ControleMamadeira>
{
    public void Configure(EntityTypeBuilder<ControleMamadeira> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.BebeNascidoId)
               .IsRequired();

        builder.Property(c => c.Data)
               .IsRequired()
               .HasColumnType("date");

        builder.Property(c => c.Hora)
               .IsRequired()
               .HasColumnType("time");

        builder.Property(c => c.QuantidadeLeite)
               .HasColumnType("decimal(10,2)");

        builder.Property(c => c.Anotacao)
               .HasColumnType("varchar(500)");

        // Relacionamento
        builder.HasOne(c => c.BebeNascido)
               .WithMany()
               .HasForeignKey(c => c.BebeNascidoId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.ToTable("ControlesMamadeira");
    }
}
