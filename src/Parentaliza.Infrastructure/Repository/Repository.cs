using Microsoft.EntityFrameworkCore;
using Parentaliza.Infrastructure.Context;
using Parentaliza.Domain.Entidades;
using Parentaliza.Domain.InterfacesRepository;

namespace Parentaliza.Infrastructure.Repository;

public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity, new()
{
    protected readonly ApplicationDbContext _contexto;
    protected readonly DbSet<TEntity> _dbSet;

    protected Repository(ApplicationDbContext contexto)
    {
        _contexto = contexto;
        _dbSet = _contexto.Set<TEntity>();
    }

    public virtual async Task<TEntity> ObterPorId(Guid id)
    {
        var dado = await _dbSet.FindAsync(id);
        return dado;
    }

    public virtual async Task<List<TEntity>> ObterTodos()
    {
        var dado = await _dbSet.AsNoTracking().ToListAsync();
        return dado;
    }

    public virtual async Task Adicionar(TEntity entity)
    {
        await _dbSet.AddAsync(entity);
        await SaveChanges();
    }

    public virtual async Task Atualizar(TEntity entity)
    {
        _dbSet.Update(entity);
        await SaveChanges();
    }

    public virtual async Task Remover(Guid id)
    {
        var entity = await ObterPorId(id);
        if (entity == null)
        {
            throw new ArgumentException($"Entidade com Id {id} não encontrada.", nameof(id));
        }
        _dbSet.Remove(entity);
        await SaveChanges();
    }

    public async Task<int> SaveChanges()
    {
        var dado = await _contexto.SaveChangesAsync();
        return dado;
    }

    public void Dispose()
    {
        _contexto?.Dispose();
    }
}