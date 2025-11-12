using Parentaliza.Domain.Entidades;

namespace Parentaliza.Domain.Repository;

public interface IRepository<TEntity> : IDisposable where TEntity : Entity
{
    Task<Entity> ObterPorId(Guid id);
    Task<List<TEntity>> ObterTodos();
    Task Adicionar(TEntity entity);
    Task Atualizar(TEntity entity);
    Task Remover(Guid id);
    Task<int> SaveChanges(); 
}