using MediatR;
using Microsoft.Extensions.Logging;
using Parentaliza.Application.Mediator;
using Parentaliza.Domain.InterfacesRepository;
using System.Net;

namespace Parentaliza.Application.CasosDeUso.BebeGestacaoCasoDeUso.ListarPorResponsavel;

public class ListarBebeGestacaoPorResponsavelCommandHandler : IRequestHandler<ListarBebeGestacaoPorResponsavelCommand, CommandResponse<List<ListarBebeGestacaoPorResponsavelCommandResponse>>>
{
    private readonly IBebeGestacaoRepository _bebeGestacaoRepository;
    private readonly IResponsavelRepository _responsavelRepository;
    private readonly ILogger<ListarBebeGestacaoPorResponsavelCommandHandler> _logger;

    public ListarBebeGestacaoPorResponsavelCommandHandler(
        IBebeGestacaoRepository bebeGestacaoRepository,
        IResponsavelRepository responsavelRepository,
        ILogger<ListarBebeGestacaoPorResponsavelCommandHandler> logger)
    {
        _bebeGestacaoRepository = bebeGestacaoRepository;
        _responsavelRepository = responsavelRepository;
        _logger = logger;
    }

    public async Task<CommandResponse<List<ListarBebeGestacaoPorResponsavelCommandResponse>>> Handle(
        ListarBebeGestacaoPorResponsavelCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            var responsavel = await _responsavelRepository.ObterPorId(request.ResponsavelId);
            if (responsavel == null)
            {
                return CommandResponse<List<ListarBebeGestacaoPorResponsavelCommandResponse>>.AdicionarErro(
                    statusCode: HttpStatusCode.NotFound,
                    mensagem: "Responsável não encontrado.");
            }

            var bebes = await _bebeGestacaoRepository.ObterPorResponsavelId(request.ResponsavelId);

            var response = bebes.Select(bebe => new ListarBebeGestacaoPorResponsavelCommandResponse(
                bebe.Id,
                bebe.ResponsavelId,
                bebe.Nome,
                bebe.DataPrevista,
                bebe.DiasDeGestacao,
                bebe.PesoEstimado,
                bebe.ComprimentoEstimado
            )).ToList();

            return CommandResponse<List<ListarBebeGestacaoPorResponsavelCommandResponse>>.Sucesso(response, HttpStatusCode.OK);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao listar bebês em gestação do responsável");
            return CommandResponse<List<ListarBebeGestacaoPorResponsavelCommandResponse>>.ErroCritico(
                mensagem: $"Ocorreu um erro ao listar os bebês em gestação do responsável: {ex.Message}");
        }
    }
}

