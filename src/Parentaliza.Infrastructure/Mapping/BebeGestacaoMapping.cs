using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Parentaliza.Domain.Entidades;

namespace Parentaliza.Infrastructure.Mapping;

public class BebeGestacaoMapping : IEntityTypeConfiguration<BebeGestacao>
{
    public void Configure(EntityTypeBuilder<BebeGestacao> builder)
    {
        builder.HasKey(b => b.Id);

        builder.Property(b => b.Nome)
               .IsRequired()
               .HasColumnType("varchar(100)");

        builder.Property(b => b.DataPrevista)
               .IsRequired()
               .HasColumnType("date");

        builder.Property(b => b.DiasDeGestacao)
               .IsRequired()
               .HasColumnType("varchar(3)");

        builder.Property(b => b.PesoEstimado)
               .IsRequired();

        builder.Property(b => b.ComprimentoEstimado)
               .IsRequired();

        builder.ToTable("BebeGestacao");
    }
}