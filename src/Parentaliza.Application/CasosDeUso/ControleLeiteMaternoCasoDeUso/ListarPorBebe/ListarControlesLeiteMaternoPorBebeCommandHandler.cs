using MediatR;
using Microsoft.Extensions.Logging;
using Parentaliza.Application.Mediator;
using Parentaliza.Domain.InterfacesRepository;
using System.Net;

namespace Parentaliza.Application.CasosDeUso.ControleLeiteMaternoCasoDeUso.ListarPorBebe;

public class ListarControlesLeiteMaternoPorBebeCommandHandler : IRequestHandler<ListarControlesLeiteMaternoPorBebeCommand, CommandResponse<List<ListarControlesLeiteMaternoPorBebeCommandResponse>>>
{
    private readonly IControleLeiteMaternoRepository _controleLeiteMaternoRepository;
    private readonly IBebeNascidoRepository _bebeNascidoRepository;
    private readonly ILogger<ListarControlesLeiteMaternoPorBebeCommandHandler> _logger;

    public ListarControlesLeiteMaternoPorBebeCommandHandler(
        IControleLeiteMaternoRepository controleLeiteMaternoRepository,
        IBebeNascidoRepository bebeNascidoRepository,
        ILogger<ListarControlesLeiteMaternoPorBebeCommandHandler> logger)
    {
        _controleLeiteMaternoRepository = controleLeiteMaternoRepository;
        _bebeNascidoRepository = bebeNascidoRepository;
        _logger = logger;
    }

    public async Task<CommandResponse<List<ListarControlesLeiteMaternoPorBebeCommandResponse>>> Handle(ListarControlesLeiteMaternoPorBebeCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var bebe = await _bebeNascidoRepository.ObterPorId(request.BebeNascidoId);
            if (bebe == null)
            {
                return CommandResponse<List<ListarControlesLeiteMaternoPorBebeCommandResponse>>.AdicionarErro(
                    statusCode: HttpStatusCode.NotFound,
                    mensagem: "Bebê não encontrado.");
            }

            var controles = await _controleLeiteMaternoRepository.ObterControlesPorBebe(request.BebeNascidoId);

            var response = controles.Select(controle => new ListarControlesLeiteMaternoPorBebeCommandResponse(
                controle.Id,
                controle.BebeNascidoId,
                controle.Cronometro,
                controle.LadoDireito,
                controle.LadoEsquerdo
            )).ToList();

            return CommandResponse<List<ListarControlesLeiteMaternoPorBebeCommandResponse>>.Sucesso(response, HttpStatusCode.OK);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao listar controles de leite materno por bebê");
            return CommandResponse<List<ListarControlesLeiteMaternoPorBebeCommandResponse>>.ErroCritico(
                mensagem: $"Ocorreu um erro ao listar os controles de leite materno: {ex.Message}");
        }
    }
}

