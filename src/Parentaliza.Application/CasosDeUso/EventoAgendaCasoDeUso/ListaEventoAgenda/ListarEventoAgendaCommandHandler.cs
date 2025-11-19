using MediatR;
using Parentaliza.Application.Mediator;
using Parentaliza.Domain.InterfacesRepository;
using System.Net;

namespace Parentaliza.Application.CasosDeUso.EventoAgendaCasoDeUso.ListaEventoAgenda;

public class ListarEventoAgendaCommandHandler : IRequestHandler<ListarEventoAgendaCommandResponse, CommandResponse<List<ListarEventoAgendaCommand>>>
{
    private readonly IEventoAgendaRepository _eventoAgendaRepository;

    public ListarEventoAgendaCommandHandler(IEventoAgendaRepository eventoAgendaRepository)
    {
        _eventoAgendaRepository = eventoAgendaRepository;
    }

    public async Task<CommandResponse<List<ListarEventoAgendaCommand>>> Handle(ListarEventoAgendaCommandResponse request, CancellationToken cancellationToken)
    {
        try
        {
            var AgendaComEventos = await _eventoAgendaRepository.ObterTodos();
            var response = AgendaComEventos.Select(agenda => new ListarEventoAgendaCommand(
                agenda.Evento,
                agenda.Especialidade,
                agenda.Localizacao,
                agenda.Data,
                agenda.Hora,
                agenda.Anotacao
            )).ToList();
            return CommandResponse<List<ListarEventoAgendaCommand>>.Sucesso(response, HttpStatusCode.OK);
        }
        catch (Exception ex)
        {
            return CommandResponse<List<ListarEventoAgendaCommand>>.ErroCritico(mensagem: $"Ocorreu um erro ao listar os eventos da agenda: {ex.Message}");
        }
    }
}