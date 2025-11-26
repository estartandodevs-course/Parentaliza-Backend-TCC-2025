using MediatR;
using Microsoft.Extensions.Logging;
using Parentaliza.Application.Mediator;
using Parentaliza.Domain.InterfacesRepository;
using System.Net;

namespace Parentaliza.Application.CasosDeUso.ControleFraldaCasoDeUso.Obter;

public class ObterControleFraldaCommandHandler : IRequestHandler<ObterControleFraldaCommand, CommandResponse<ObterControleFraldaCommandResponse>>
{
    private readonly IControleFraldaRepository _controleFraldaRepository;
    private readonly ILogger<ObterControleFraldaCommandHandler> _logger;

    public ObterControleFraldaCommandHandler(
        IControleFraldaRepository controleFraldaRepository,
        ILogger<ObterControleFraldaCommandHandler> logger)
    {
        _controleFraldaRepository = controleFraldaRepository;
        _logger = logger;
    }

    public async Task<CommandResponse<ObterControleFraldaCommandResponse>> Handle(ObterControleFraldaCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var controleFralda = await _controleFraldaRepository.ObterPorId(request.Id);

            if (controleFralda == null)
            {
                return CommandResponse<ObterControleFraldaCommandResponse>.AdicionarErro(
                    statusCode: HttpStatusCode.NotFound,
                    mensagem: "Controle de fralda não encontrado.");
            }

            var response = new ObterControleFraldaCommandResponse(
                controleFralda.Id,
                controleFralda.BebeNascidoId,
                controleFralda.HoraTroca,
                controleFralda.TipoFralda,
                controleFralda.Observacoes
            );

            return CommandResponse<ObterControleFraldaCommandResponse>.Sucesso(response, HttpStatusCode.OK);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao obter controle de fralda");
            return CommandResponse<ObterControleFraldaCommandResponse>.ErroCritico(
                mensagem: $"Ocorreu um erro ao obter o controle de fralda: {ex.Message}");
        }
    }
}