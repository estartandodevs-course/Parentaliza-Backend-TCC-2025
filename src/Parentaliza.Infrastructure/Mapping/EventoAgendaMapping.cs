using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Parentaliza.Domain.Entidades;

namespace Parentaliza.Infrastructure.Mapping;

public class EventoAgendaMapping : IEntityTypeConfiguration<EventoAgenda>
{
    public void Configure(EntityTypeBuilder<EventoAgenda> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Evento)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(e => e.Especialidade)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(e => e.Localizacao)
               .IsRequired()
               .HasMaxLength(500);

        builder.Property(e => e.Data)
               .IsRequired()
               .HasColumnType("date");

        builder.Property(e => e.Hora)
               .IsRequired()
               .HasColumnType("time");

        builder.Property(e => e.Anotacao)
               .IsRequired()
               .HasMaxLength(1000);

        builder.ToTable("EventosAgendas");
    }
}