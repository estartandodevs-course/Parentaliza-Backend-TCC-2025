using MediatR;
using Microsoft.Extensions.Logging;
using Parentaliza.Application.Mediator;
using Parentaliza.Domain.Entidades;
using Parentaliza.Domain.InterfacesRepository;
using System.Net;
using System.Reflection;

namespace Parentaliza.Application.CasosDeUso.EventoAgendaCasoDeUso.Editar;

public class EditarEventoAgendaCommandHandler : IRequestHandler<EditarEventoAgendaCommand, CommandResponse<EditarEventoAgendaCommandResponse>>
{
    private readonly IEventoAgendaRepository _eventoAgendaRepository;
    private readonly IResponsavelRepository _responsavelRepository;
    private readonly ILogger<EditarEventoAgendaCommandHandler> _logger;

    public EditarEventoAgendaCommandHandler(
        IEventoAgendaRepository eventoAgendaRepository,
        IResponsavelRepository responsavelRepository,
        ILogger<EditarEventoAgendaCommandHandler> logger)
    {
        _eventoAgendaRepository = eventoAgendaRepository;
        _responsavelRepository = responsavelRepository;
        _logger = logger;
    }

    public async Task<CommandResponse<EditarEventoAgendaCommandResponse>> Handle(EditarEventoAgendaCommand request, CancellationToken cancellationToken)
    {
        if (!request.Validar())
        {
            return CommandResponse<EditarEventoAgendaCommandResponse>.ErroValidacao(request.ResultadoDasValidacoes);
        }

        try
        {
            var eventoAgenda = await _eventoAgendaRepository.ObterPorId(request.Id);

            if (eventoAgenda == null)
            {
                return CommandResponse<EditarEventoAgendaCommandResponse>.AdicionarErro(
                    statusCode: HttpStatusCode.NotFound,
                    mensagem: "Evento da agenda não encontrado.");
            }

            var responsavel = await _responsavelRepository.ObterPorId(request.ResponsavelId);
            if (responsavel == null)
            {
                return CommandResponse<EditarEventoAgendaCommandResponse>.AdicionarErro(
                    statusCode: HttpStatusCode.NotFound,
                    mensagem: "Responsável não encontrado.");
            }

            if (eventoAgenda.Evento?.ToLower() != request.Evento?.ToLower())
            {
                var nomeJaUtilizado = await _eventoAgendaRepository.NomeJaUtilizado(request.Evento);
                if (nomeJaUtilizado)
                {
                    return CommandResponse<EditarEventoAgendaCommandResponse>.AdicionarErro(
                        statusCode: HttpStatusCode.Conflict,
                        mensagem: "O nome do evento ou consulta já está em uso.");
                }
            }

            // Atualizar a entidade existente usando reflection para acessar propriedades privadas
            var tipo = typeof(EventoAgenda);
            tipo.GetProperty(nameof(EventoAgenda.ResponsavelId))?.SetValue(eventoAgenda, request.ResponsavelId);
            tipo.GetProperty(nameof(EventoAgenda.Evento))?.SetValue(eventoAgenda, request.Evento);
            tipo.GetProperty(nameof(EventoAgenda.Especialidade))?.SetValue(eventoAgenda, request.Especialidade);
            tipo.GetProperty(nameof(EventoAgenda.Localizacao))?.SetValue(eventoAgenda, request.Localizacao);
            tipo.GetProperty(nameof(EventoAgenda.Data))?.SetValue(eventoAgenda, request.Data);
            tipo.GetProperty(nameof(EventoAgenda.Hora))?.SetValue(eventoAgenda, request.Hora);
            tipo.GetProperty(nameof(EventoAgenda.Anotacao))?.SetValue(eventoAgenda, request.Anotacao);

            await _eventoAgendaRepository.Atualizar(eventoAgenda);

            var response = new EditarEventoAgendaCommandResponse(eventoAgenda.Id);

            return CommandResponse<EditarEventoAgendaCommandResponse>.Sucesso(response, HttpStatusCode.OK);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao editar evento da agenda");
            return CommandResponse<EditarEventoAgendaCommandResponse>.ErroCritico(
                mensagem: $"Ocorreu um erro ao editar o evento da agenda: {ex.Message}");
        }
    }
}