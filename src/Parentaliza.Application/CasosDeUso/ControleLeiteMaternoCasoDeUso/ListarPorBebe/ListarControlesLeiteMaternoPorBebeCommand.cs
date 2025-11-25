using MediatR;
using Parentaliza.Application.Mediator;

namespace Parentaliza.Application.CasosDeUso.ControleLeiteMaternoCasoDeUso.ListarPorBebe;

public class ListarControlesLeiteMaternoPorBebeCommand : IRequest<CommandResponse<List<ListarControlesLeiteMaternoPorBebeCommandResponse>>>
{
    public Guid BebeNascidoId { get; private set; }

    public ListarControlesLeiteMaternoPorBebeCommand(Guid bebeNascidoId)
    {
        BebeNascidoId = bebeNascidoId;
    }
}

