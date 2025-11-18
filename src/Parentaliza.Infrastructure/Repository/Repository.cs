using Microsoft.EntityFrameworkCore;
using Parentaliza.API.Infrastructure;
using Parentaliza.Domain.Entidades;
using Parentaliza.Domain.Repository;

namespace Parentaliza.Infrastructure.Repository;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity
{
    protected readonly ApplicationDbContext _context;
    protected readonly DbSet<TEntity> _dbSet;

    public Repository(ApplicationDbContext context)
    {
        _context = context;
        _dbSet = context.Set<TEntity>();
    }

    public virtual async Task<Entity> ObterPorId(Guid id)
    {
        return await _dbSet.FindAsync(id) ?? throw new KeyNotFoundException($"Entidade com ID {id} não encontrada.");
    }

    public virtual async Task<List<TEntity>> ObterTodos()
    {
        return await _dbSet.ToListAsync();
    }

    public virtual async Task Adicionar(TEntity entity)
    {
        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public virtual async Task Atualizar(TEntity entity)
    {
        _dbSet.Update(entity);
        await _context.SaveChangesAsync();
    }

    public virtual async Task Remover(Guid id)
    {
        var entity = await _dbSet.FindAsync(id);
        if (entity != null)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }

    public virtual async Task<int> SaveChanges()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context?.Dispose();
    }
}

