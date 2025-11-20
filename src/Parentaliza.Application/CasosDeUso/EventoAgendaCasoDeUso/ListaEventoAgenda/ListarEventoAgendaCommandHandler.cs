using MediatR;
using Parentaliza.Application.Mediator;
using Parentaliza.Domain.InterfacesRepository;
using System.Net;

namespace Parentaliza.Application.CasosDeUso.EventoAgendaCasoDeUso.ListaEventoAgenda;

public class ListarEventoAgendaCommandHandler : IRequestHandler<ListarEventoAgendaCommand, CommandResponse<List<ListarEventoAgendaCommandResponse>>>
{
    private readonly IEventoAgendaRepository _eventoAgendaRepository;

    public ListarEventoAgendaCommandHandler(IEventoAgendaRepository eventoAgendaRepository)
    {
        _eventoAgendaRepository = eventoAgendaRepository;
    }

    public async Task<CommandResponse<List<ListarEventoAgendaCommandResponse>>> Handle(ListarEventoAgendaCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var agendaComEventos = await _eventoAgendaRepository.ObterTodos();
            var response = agendaComEventos.Select(agenda => new ListarEventoAgendaCommandResponse(
                agenda.Id,
                agenda.Evento,
                agenda.Especialidade,
                agenda.Localizacao,
                agenda.Data,
                agenda.Hora,
                agenda.Anotacao
            )).ToList();
            return CommandResponse<List<ListarEventoAgendaCommandResponse>>.Sucesso(response, HttpStatusCode.OK);
        }
        catch (Exception ex)
        {
            return CommandResponse<List<ListarEventoAgendaCommandResponse>>.ErroCritico(mensagem: $"Ocorreu um erro ao listar os eventos da agenda: {ex.Message}");
        }
    }
}