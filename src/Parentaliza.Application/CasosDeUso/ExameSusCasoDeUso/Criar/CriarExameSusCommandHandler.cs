using MediatR;
using Microsoft.Extensions.Logging;
using Parentaliza.Application.Mediator;
using Parentaliza.Domain.Entidades;
using Parentaliza.Domain.InterfacesRepository;
using System.Net;

namespace Parentaliza.Application.CasosDeUso.ExameSusCasoDeUso.Criar;

public class CriarExameSusCommandHandler : IRequestHandler<CriarExameSusCommand, CommandResponse<CriarExameSusCommandResponse>>
{
    private readonly IExameSusRepository _exameSusRepository;
    private readonly ILogger<CriarExameSusCommandHandler> _logger;

    public CriarExameSusCommandHandler(
        IExameSusRepository exameSusRepository,
        ILogger<CriarExameSusCommandHandler> logger)
    {
        _exameSusRepository = exameSusRepository;
        _logger = logger;
    }

    public async Task<CommandResponse<CriarExameSusCommandResponse>> Handle(CriarExameSusCommand request, CancellationToken cancellationToken)
    {
        if (!request.Validar())
        {
            return CommandResponse<CriarExameSusCommandResponse>.ErroValidacao(request.ResultadoDasValidacoes);
        }

        try
        {
            var exameSus = new ExameSus(
                nomeExame: request.NomeExame,
                descricao: request.Descricao,
                categoriaFaixaEtaria: request.CategoriaFaixaEtaria,
                idadeMinMesesExame: request.IdadeMinMesesExame,
                idadeMaxMesesExame: request.IdadeMaxMesesExame
            );

            await _exameSusRepository.Adicionar(exameSus);

            var response = new CriarExameSusCommandResponse(exameSus.Id);

            return CommandResponse<CriarExameSusCommandResponse>.Sucesso(response, HttpStatusCode.Created);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao criar exame SUS");
            return CommandResponse<CriarExameSusCommandResponse>.ErroCritico(
                mensagem: $"Ocorreu um erro ao criar o exame SUS: {ex.Message}");
        }
    }
}