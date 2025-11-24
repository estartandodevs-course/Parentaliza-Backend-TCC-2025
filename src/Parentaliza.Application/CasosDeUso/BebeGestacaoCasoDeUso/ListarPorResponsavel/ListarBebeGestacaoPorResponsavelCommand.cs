using MediatR;
using Parentaliza.Application.Mediator;

namespace Parentaliza.Application.CasosDeUso.BebeGestacaoCasoDeUso.ListarPorResponsavel;

public class ListarBebeGestacaoPorResponsavelCommand : IRequest<CommandResponse<List<ListarBebeGestacaoPorResponsavelCommandResponse>>>
{
    public Guid ResponsavelId { get; private set; }

    public ListarBebeGestacaoPorResponsavelCommand(Guid responsavelId)
    {
        ResponsavelId = responsavelId;
    }
}

