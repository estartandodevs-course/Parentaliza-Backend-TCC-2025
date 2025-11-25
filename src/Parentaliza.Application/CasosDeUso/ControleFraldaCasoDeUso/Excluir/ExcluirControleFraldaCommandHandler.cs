using MediatR;
using Microsoft.Extensions.Logging;
using Parentaliza.Application.Mediator;
using Parentaliza.Domain.InterfacesRepository;
using System.Net;

namespace Parentaliza.Application.CasosDeUso.ControleFraldaCasoDeUso.Excluir;

public class ExcluirControleFraldaCommandHandler : IRequestHandler<ExcluirControleFraldaCommand, CommandResponse<Unit>>
{
    private readonly IControleFraldaRepository _controleFraldaRepository;
    private readonly ILogger<ExcluirControleFraldaCommandHandler> _logger;

    public ExcluirControleFraldaCommandHandler(
        IControleFraldaRepository controleFraldaRepository,
        ILogger<ExcluirControleFraldaCommandHandler> logger)
    {
        _controleFraldaRepository = controleFraldaRepository;
        _logger = logger;
    }

    public async Task<CommandResponse<Unit>> Handle(ExcluirControleFraldaCommand request, CancellationToken cancellationToken)
    {
        if (!request.Validar())
        {
            return CommandResponse<Unit>.ErroValidacao(request.ResultadoDasValidacoes);
        }

        try
        {
            var controleFralda = await _controleFraldaRepository.ObterPorId(request.Id);

            if (controleFralda == null)
            {
                return CommandResponse<Unit>.AdicionarErro(
                    statusCode: HttpStatusCode.NotFound,
                    mensagem: "Controle de fralda não encontrado.");
            }

            await _controleFraldaRepository.Remover(request.Id);

            return CommandResponse<Unit>.Sucesso(Unit.Value, HttpStatusCode.NoContent);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao excluir controle de fralda");
            return CommandResponse<Unit>.ErroCritico(
                mensagem: $"Ocorreu um erro ao excluir o controle de fralda: {ex.Message}");
        }
    }
}

