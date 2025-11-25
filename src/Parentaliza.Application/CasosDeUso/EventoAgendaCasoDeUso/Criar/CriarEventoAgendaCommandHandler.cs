using MediatR;
using Microsoft.Extensions.Logging;
using Parentaliza.Application.Mediator;
using Parentaliza.Domain.Entidades;
using Parentaliza.Domain.InterfacesRepository;
using System.Net;

namespace Parentaliza.Application.CasosDeUso.EventoAgendaCasoDeUso.Criar;

public class CriarEventoAgendaCommandHandler : IRequestHandler<CriarEventoAgendaCommand, CommandResponse<CriarEventoAgendaCommandResponse>>
{
    private readonly IEventoAgendaRepository _eventoAgendaRepository;
    private readonly IResponsavelRepository _responsavelRepository;
    private readonly ILogger<CriarEventoAgendaCommandHandler> _logger;

    public CriarEventoAgendaCommandHandler(
        IEventoAgendaRepository eventoAgendaRepository,
        IResponsavelRepository responsavelRepository,
        ILogger<CriarEventoAgendaCommandHandler> logger)
    {
        _eventoAgendaRepository = eventoAgendaRepository;
        _responsavelRepository = responsavelRepository;
        _logger = logger;
    }
    public async Task<CommandResponse<CriarEventoAgendaCommandResponse>> Handle(CriarEventoAgendaCommand request, CancellationToken cancellationToken)
    {
        if (!request.Validar())
        {
            return CommandResponse<CriarEventoAgendaCommandResponse>.ErroValidacao(request.ResultadoDasValidacoes);
        }

        try
        {
            var responsavel = await _responsavelRepository.ObterPorId(request.ResponsavelId);
            if (responsavel == null)
            {
                return CommandResponse<CriarEventoAgendaCommandResponse>.AdicionarErro(
                    statusCode: HttpStatusCode.NotFound,
                    mensagem: "Responsável não encontrado.");
            }

            var nomeJaUtilizado = await _eventoAgendaRepository.NomeJaUtilizado(request.Evento);

            if (nomeJaUtilizado)
            {
                return CommandResponse<CriarEventoAgendaCommandResponse>.AdicionarErro(statusCode: HttpStatusCode.Conflict, mensagem: "O nome do evento ou consulta já está em uso.");
            }

            var eventoAgenda = new EventoAgenda(
                responsavelId: request.ResponsavelId,
                evento: request.Evento,
                especialidade: request.Especialidade,
                localizacao: request.Localizacao,
                hora: request.Hora,
                data: request.Data,
                anotacao: request.Anotacao);

            await _eventoAgendaRepository.Adicionar(eventoAgenda);

            var response = new CriarEventoAgendaCommandResponse(eventoAgenda.Id);


            return CommandResponse<CriarEventoAgendaCommandResponse>.Sucesso(response, statusCode: HttpStatusCode.Created);

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao criar evento/consulta");
            return CommandResponse<CriarEventoAgendaCommandResponse>.ErroCritico(mensagem: $"Ocorreu um erro ao criar o evento/consulta: {ex.Message}");
        }

    }
}