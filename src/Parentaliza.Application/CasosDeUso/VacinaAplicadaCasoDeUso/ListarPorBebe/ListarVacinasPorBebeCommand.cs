using MediatR;
using Parentaliza.Application.Mediator;

namespace Parentaliza.Application.CasosDeUso.VacinaAplicadaCasoDeUso.ListarPorBebe;

public class ListarVacinasPorBebeCommand : IRequest<CommandResponse<List<ListarVacinasPorBebeCommandResponse>>>
{
    public Guid BebeNascidoId { get; private set; }

    public ListarVacinasPorBebeCommand(Guid bebeNascidoId)
    {
        BebeNascidoId = bebeNascidoId;
    }
}

