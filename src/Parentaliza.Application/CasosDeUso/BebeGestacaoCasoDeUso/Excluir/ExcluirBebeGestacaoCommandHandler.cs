using MediatR;
using Microsoft.Extensions.Logging;
using Parentaliza.Application.Mediator;
using Parentaliza.Domain.InterfacesRepository;
using System.Net;

namespace Parentaliza.Application.CasosDeUso.BebeGestacaoCasoDeUso.Excluir;

public class ExcluirBebeGestacaoCommandHandler : IRequestHandler<ExcluirBebeGestacaoCommand, CommandResponse<ExcluirBebeGestacaoCommandResponse>>
{
    private readonly IBebeGestacaoRepository _bebeGestacaoRepository;
    private readonly ILogger<ExcluirBebeGestacaoCommandHandler> _logger;

    public ExcluirBebeGestacaoCommandHandler(
        IBebeGestacaoRepository bebeGestacaoRepository,
        ILogger<ExcluirBebeGestacaoCommandHandler> logger)
    {
        _bebeGestacaoRepository = bebeGestacaoRepository;
        _logger = logger;
    }

    public async Task<CommandResponse<ExcluirBebeGestacaoCommandResponse>> Handle(ExcluirBebeGestacaoCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var bebeGestacao = await _bebeGestacaoRepository.ObterPorId(request.Id);

            if (bebeGestacao == null)
            {
                return CommandResponse<ExcluirBebeGestacaoCommandResponse>.AdicionarErro(
                    statusCode: HttpStatusCode.NotFound,
                    mensagem: "Bebê gestação não encontrado.");
            }

            await _bebeGestacaoRepository.Remover(request.Id);

            return CommandResponse<ExcluirBebeGestacaoCommandResponse>.Sucesso(string.Empty, HttpStatusCode.NoContent);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao excluir bebê em gestação");
            return CommandResponse<ExcluirBebeGestacaoCommandResponse>.ErroCritico(
                mensagem: $"Ocorreu um erro ao excluir o bebê gestação: {ex.Message}");
        }
    }
}
