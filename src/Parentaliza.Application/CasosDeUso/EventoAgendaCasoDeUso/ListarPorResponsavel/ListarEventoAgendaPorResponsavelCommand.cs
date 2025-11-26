using MediatR;
using Parentaliza.Application.Mediator;

namespace Parentaliza.Application.CasosDeUso.EventoAgendaCasoDeUso.ListarPorResponsavel;

public class ListarEventoAgendaPorResponsavelCommand : IRequest<CommandResponse<List<ListarEventoAgendaPorResponsavelCommandResponse>>>
{
    public Guid ResponsavelId { get; private set; }

    public ListarEventoAgendaPorResponsavelCommand(Guid responsavelId)
    {
        ResponsavelId = responsavelId;
    }
}