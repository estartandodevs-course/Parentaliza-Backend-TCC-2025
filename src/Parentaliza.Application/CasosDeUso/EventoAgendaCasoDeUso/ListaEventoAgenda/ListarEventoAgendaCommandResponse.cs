using MediatR;
using Parentaliza.Application.Mediator;

namespace Parentaliza.Application.CasosDeUso.EventoAgendaCasoDeUso.ListaEventoAgenda;

public class ListarEventoAgendaCommandResponse : IRequest<CommandResponse<List<ListarEventoAgendaCommand>>>{}