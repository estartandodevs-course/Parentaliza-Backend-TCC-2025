using MediatR;
using Parentaliza.Application.Mediator;

namespace Parentaliza.Application.CasosDeUso.ControleMamadeiraCasoDeUso.ListarPorBebe;

public class ListarControlesMamadeiraPorBebeCommand : IRequest<CommandResponse<List<ListarControlesMamadeiraPorBebeCommandResponse>>>
{
    public Guid BebeNascidoId { get; private set; }

    public ListarControlesMamadeiraPorBebeCommand(Guid bebeNascidoId)
    {
        BebeNascidoId = bebeNascidoId;
    }
}

