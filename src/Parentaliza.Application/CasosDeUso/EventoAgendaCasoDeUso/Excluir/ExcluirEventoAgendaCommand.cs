using MediatR;
using Parentaliza.Application.Mediator;

namespace Parentaliza.Application.CasosDeUso.EventoAgendaCasoDeUso.Excluir;

public class ExcluirEventoAgendaCommand : IRequest<CommandResponse<ExcluirEventoAgendaCommandResponse>>
{
    public Guid Id { get; private set; }

    public ExcluirEventoAgendaCommand(Guid id)
    {
        Id = id;
    }
}
