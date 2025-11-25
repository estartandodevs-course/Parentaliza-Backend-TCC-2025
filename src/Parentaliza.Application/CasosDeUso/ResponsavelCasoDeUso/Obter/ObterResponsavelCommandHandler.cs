using MediatR;
using Microsoft.Extensions.Logging;
using Parentaliza.Application.Mediator;
using Parentaliza.Domain.InterfacesRepository;
using System.Net;

namespace Parentaliza.Application.CasosDeUso.ResponsavelCasoDeUso.Obter;

public class ObterResponsavelCommandHandler : IRequestHandler<ObterResponsavelCommand, CommandResponse<ObterResponsavelCommandResponse>>
{
    private readonly IResponsavelRepository _responsavelRepository;
    private readonly ILogger<ObterResponsavelCommandHandler> _logger;

    public ObterResponsavelCommandHandler(
        IResponsavelRepository responsavelRepository,
        ILogger<ObterResponsavelCommandHandler> logger)
    {
        _responsavelRepository = responsavelRepository;
        _logger = logger;
    }

    public async Task<CommandResponse<ObterResponsavelCommandResponse>> Handle(ObterResponsavelCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var responsavel = await _responsavelRepository.ObterPorId(request.Id);

            if (responsavel == null)
            {
                return CommandResponse<ObterResponsavelCommandResponse>.AdicionarErro(
                    statusCode: HttpStatusCode.NotFound,
                    mensagem: "Responsável não encontrado.");
            }

            var response = new ObterResponsavelCommandResponse(
                responsavel.Id,
                responsavel.Nome,
                responsavel.Email,
                responsavel.TipoResponsavel,
                responsavel.FaseNascimento
            );

            return CommandResponse<ObterResponsavelCommandResponse>.Sucesso(response, HttpStatusCode.OK);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao obter responsável");
            return CommandResponse<ObterResponsavelCommandResponse>.ErroCritico(
                mensagem: $"Ocorreu um erro ao obter o responsável: {ex.Message}");
        }
    }
}

