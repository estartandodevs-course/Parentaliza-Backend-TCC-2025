using MediatR;
using Microsoft.Extensions.Logging;
using Parentaliza.Application.Mediator;
using Parentaliza.Domain.InterfacesRepository;
using System.Net;

namespace Parentaliza.Application.CasosDeUso.EventoAgendaCasoDeUso.Excluir;

public class ExcluirEventoAgendaCommandHandler : IRequestHandler<ExcluirEventoAgendaCommand, CommandResponse<ExcluirEventoAgendaCommandResponse>>
{
    private readonly IEventoAgendaRepository _eventoAgendaRepository;
    private readonly ILogger<ExcluirEventoAgendaCommandHandler> _logger;

    public ExcluirEventoAgendaCommandHandler(
        IEventoAgendaRepository eventoAgendaRepository,
        ILogger<ExcluirEventoAgendaCommandHandler> logger)
    {
        _eventoAgendaRepository = eventoAgendaRepository;
        _logger = logger;
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

            return CommandResponse<ExcluirEventoAgendaCommandResponse>.Sucesso(string.Empty, HttpStatusCode.NoContent);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao excluir evento da agenda");
            return CommandResponse<ExcluirEventoAgendaCommandResponse>.ErroCritico(
                mensagem: $"Ocorreu um erro ao excluir o evento da agenda: {ex.Message}");
        }
    }
}
