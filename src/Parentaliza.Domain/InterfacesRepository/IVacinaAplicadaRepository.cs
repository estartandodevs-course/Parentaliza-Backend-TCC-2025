using Parentaliza.Domain.Entidades;

namespace Parentaliza.Domain.InterfacesRepository;

public interface IVacinaAplicadaRepository : IRepository<VacinaAplicada>
{
    Task<List<VacinaAplicada>> ObterVacinasPorBebe(Guid bebeNascidoId);
    Task<VacinaAplicada?> ObterVacinaAplicadaPorBebeEVacina(Guid bebeNascidoId, Guid vacinaSusId);
}

