using MediatR;
using Microsoft.Extensions.Logging;
using Parentaliza.Application.Mediator;
using Parentaliza.Domain.InterfacesRepository;
using System.Net;

namespace Parentaliza.Application.CasosDeUso.BebeGestacaoCasoDeUso.Obter;

public class ObterBebeGestacaoCommandHandler : IRequestHandler<ObterBebeGestacaoCommand, CommandResponse<ObterBebeGestacaoCommandResponse>>
{
    private readonly IBebeGestacaoRepository _bebeGestacaoRepository;
    private readonly ILogger<ObterBebeGestacaoCommandHandler> _logger;

    public ObterBebeGestacaoCommandHandler(
        IBebeGestacaoRepository bebeGestacaoRepository,
        ILogger<ObterBebeGestacaoCommandHandler> logger)
    {
        _bebeGestacaoRepository = bebeGestacaoRepository;
        _logger = logger;
    }

    public async Task<CommandResponse<ObterBebeGestacaoCommandResponse>> Handle (ObterBebeGestacaoCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var bebeGestacao = await _bebeGestacaoRepository.ObterPorId(request.Id);

            if (bebeGestacao == null)
            {
                return CommandResponse<ObterBebeGestacaoCommandResponse>.AdicionarErro(
                    statusCode: HttpStatusCode.NotFound,
                    mensagem: "Bebê não encontrado."
                    );
            }

            var bebeGestacaoResponse = new ObterBebeGestacaoCommandResponse(
                bebeGestacao.Id,
                bebeGestacao.Nome,
                bebeGestacao.DataPrevista,
                bebeGestacao.DiasDeGestacao,
                bebeGestacao.PesoEstimado,
                bebeGestacao.ComprimentoEstimado
                );

            return CommandResponse<ObterBebeGestacaoCommandResponse>.Sucesso(bebeGestacaoResponse, HttpStatusCode.OK);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao obter bebê em gestação");
            return CommandResponse<ObterBebeGestacaoCommandResponse>.ErroCritico(mensagem: $"Ocorreu um erro ao obter o bebê: {ex.Message}");
        }
    }
}