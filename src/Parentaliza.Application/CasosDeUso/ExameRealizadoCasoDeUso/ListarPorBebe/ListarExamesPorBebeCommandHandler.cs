using MediatR;
using Microsoft.Extensions.Logging;
using Parentaliza.Application.Mediator;
using Parentaliza.Domain.InterfacesRepository;
using System.Net;

namespace Parentaliza.Application.CasosDeUso.ExameRealizadoCasoDeUso.ListarPorBebe;

public class ListarExamesPorBebeCommandHandler : IRequestHandler<ListarExamesPorBebeCommand, CommandResponse<List<ListarExamesPorBebeCommandResponse>>>
{
    private readonly IExameRealizadoRepository _exameRealizadoRepository;
    private readonly IExameSusRepository _exameSusRepository;
    private readonly IBebeNascidoRepository _bebeNascidoRepository;
    private readonly ILogger<ListarExamesPorBebeCommandHandler> _logger;

    public ListarExamesPorBebeCommandHandler(
        IExameRealizadoRepository exameRealizadoRepository,
        IExameSusRepository exameSusRepository,
        IBebeNascidoRepository bebeNascidoRepository,
        ILogger<ListarExamesPorBebeCommandHandler> logger)
    {
        _exameRealizadoRepository = exameRealizadoRepository;
        _exameSusRepository = exameSusRepository;
        _bebeNascidoRepository = bebeNascidoRepository;
        _logger = logger;
    }

    public async Task<CommandResponse<List<ListarExamesPorBebeCommandResponse>>> Handle(ListarExamesPorBebeCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var bebe = await _bebeNascidoRepository.ObterPorId(request.BebeNascidoId);
            if (bebe == null)
            {
                return CommandResponse<List<ListarExamesPorBebeCommandResponse>>.AdicionarErro(
                    statusCode: HttpStatusCode.NotFound,
                    mensagem: "Bebê não encontrado.");
            }

            // Busca todos os exames SUS disponíveis
            var todosExamesSus = await _exameSusRepository.ObterTodos();
            
            // Busca os exames realizados pelo bebê
            var examesRealizados = await _exameRealizadoRepository.ObterExamesPorBebe(request.BebeNascidoId);
            var examesRealizadosDict = examesRealizados.ToDictionary(e => e.ExameSusId);

            // Cria a resposta combinando todos os exames SUS com o status de realização
            var response = todosExamesSus.Select(exameSus =>
            {
                var exameRealizado = examesRealizadosDict.GetValueOrDefault(exameSus.Id);
                return new ListarExamesPorBebeCommandResponse(
                    exameSusId: exameSus.Id,
                    nomeExame: exameSus.NomeExame,
                    categoriaFaixaEtaria: exameSus.CategoriaFaixaEtaria,
                    idadeMinMeses: exameSus.IdadeMinMesesExame,
                    idadeMaxMeses: exameSus.IdadeMaxMesesExame,
                    realizado: exameRealizado?.Realizado ?? false,
                    dataRealizacao: exameRealizado?.DataRealizacao,
                    observacoes: exameRealizado?.Observacoes
                );
            }).ToList();

            return CommandResponse<List<ListarExamesPorBebeCommandResponse>>.Sucesso(response, HttpStatusCode.OK);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao listar exames por bebê");
            return CommandResponse<List<ListarExamesPorBebeCommandResponse>>.ErroCritico(
                mensagem: $"Ocorreu um erro ao listar os exames: {ex.Message}");
        }
    }
}

