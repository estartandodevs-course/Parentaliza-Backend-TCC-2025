using Parentaliza.Domain.Entidades;

namespace Parentaliza.Domain.InterfacesRepository;
public interface IConteudoRepository : IRepository<Conteudo>
{
    Task<bool> NomeJaUtilizado(string? titulo);
}