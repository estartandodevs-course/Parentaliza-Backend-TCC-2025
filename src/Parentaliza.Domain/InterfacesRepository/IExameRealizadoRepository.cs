using Parentaliza.Domain.Entidades;

namespace Parentaliza.Domain.InterfacesRepository;

public interface IExameRealizadoRepository : IRepository<ExameRealizado>
{
    Task<List<ExameRealizado>> ObterExamesPorBebe(Guid bebeNascidoId);
    Task<ExameRealizado?> ObterExameRealizadoPorBebeEExame(Guid bebeNascidoId, Guid exameSusId);
}

