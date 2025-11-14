using Parentaliza.Domain.Entidades;

namespace Parentaliza.Domain.Repository;

public interface IBebeNascidoRepository : IRepository<BebeNascido>
{
    Task<bool> NomeJaUtilizado(string? nome);
}
