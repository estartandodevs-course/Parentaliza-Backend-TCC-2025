using MediatR;
using Microsoft.Extensions.Logging;
using Parentaliza.Application.Mediator;
using Parentaliza.Domain.InterfacesRepository;
using System.Net;

namespace Parentaliza.Application.CasosDeUso.ExameSusCasoDeUso.Listar;

public class ListarExameSusCommandHandler : IRequestHandler<ListarExameSusCommand, CommandResponse<List<ListarExameSusCommandResponse>>>
{
    private readonly IExameSusRepository _exameSusRepository;
    private readonly ILogger<ListarExameSusCommandHandler> _logger;

    public ListarExameSusCommandHandler(
        IExameSusRepository exameSusRepository,
        ILogger<ListarExameSusCommandHandler> logger)
    {
        _exameSusRepository = exameSusRepository;
        _logger = logger;
    }

    public async Task<CommandResponse<List<ListarExameSusCommandResponse>>> Handle(ListarExameSusCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var todosExames = await _exameSusRepository.ObterTodos();
            var response = todosExames.Select(exame => new ListarExameSusCommandResponse(
                exame.Id,
                exame.NomeExame,
                exame.Descricao,
                exame.CategoriaFaixaEtaria,
                exame.IdadeMinMesesExame,
                exame.IdadeMaxMesesExame
            )).ToList();

            return CommandResponse<List<ListarExameSusCommandResponse>>.Sucesso(response, HttpStatusCode.OK);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao listar exames SUS");
            return CommandResponse<List<ListarExameSusCommandResponse>>.ErroCritico(
                mensagem: $"Ocorreu um erro ao listar os exames SUS: {ex.Message}");
        }
    }
}

