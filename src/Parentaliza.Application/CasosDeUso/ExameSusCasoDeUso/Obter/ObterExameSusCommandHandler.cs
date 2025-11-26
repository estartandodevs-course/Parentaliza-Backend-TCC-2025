using MediatR;
using Microsoft.Extensions.Logging;
using Parentaliza.Application.Mediator;
using Parentaliza.Domain.InterfacesRepository;
using System.Net;

namespace Parentaliza.Application.CasosDeUso.ExameSusCasoDeUso.Obter;

public class ObterExameSusCommandHandler : IRequestHandler<ObterExameSusCommand, CommandResponse<ObterExameSusCommandResponse>>
{
    private readonly IExameSusRepository _exameSusRepository;
    private readonly ILogger<ObterExameSusCommandHandler> _logger;

    public ObterExameSusCommandHandler(
        IExameSusRepository exameSusRepository,
        ILogger<ObterExameSusCommandHandler> logger)
    {
        _exameSusRepository = exameSusRepository;
        _logger = logger;
    }

    public async Task<CommandResponse<ObterExameSusCommandResponse>> Handle(ObterExameSusCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var exameSus = await _exameSusRepository.ObterPorId(request.Id);

            if (exameSus == null)
            {
                return CommandResponse<ObterExameSusCommandResponse>.AdicionarErro(
                    statusCode: HttpStatusCode.NotFound,
                    mensagem: "Exame SUS não encontrado.");
            }

            var response = new ObterExameSusCommandResponse(
                exameSus.Id,
                exameSus.NomeExame,
                exameSus.Descricao,
                exameSus.CategoriaFaixaEtaria,
                exameSus.IdadeMinMesesExame,
                exameSus.IdadeMaxMesesExame
            );

            return CommandResponse<ObterExameSusCommandResponse>.Sucesso(response, HttpStatusCode.OK);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao obter exame SUS");
            return CommandResponse<ObterExameSusCommandResponse>.ErroCritico(
                mensagem: $"Ocorreu um erro ao obter o exame SUS: {ex.Message}");
        }
    }
}