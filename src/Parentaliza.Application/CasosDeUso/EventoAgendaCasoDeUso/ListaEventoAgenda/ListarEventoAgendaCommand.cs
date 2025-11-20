using MediatR;
using Parentaliza.Application.Mediator;

namespace Parentaliza.Application.CasosDeUso.EventoAgendaCasoDeUso.ListaEventoAgenda;

public class ListarEventoAgendaCommand : IRequest<CommandResponse<List<ListarEventoAgendaCommandResponse>>>
{
}
