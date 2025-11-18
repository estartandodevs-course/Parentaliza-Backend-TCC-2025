using Microsoft.EntityFrameworkCore;
using Parentaliza.Domain.Entidades;

namespace Parentaliza.API.Infrastructure;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<EventoAgenda> EventoAgendas { get; set; }
    public DbSet<BebeNascido> BebeNascidos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder); // Faz a configuração padrão do Entity Framework Core
    }
}
