using Parentaliza.Domain.Entidades;

namespace Parentaliza.Domain.InterfacesRepository;

public interface IControleLeiteMaternoRepository : IRepository<ControleLeiteMaterno>
{
    Task<List<ControleLeiteMaterno>> ObterControlesPorBebe(Guid bebeNascidoId);
}