using MediatR;
using Microsoft.Extensions.Logging;
using Parentaliza.Application.Mediator;
using Parentaliza.Domain.Entidades;
using Parentaliza.Domain.InterfacesRepository;
using System.Net;

namespace Parentaliza.Application.CasosDeUso.ExameRealizadoCasoDeUso.Desmarcar;

public class DesmarcarExameRealizadoCommandHandler : IRequestHandler<DesmarcarExameRealizadoCommand, CommandResponse<DesmarcarExameRealizadoCommandResponse>>
{
    private readonly IExameRealizadoRepository _exameRealizadoRepository;
    private readonly IBebeNascidoRepository _bebeNascidoRepository;
    private readonly IExameSusRepository _exameSusRepository;
    private readonly ILogger<DesmarcarExameRealizadoCommandHandler> _logger;

    public DesmarcarExameRealizadoCommandHandler(
        IExameRealizadoRepository exameRealizadoRepository,
        IBebeNascidoRepository bebeNascidoRepository,
        IExameSusRepository exameSusRepository,
        ILogger<DesmarcarExameRealizadoCommandHandler> logger)
    {
        _exameRealizadoRepository = exameRealizadoRepository;
        _bebeNascidoRepository = bebeNascidoRepository;
        _exameSusRepository = exameSusRepository;
        _logger = logger;
    }

    public async Task<CommandResponse<DesmarcarExameRealizadoCommandResponse>> Handle(DesmarcarExameRealizadoCommand request, CancellationToken cancellationToken)
    {
        if (!request.Validar())
        {
            return CommandResponse<DesmarcarExameRealizadoCommandResponse>.ErroValidacao(request.ResultadoDasValidacoes);
        }

        try
        {
            var bebe = await _bebeNascidoRepository.ObterPorId(request.BebeNascidoId);
            if (bebe == null)
            {
                return CommandResponse<DesmarcarExameRealizadoCommandResponse>.AdicionarErro(
                    statusCode: HttpStatusCode.NotFound,
                    mensagem: "Bebê não encontrado.");
            }

            var exameSus = await _exameSusRepository.ObterPorId(request.ExameSusId);
            if (exameSus == null)
            {
                return CommandResponse<DesmarcarExameRealizadoCommandResponse>.AdicionarErro(
                    statusCode: HttpStatusCode.NotFound,
                    mensagem: "Exame SUS não encontrado.");
            }

            var exameRealizado = await _exameRealizadoRepository.ObterExameRealizadoPorBebeEExame(request.BebeNascidoId, request.ExameSusId);

            if (exameRealizado == null)
            {
                // Se não existe, cria um registro marcado como não realizado
                exameRealizado = new ExameRealizado(
                    bebeNascidoId: request.BebeNascidoId,
                    exameSusId: request.ExameSusId,
                    dataRealizacao: null,
                    realizado: false,
                    observacoes: null
                );
                await _exameRealizadoRepository.Adicionar(exameRealizado);
            }
            else
            {
                // Se existe, desmarca
                exameRealizado.MarcarComoNaoRealizado();
                await _exameRealizadoRepository.Atualizar(exameRealizado);
            }

            var response = new DesmarcarExameRealizadoCommandResponse(exameRealizado.Id);

            return CommandResponse<DesmarcarExameRealizadoCommandResponse>.Sucesso(response, HttpStatusCode.OK);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao desmarcar exame");
            return CommandResponse<DesmarcarExameRealizadoCommandResponse>.ErroCritico(
                mensagem: $"Ocorreu um erro ao desmarcar o exame: {ex.Message}");
        }
    }
}

