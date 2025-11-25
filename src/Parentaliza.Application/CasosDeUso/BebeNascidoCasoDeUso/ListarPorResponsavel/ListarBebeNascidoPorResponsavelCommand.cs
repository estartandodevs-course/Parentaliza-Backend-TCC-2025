using MediatR;
using Parentaliza.Application.Mediator;

namespace Parentaliza.Application.CasosDeUso.BebeNascidoCasoDeUso.ListarPorResponsavel;

public class ListarBebeNascidoPorResponsavelCommand : IRequest<CommandResponse<List<ListarBebeNascidoPorResponsavelCommandResponse>>>
{
    public Guid ResponsavelId { get; private set; }

    public ListarBebeNascidoPorResponsavelCommand(Guid responsavelId)
    {
        ResponsavelId = responsavelId;
    }
}

