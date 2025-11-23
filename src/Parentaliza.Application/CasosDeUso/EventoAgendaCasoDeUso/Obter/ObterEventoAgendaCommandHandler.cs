using MediatR;
using Parentaliza.Application.Mediator;
using Parentaliza.Domain.InterfacesRepository;
using System.Net;

namespace Parentaliza.Application.CasosDeUso.EventoAgendaCasoDeUso.Obter;

public class ObterEventoAgendaCommandHandler : IRequestHandler<ObterEventoAgendaCommand, CommandResponse<ObterEventoAgendaCommandResponse>>
{
    private readonly IEventoAgendaRepository _eventoAgendaRepository;

    public ObterEventoAgendaCommandHandler(IEventoAgendaRepository eventoAgendaRepository)
    {
        _eventoAgendaRepository = eventoAgendaRepository;
    }

    public async Task<CommandResponse<ObterEventoAgendaCommandResponse>> Handle(ObterEventoAgendaCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var eventoAgenda = await _eventoAgendaRepository.ObterPorId(request.Id);

            if (eventoAgenda == null)
            {
                return CommandResponse<ObterEventoAgendaCommandResponse>.AdicionarErro(
                    statusCode: HttpStatusCode.NotFound,
                    mensagem: "Evento da agenda não encontrado.");
            }

            var response = new ObterEventoAgendaCommandResponse(
                eventoAgenda.Id,
                eventoAgenda.Evento,
                eventoAgenda.Especialidade,
                eventoAgenda.Localizacao,
                eventoAgenda.Data,
                eventoAgenda.Hora,
                eventoAgenda.Anotacao
            );

            return CommandResponse<ObterEventoAgendaCommandResponse>.Sucesso(response, HttpStatusCode.OK);
        }
        catch (Exception ex)
        {
            return CommandResponse<ObterEventoAgendaCommandResponse>.ErroCritico(
                mensagem: $"Ocorreu um erro ao obter o evento da agenda: {ex.Message}");
        }
    }
}