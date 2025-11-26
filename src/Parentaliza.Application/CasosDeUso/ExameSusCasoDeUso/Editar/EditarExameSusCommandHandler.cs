using MediatR;
using Microsoft.Extensions.Logging;
using Parentaliza.Application.Mediator;
using Parentaliza.Domain.Entidades;
using Parentaliza.Domain.InterfacesRepository;
using System.Net;

namespace Parentaliza.Application.CasosDeUso.ExameSusCasoDeUso.Editar;

public class EditarExameSusCommandHandler : IRequestHandler<EditarExameSusCommand, CommandResponse<EditarExameSusCommandResponse>>
{
    private readonly IExameSusRepository _exameSusRepository;
    private readonly ILogger<EditarExameSusCommandHandler> _logger;

    public EditarExameSusCommandHandler(
        IExameSusRepository exameSusRepository,
        ILogger<EditarExameSusCommandHandler> logger)
    {
        _exameSusRepository = exameSusRepository;
        _logger = logger;
    }

    public async Task<CommandResponse<EditarExameSusCommandResponse>> Handle(EditarExameSusCommand request, CancellationToken cancellationToken)
    {
        if (!request.Validar())
        {
            return CommandResponse<EditarExameSusCommandResponse>.ErroValidacao(request.ResultadoDasValidacoes);
        }

        try
        {
            var exameSus = await _exameSusRepository.ObterPorId(request.Id);

            if (exameSus == null)
            {
                return CommandResponse<EditarExameSusCommandResponse>.AdicionarErro(
                    statusCode: HttpStatusCode.NotFound,
                    mensagem: "Exame SUS não encontrado.");
            }

            var tipo = typeof(ExameSus);
            tipo.GetProperty(nameof(ExameSus.NomeExame))?.SetValue(exameSus, request.NomeExame);
            tipo.GetProperty(nameof(ExameSus.Descricao))?.SetValue(exameSus, request.Descricao);
            tipo.GetProperty(nameof(ExameSus.CategoriaFaixaEtaria))?.SetValue(exameSus, request.CategoriaFaixaEtaria);
            tipo.GetProperty(nameof(ExameSus.IdadeMinMesesExame))?.SetValue(exameSus, request.IdadeMinMesesExame);
            tipo.GetProperty(nameof(ExameSus.IdadeMaxMesesExame))?.SetValue(exameSus, request.IdadeMaxMesesExame);

            await _exameSusRepository.Atualizar(exameSus);

            var response = new EditarExameSusCommandResponse(exameSus.Id);

            return CommandResponse<EditarExameSusCommandResponse>.Sucesso(response, HttpStatusCode.OK);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao editar exame SUS");
            return CommandResponse<EditarExameSusCommandResponse>.ErroCritico(
                mensagem: $"Ocorreu um erro ao editar o exame SUS: {ex.Message}");
        }
    }
}