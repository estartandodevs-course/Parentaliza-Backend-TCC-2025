using MediatR;
using Microsoft.Extensions.Logging;
using Parentaliza.Application.Mediator;
using Parentaliza.Domain.InterfacesRepository;
using System.Net;

namespace Parentaliza.Application.CasosDeUso.ControleMamadeiraCasoDeUso.ListarPorBebe;

public class ListarControlesMamadeiraPorBebeCommandHandler : IRequestHandler<ListarControlesMamadeiraPorBebeCommand, CommandResponse<List<ListarControlesMamadeiraPorBebeCommandResponse>>>
{
    private readonly IControleMamadeiraRepository _controleMamadeiraRepository;
    private readonly IBebeNascidoRepository _bebeNascidoRepository;
    private readonly ILogger<ListarControlesMamadeiraPorBebeCommandHandler> _logger;

    public ListarControlesMamadeiraPorBebeCommandHandler(
        IControleMamadeiraRepository controleMamadeiraRepository,
        IBebeNascidoRepository bebeNascidoRepository,
        ILogger<ListarControlesMamadeiraPorBebeCommandHandler> logger)
    {
        _controleMamadeiraRepository = controleMamadeiraRepository;
        _bebeNascidoRepository = bebeNascidoRepository;
        _logger = logger;
    }

    public async Task<CommandResponse<List<ListarControlesMamadeiraPorBebeCommandResponse>>> Handle(ListarControlesMamadeiraPorBebeCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var bebe = await _bebeNascidoRepository.ObterPorId(request.BebeNascidoId);
            if (bebe == null)
            {
                return CommandResponse<List<ListarControlesMamadeiraPorBebeCommandResponse>>.AdicionarErro(
                    statusCode: HttpStatusCode.NotFound,
                    mensagem: "Bebê não encontrado.");
            }

            var controles = await _controleMamadeiraRepository.ObterControlesPorBebe(request.BebeNascidoId);

            var response = controles.Select(controle => new ListarControlesMamadeiraPorBebeCommandResponse(
                controle.Id,
                controle.BebeNascidoId,
                controle.Data,
                controle.Hora,
                controle.QuantidadeLeite,
                controle.Anotacao
            )).ToList();

            return CommandResponse<List<ListarControlesMamadeiraPorBebeCommandResponse>>.Sucesso(response, HttpStatusCode.OK);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao listar controles de mamadeira por bebê");
            return CommandResponse<List<ListarControlesMamadeiraPorBebeCommandResponse>>.ErroCritico(
                mensagem: $"Ocorreu um erro ao listar os controles de mamadeira: {ex.Message}");
        }
    }
}