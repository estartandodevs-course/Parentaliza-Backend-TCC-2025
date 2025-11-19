using Parentaliza.Domain.Entidades;

namespace Parentaliza.Domain.InterfacesRepository;
public interface IBebeNascidoRepository : IRepository<BebeNascido>
{
    Task<bool> NomeJaUtilizado(string? nome);
}