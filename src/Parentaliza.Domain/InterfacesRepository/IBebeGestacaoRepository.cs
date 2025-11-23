using Parentaliza.Domain.Entidades;

namespace Parentaliza.Domain.InterfacesRepository;
public interface IBebeGestacaoRepository : IRepository<BebeGestacao>
{
    Task<BebeGestacao> ObterBebeGestacao(Guid bebeGestacaoId);
    Task<bool> NomeJaUtilizado(string? nome);
}