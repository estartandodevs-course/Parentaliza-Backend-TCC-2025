using MediatR;
using Microsoft.Extensions.Logging;
using Parentaliza.Application.Mediator;
using Parentaliza.Domain.InterfacesRepository;
using System.Net;

namespace Parentaliza.Application.CasosDeUso.VacinaAplicadaCasoDeUso.ListarPorBebe;

public class ListarVacinasPorBebeCommandHandler : IRequestHandler<ListarVacinasPorBebeCommand, CommandResponse<List<ListarVacinasPorBebeCommandResponse>>>
{
    private readonly IVacinaAplicadaRepository _vacinaAplicadaRepository;
    private readonly IVacinaSusRepository _vacinaSusRepository;
    private readonly IBebeNascidoRepository _bebeNascidoRepository;
    private readonly ILogger<ListarVacinasPorBebeCommandHandler> _logger;

    public ListarVacinasPorBebeCommandHandler(
        IVacinaAplicadaRepository vacinaAplicadaRepository,
        IVacinaSusRepository vacinaSusRepository,
        IBebeNascidoRepository bebeNascidoRepository,
        ILogger<ListarVacinasPorBebeCommandHandler> logger)
    {
        _vacinaAplicadaRepository = vacinaAplicadaRepository;
        _vacinaSusRepository = vacinaSusRepository;
        _bebeNascidoRepository = bebeNascidoRepository;
        _logger = logger;
    }

    public async Task<CommandResponse<List<ListarVacinasPorBebeCommandResponse>>> Handle(ListarVacinasPorBebeCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var bebe = await _bebeNascidoRepository.ObterPorId(request.BebeNascidoId);
            if (bebe == null)
            {
                return CommandResponse<List<ListarVacinasPorBebeCommandResponse>>.AdicionarErro(
                    statusCode: HttpStatusCode.NotFound,
                    mensagem: "Bebê não encontrado.");
            }

            // Busca todas as vacinas SUS disponíveis
            var todasVacinasSus = await _vacinaSusRepository.ObterTodos();
            
            // Busca as vacinas aplicadas pelo bebê
            var vacinasAplicadas = await _vacinaAplicadaRepository.ObterVacinasPorBebe(request.BebeNascidoId);
            var vacinasAplicadasDict = vacinasAplicadas.ToDictionary(v => v.VacinaSusId);

            // Cria a resposta combinando todas as vacinas SUS com o status de aplicação
            var response = todasVacinasSus.Select(vacinaSus =>
            {
                var vacinaAplicada = vacinasAplicadasDict.GetValueOrDefault(vacinaSus.Id);
                return new ListarVacinasPorBebeCommandResponse(
                    vacinaSusId: vacinaSus.Id,
                    nomeVacina: vacinaSus.NomeVacina,
                    descricao: vacinaSus.Descricao,
                    categoriaFaixaEtaria: vacinaSus.CategoriaFaixaEtaria,
                    idadeMinMeses: vacinaSus.IdadeMinMesesVacina,
                    idadeMaxMeses: vacinaSus.IdadeMaxMesesVacina,
                    aplicada: vacinaAplicada?.Aplicada ?? false,
                    dataAplicacao: vacinaAplicada?.DataAplicacao,
                    lote: vacinaAplicada?.Lote,
                    localAplicacao: vacinaAplicada?.LocalAplicacao,
                    observacoes: vacinaAplicada?.Observacoes
                );
            }).ToList();

            return CommandResponse<List<ListarVacinasPorBebeCommandResponse>>.Sucesso(response, HttpStatusCode.OK);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao listar vacinas por bebê");
            return CommandResponse<List<ListarVacinasPorBebeCommandResponse>>.ErroCritico(
                mensagem: $"Ocorreu um erro ao listar as vacinas: {ex.Message}");
        }
    }
}

