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
               .HasColumnType("int");

        builder.Property(b => b.PesoEstimado)
               .IsRequired()
               .HasColumnType("decimal(10,2)");

        builder.Property(b => b.ComprimentoEstimado)
               .IsRequired()
               .HasColumnType("decimal(10,2)");

        builder.Property(b => b.ResponsavelId)
               .IsRequired();

        // Relacionamento
        builder.HasOne(b => b.Responsavel)
               .WithMany()
               .HasForeignKey(b => b.ResponsavelId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.ToTable("BebeGestacao");
    }
}