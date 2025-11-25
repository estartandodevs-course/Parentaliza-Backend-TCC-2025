using MediatR;
using Microsoft.Extensions.Logging;
using Parentaliza.Application.Mediator;
using Parentaliza.Domain.InterfacesRepository;
using System.Net;

namespace Parentaliza.Application.CasosDeUso.ControleFraldaCasoDeUso.ListarPorBebe;

public class ListarControlesFraldaPorBebeCommandHandler : IRequestHandler<ListarControlesFraldaPorBebeCommand, CommandResponse<List<ListarControlesFraldaPorBebeCommandResponse>>>
{
    private readonly IControleFraldaRepository _controleFraldaRepository;
    private readonly IBebeNascidoRepository _bebeNascidoRepository;
    private readonly ILogger<ListarControlesFraldaPorBebeCommandHandler> _logger;

    public ListarControlesFraldaPorBebeCommandHandler(
        IControleFraldaRepository controleFraldaRepository,
        IBebeNascidoRepository bebeNascidoRepository,
        ILogger<ListarControlesFraldaPorBebeCommandHandler> logger)
    {
        _controleFraldaRepository = controleFraldaRepository;
        _bebeNascidoRepository = bebeNascidoRepository;
        _logger = logger;
    }

    public async Task<CommandResponse<List<ListarControlesFraldaPorBebeCommandResponse>>> Handle(ListarControlesFraldaPorBebeCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var bebe = await _bebeNascidoRepository.ObterPorId(request.BebeNascidoId);
            if (bebe == null)
            {
                return CommandResponse<List<ListarControlesFraldaPorBebeCommandResponse>>.AdicionarErro(
                    statusCode: HttpStatusCode.NotFound,
                    mensagem: "Bebê não encontrado.");
            }

            var controles = await _controleFraldaRepository.ObterControlesPorBebe(request.BebeNascidoId);

            var response = controles.Select(controle => new ListarControlesFraldaPorBebeCommandResponse(
                controle.Id,
                controle.BebeNascidoId,
                controle.HoraTroca,
                controle.TipoFralda,
                controle.Observacoes
            )).ToList();

            return CommandResponse<List<ListarControlesFraldaPorBebeCommandResponse>>.Sucesso(response, HttpStatusCode.OK);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao listar controles de fralda por bebê");
            return CommandResponse<List<ListarControlesFraldaPorBebeCommandResponse>>.ErroCritico(
                mensagem: $"Ocorreu um erro ao listar os controles de fralda: {ex.Message}");
        }
    }
}

