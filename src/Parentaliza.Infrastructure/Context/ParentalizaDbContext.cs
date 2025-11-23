using Microsoft.EntityFrameworkCore;
using Parentaliza.Domain.Entidades;

namespace Parentaliza.API.Infrastructure;
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        // Desabilita o rastreamento automático de mudanças para melhorar a performance em consultas somente leitura
        ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.TrackAll;
        // Desabilita a detecção automática de mudanças para operações explícitas de atualização
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
    public DbSet<Responsavel> Responsaveis { get; private set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Aplica todas as configurações de mapeamento de entidades do assembly atual
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        // Configurar o comportamento de exclusão em cascata para evitar exclusões acidentais
        foreach (var relationship in modelBuilder.Model.GetEntityTypes()
        .SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.Restrict;

        base.OnModelCreating(modelBuilder);
    }
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("DataCadastro") != null))
        {
            if (entry.State == EntityState.Added)
            {
                entry.Property("DataCadastro").CurrentValue = DateTime.Now;
            }

            if (entry.State == EntityState.Modified)
            {
                entry.Property("DataCadastro").IsModified = false;
            }
        }
        return base.SaveChangesAsync(cancellationToken);
    }
}