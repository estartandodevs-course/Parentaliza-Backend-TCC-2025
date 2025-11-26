using MediatR;
using Parentaliza.Application.Mediator;

namespace Parentaliza.Application.CasosDeUso.ControleFraldaCasoDeUso.ListarPorBebe;

public class ListarControlesFraldaPorBebeCommand : IRequest<CommandResponse<List<ListarControlesFraldaPorBebeCommandResponse>>>
{
    public Guid BebeNascidoId { get; private set; }

    public ListarControlesFraldaPorBebeCommand(Guid bebeNascidoId)
    {
        BebeNascidoId = bebeNascidoId;
    }
}