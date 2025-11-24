using MediatR;
using Microsoft.Extensions.Logging;
using Parentaliza.Application.Mediator;
using Parentaliza.Domain.InterfacesRepository;
using System.Net;

namespace Parentaliza.Application.CasosDeUso.BebeNascidoCasoDeUso.Excluir;

public class ExcluirBebeNascidoCommandHandler : IRequestHandler<ExcluirBebeNascidoCommand, CommandResponse<ExcluirBebeNascidoCommandResponse>>
{
    private readonly IBebeNascidoRepository _bebeNascidoRepository;
    private readonly ILogger<ExcluirBebeNascidoCommandHandler> _logger;

    public ExcluirBebeNascidoCommandHandler(
        IBebeNascidoRepository bebeNascidoRepository,
        ILogger<ExcluirBebeNascidoCommandHandler> logger)
    {
        _bebeNascidoRepository = bebeNascidoRepository;
        _logger = logger;
    }

    public async Task<CommandResponse<ExcluirBebeNascidoCommandResponse>> Handle(ExcluirBebeNascidoCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var bebeNascido = await _bebeNascidoRepository.ObterPorId(request.Id);

            if (bebeNascido == null)
            {
                return CommandResponse<ExcluirBebeNascidoCommandResponse>.AdicionarErro(
                    statusCode: HttpStatusCode.NotFound,
                    mensagem: "Bebê nascido não encontrado.");
            }

            await _bebeNascidoRepository.Remover(request.Id);

            return CommandResponse<ExcluirBebeNascidoCommandResponse>.Sucesso(string.Empty, HttpStatusCode.NoContent);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao excluir bebê nascido");
            return CommandResponse<ExcluirBebeNascidoCommandResponse>.ErroCritico(
                mensagem: $"Ocorreu um erro ao excluir o bebê nascido: {ex.Message}");
        }
    }
}
