using MediatR;
using Microsoft.Extensions.Logging;
using Parentaliza.Application.Mediator;
using Parentaliza.Domain.Entidades;
using Parentaliza.Domain.InterfacesRepository;
using System.Net;

namespace Parentaliza.Application.CasosDeUso.ExameRealizadoCasoDeUso.MarcarRealizado;

public class MarcarExameRealizadoCommandHandler : IRequestHandler<MarcarExameRealizadoCommand, CommandResponse<MarcarExameRealizadoCommandResponse>>
{
    private readonly IExameRealizadoRepository _exameRealizadoRepository;
    private readonly IBebeNascidoRepository _bebeNascidoRepository;
    private readonly IExameSusRepository _exameSusRepository;
    private readonly ILogger<MarcarExameRealizadoCommandHandler> _logger;

    public MarcarExameRealizadoCommandHandler(
        IExameRealizadoRepository exameRealizadoRepository,
        IBebeNascidoRepository bebeNascidoRepository,
        IExameSusRepository exameSusRepository,
        ILogger<MarcarExameRealizadoCommandHandler> logger)
    {
        _exameRealizadoRepository = exameRealizadoRepository;
        _bebeNascidoRepository = bebeNascidoRepository;
        _exameSusRepository = exameSusRepository;
        _logger = logger;
    }

    public async Task<CommandResponse<MarcarExameRealizadoCommandResponse>> Handle(MarcarExameRealizadoCommand request, CancellationToken cancellationToken)
    {
        if (!request.Validar())
        {
            return CommandResponse<MarcarExameRealizadoCommandResponse>.ErroValidacao(request.ResultadoDasValidacoes);
        }

        try
        {
            var bebe = await _bebeNascidoRepository.ObterPorId(request.BebeNascidoId);
            if (bebe == null)
            {
                return CommandResponse<MarcarExameRealizadoCommandResponse>.AdicionarErro(
                    statusCode: HttpStatusCode.NotFound,
                    mensagem: "Bebê não encontrado.");
            }

            var exameSus = await _exameSusRepository.ObterPorId(request.ExameSusId);
            if (exameSus == null)
            {
                return CommandResponse<MarcarExameRealizadoCommandResponse>.AdicionarErro(
                    statusCode: HttpStatusCode.NotFound,
                    mensagem: "Exame SUS não encontrado.");
            }

            var exameRealizado = await _exameRealizadoRepository.ObterExameRealizadoPorBebeEExame(request.BebeNascidoId, request.ExameSusId);

            if (exameRealizado == null)
            {
                exameRealizado = new ExameRealizado(
                    bebeNascidoId: request.BebeNascidoId,
                    exameSusId: request.ExameSusId,
                    dataRealizacao: request.DataRealizacao,
                    realizado: true,
                    observacoes: request.Observacoes
                );
                await _exameRealizadoRepository.Adicionar(exameRealizado);
            }
            else
            {
                exameRealizado.MarcarComoRealizado(request.DataRealizacao, request.Observacoes);
                await _exameRealizadoRepository.Atualizar(exameRealizado);
            }

            var response = new MarcarExameRealizadoCommandResponse(exameRealizado.Id);

            return CommandResponse<MarcarExameRealizadoCommandResponse>.Sucesso(response, HttpStatusCode.OK);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao marcar exame como realizado");
            return CommandResponse<MarcarExameRealizadoCommandResponse>.ErroCritico(
                mensagem: $"Ocorreu um erro ao marcar o exame como realizado: {ex.Message}");
        }
    }
}