using MediatR;
using Parentaliza.Application.Mediator;

namespace Parentaliza.Application.CasosDeUso.ExameRealizadoCasoDeUso.ListarPorBebe;

public class ListarExamesPorBebeCommand : IRequest<CommandResponse<List<ListarExamesPorBebeCommandResponse>>>
{
    public Guid BebeNascidoId { get; private set; }

    public ListarExamesPorBebeCommand(Guid bebeNascidoId)
    {
        BebeNascidoId = bebeNascidoId;
    }
}

