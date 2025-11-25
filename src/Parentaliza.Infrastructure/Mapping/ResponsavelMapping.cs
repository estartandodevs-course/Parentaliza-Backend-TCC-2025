using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Parentaliza.Domain.Entidades;

namespace Parentaliza.Infrastructure.Mapping;

public class ResponsavelMapping : IEntityTypeConfiguration<Responsavel>
{
    public void Configure(EntityTypeBuilder<Responsavel> builder)
    {
        builder.HasKey(r => r.Id);

        builder.Property(r => r.Nome)
               .IsRequired()
               .HasColumnType("varchar(80)");

        builder.Property(r => r.Email)
               .IsRequired()
               .HasColumnType("varchar(80)");

        builder.Property(r => r.TipoResponsavel)
               .IsRequired()
               .HasConversion<int>()
               .HasColumnType("int");

        builder.Property(r => r.Senha)
               .IsRequired()
               .HasColumnType("varchar(80)");

        builder.Property(r => r.FaseNascimento)
               .HasColumnType("varchar(80)");

        builder.ToTable("Responsaveis");
    }
}