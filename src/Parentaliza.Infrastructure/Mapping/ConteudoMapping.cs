using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Parentaliza.Domain.Entidades;

namespace Parentaliza.Infrastructure.Mapping;

public class ConteudoMapping : IEntityTypeConfiguration<Conteudo>
{
    public void Configure(EntityTypeBuilder<Conteudo> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Titulo)
               .IsRequired()
               .HasColumnType("varchar(80)");

        builder.Property(c => c.Categoria)
               .IsRequired()
               .HasColumnType("varchar(80)");

        builder.Property(c => c.DataPublicacao)
               .IsRequired()
               .HasColumnType("varchar(80)");

        builder.Property(c => c.Descricao)
               .IsRequired()
               .HasColumnType("varchar(80)");

        builder.ToTable("Conteudos");
    }
}
