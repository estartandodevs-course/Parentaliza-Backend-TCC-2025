using Microsoft.EntityFrameworkCore;
using Parentaliza.Domain.Entidades;

namespace Parentaliza.Infrastructure.Context;
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.TrackAll;
 
        ChangeTracker.AutoDetectChangesEnabled = false;
    }
    public DbSet<EventoAgenda> EventoAgenda { get; private set; }
    public DbSet<BebeNascido> BebeNascido { get; private set; }
    public DbSet<BebeGestacao> BebeGestacao { get; private set; }
    public DbSet<Conteudo> Conteudos { get; private set; }
    public DbSet<ControleFralda> ControleFraldas { get; private set; }
    public DbSet<ControleLeiteMaterno> ControleLeiteMaternos { get; private set; }
    public DbSet<ControleMamadeira> ControleMamadeiras { get; private set; }
    public DbSet<ExameSus> ExameSus { get; private set; }
    public DbSet<VacinaSus> VacinaSus { get; private set; }
    public DbSet<ExameRealizado> ExamesRealizados { get; private set; }
    public DbSet<VacinaAplicada> VacinasAplicadas { get; private set; }
    public DbSet<Responsavel> Responsaveis { get; private set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        foreach (var relationship in modelBuilder.Model.GetEntityTypes()
        .SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.Restrict;

        base.OnModelCreating(modelBuilder);
    }
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entries = ChangeTracker.Entries<Entity>();

        foreach (var entry in entries)
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.SetCreatedAt();
            }

            if (entry.State == EntityState.Modified)
            {
                entry.Entity.SetUpdatedAt();
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}