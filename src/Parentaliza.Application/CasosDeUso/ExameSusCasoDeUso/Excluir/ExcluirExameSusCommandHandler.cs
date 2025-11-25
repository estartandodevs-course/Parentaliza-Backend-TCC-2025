using MediatR;
using Microsoft.Extensions.Logging;
using Parentaliza.Application.Mediator;
using Parentaliza.Domain.InterfacesRepository;
using System.Net;

namespace Parentaliza.Application.CasosDeUso.ExameSusCasoDeUso.Excluir;

public class ExcluirExameSusCommandHandler : IRequestHandler<ExcluirExameSusCommand, CommandResponse<ExcluirExameSusCommandResponse>>
{
    private readonly IExameSusRepository _exameSusRepository;
    private readonly ILogger<ExcluirExameSusCommandHandler> _logger;

    public ExcluirExameSusCommandHandler(
        IExameSusRepository exameSusRepository,
        ILogger<ExcluirExameSusCommandHandler> logger)
    {
        _exameSusRepository = exameSusRepository;
        _logger = logger;
    }

    public async Task<CommandResponse<ExcluirExameSusCommandResponse>> Handle(ExcluirExameSusCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var exameSus = await _exameSusRepository.ObterPorId(request.Id);

            if (exameSus == null)
            {
                return CommandResponse<ExcluirExameSusCommandResponse>.AdicionarErro(
                    statusCode: HttpStatusCode.NotFound,
                    mensagem: "Exame SUS não encontrado.");
            }

            await _exameSusRepository.Remover(request.Id);

            return CommandResponse<ExcluirExameSusCommandResponse>.Sucesso(string.Empty, HttpStatusCode.NoContent);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao excluir exame SUS");
            return CommandResponse<ExcluirExameSusCommandResponse>.ErroCritico(
                mensagem: $"Ocorreu um erro ao excluir o exame SUS: {ex.Message}");
        }
    }
}

