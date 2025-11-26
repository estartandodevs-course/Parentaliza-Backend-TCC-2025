using MediatR;
using Microsoft.Extensions.Logging;
using Parentaliza.Application.Mediator;
using Parentaliza.Domain.InterfacesRepository;
using System.Net;

namespace Parentaliza.Application.CasosDeUso.ResponsavelCasoDeUso.Excluir;

public class ExcluirResponsavelCommandHandler : IRequestHandler<ExcluirResponsavelCommand, CommandResponse<ExcluirResponsavelCommandResponse>>
{
    private readonly IResponsavelRepository _responsavelRepository;
    private readonly ILogger<ExcluirResponsavelCommandHandler> _logger;

    public ExcluirResponsavelCommandHandler(
        IResponsavelRepository responsavelRepository,
        ILogger<ExcluirResponsavelCommandHandler> logger)
    {
        _responsavelRepository = responsavelRepository;
        _logger = logger;
    }

    public async Task<CommandResponse<ExcluirResponsavelCommandResponse>> Handle(ExcluirResponsavelCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var responsavel = await _responsavelRepository.ObterPorId(request.Id);

            if (responsavel == null)
            {
                return CommandResponse<ExcluirResponsavelCommandResponse>.AdicionarErro(
                    statusCode: HttpStatusCode.NotFound,
                    mensagem: "Responsável não encontrado.");
            }

            await _responsavelRepository.Remover(request.Id);

            return CommandResponse<ExcluirResponsavelCommandResponse>.Sucesso(string.Empty, HttpStatusCode.NoContent);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao excluir responsável");
            return CommandResponse<ExcluirResponsavelCommandResponse>.ErroCritico(
                mensagem: $"Ocorreu um erro ao excluir o responsável: {ex.Message}");
        }
    }
}