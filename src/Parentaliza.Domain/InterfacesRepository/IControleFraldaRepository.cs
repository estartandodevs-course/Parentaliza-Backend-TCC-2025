using Parentaliza.Domain.Entidades;

namespace Parentaliza.Domain.InterfacesRepository;

public interface IControleFraldaRepository : IRepository<ControleFralda>
{
    Task<List<ControleFralda>> ObterControlesPorBebe(Guid bebeNascidoId);
}