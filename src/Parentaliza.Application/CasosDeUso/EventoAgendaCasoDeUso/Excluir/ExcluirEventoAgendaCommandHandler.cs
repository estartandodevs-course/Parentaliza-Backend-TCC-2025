using MediatR;
using Parentaliza.Application.Mediator;
using Parentaliza.Domain.InterfacesRepository;
using System.Net;

namespace Parentaliza.Application.CasosDeUso.EventoAgendaCasoDeUso.Excluir;

public class ExcluirEventoAgendaCommandHandler : IRequestHandler<ExcluirEventoAgendaCommand, CommandResponse<ExcluirEventoAgendaCommandResponse>>
{
    private readonly IEventoAgendaRepository _eventoAgendaRepository;

    public ExcluirEventoAgendaCommandHandler(IEventoAgendaRepository eventoAgendaRepository)
    {
        _eventoAgendaRepository = eventoAgendaRepository;
    }

    public async Task<CommandResponse<ExcluirEventoAgendaCommandResponse>> Handle(ExcluirEventoAgendaCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var eventoAgenda = await _eventoAgendaRepository.ObterPorId(request.Id);

            if (eventoAgenda == null)
            {
                return CommandResponse<ExcluirEventoAgendaCommandResponse>.AdicionarErro(
                    statusCode: HttpStatusCode.NotFound,
                    mensagem: "Evento da agenda não encontrado.");
            }

            await _eventoAgendaRepository.Remover(request.Id);

            var response = new ExcluirEventoAgendaCommandResponse(request.Id);

            return CommandResponse<ExcluirEventoAgendaCommandResponse>.Sucesso(response, HttpStatusCode.OK);
        }
        catch (Exception ex)
        {
            return CommandResponse<ExcluirEventoAgendaCommandResponse>.ErroCritico(
                mensagem: $"Ocorreu um erro ao excluir o evento da agenda: {ex.Message}");
        }
    }
}
