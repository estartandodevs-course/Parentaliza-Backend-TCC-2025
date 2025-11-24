using Parentaliza.Domain.Entidades;

namespace Parentaliza.Domain.InterfacesRepository;

public interface IControleMamadeiraRepository : IRepository<ControleMamadeira>
{
    Task<List<ControleMamadeira>> ObterControlesPorBebe(Guid bebeNascidoId);
}