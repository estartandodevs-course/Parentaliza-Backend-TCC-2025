using Parentaliza.Domain.Entidades;

namespace Parentaliza.Domain.InterfacesRepository;
public interface IBebeNascidoRepository : IRepository<BebeNascido>
{
    Task<BebeNascido?> ObterBebeNascido(Guid bebeNascidoId);
    Task<bool> NomeJaUtilizado(string? nome);
    Task<List<BebeNascido>> ObterPorResponsavelId(Guid responsavelId);
}