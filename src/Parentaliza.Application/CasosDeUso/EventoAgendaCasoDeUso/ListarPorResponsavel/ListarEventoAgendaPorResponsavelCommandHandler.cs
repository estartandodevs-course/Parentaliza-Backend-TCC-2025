using MediatR;
using Microsoft.Extensions.Logging;
using Parentaliza.Application.Mediator;
using Parentaliza.Domain.InterfacesRepository;
using System.Net;

namespace Parentaliza.Application.CasosDeUso.EventoAgendaCasoDeUso.ListarPorResponsavel;

public class ListarEventoAgendaPorResponsavelCommandHandler : IRequestHandler<ListarEventoAgendaPorResponsavelCommand, CommandResponse<List<ListarEventoAgendaPorResponsavelCommandResponse>>>
{
    private readonly IEventoAgendaRepository _eventoAgendaRepository;
    private readonly IResponsavelRepository _responsavelRepository;
    private readonly ILogger<ListarEventoAgendaPorResponsavelCommandHandler> _logger;

    public ListarEventoAgendaPorResponsavelCommandHandler(
        IEventoAgendaRepository eventoAgendaRepository,
        IResponsavelRepository responsavelRepository,
        ILogger<ListarEventoAgendaPorResponsavelCommandHandler> logger)
    {
        _eventoAgendaRepository = eventoAgendaRepository;
        _responsavelRepository = responsavelRepository;
        _logger = logger;
    }

    public async Task<CommandResponse<List<ListarEventoAgendaPorResponsavelCommandResponse>>> Handle(
        ListarEventoAgendaPorResponsavelCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            var responsavel = await _responsavelRepository.ObterPorId(request.ResponsavelId);
            if (responsavel == null)
            {
                return CommandResponse<List<ListarEventoAgendaPorResponsavelCommandResponse>>.AdicionarErro(
                    statusCode: HttpStatusCode.NotFound,
                    mensagem: "Responsável não encontrado.");
            }

            var eventos = await _eventoAgendaRepository.ObterPorResponsavelId(request.ResponsavelId);

            var response = eventos.Select(evento => new ListarEventoAgendaPorResponsavelCommandResponse(
                evento.Id,
                evento.ResponsavelId,
                evento.Evento,
                evento.Especialidade,
                evento.Localizacao,
                evento.Data,
                evento.Hora,
                evento.Anotacao
            )).ToList();

            return CommandResponse<List<ListarEventoAgendaPorResponsavelCommandResponse>>.Sucesso(response, HttpStatusCode.OK);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao listar eventos do responsável");
            return CommandResponse<List<ListarEventoAgendaPorResponsavelCommandResponse>>.ErroCritico(
                mensagem: $"Ocorreu um erro ao listar os eventos do responsável: {ex.Message}");
        }
    }
}

