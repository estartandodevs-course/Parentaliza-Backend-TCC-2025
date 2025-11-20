using MediatR;
using Parentaliza.Application.Mediator;

namespace Parentaliza.Application.CasosDeUso.EventoAgendaCasoDeUso.Obter;

public class ObterEventoAgendaCommand : IRequest<CommandResponse<ObterEventoAgendaCommandResponse>>
{
    public Guid Id { get; private set; }

    public ObterEventoAgendaCommand(Guid id)
    {
        Id = id;
    }
}
