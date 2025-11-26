using MediatR;
using Microsoft.Extensions.Logging;
using Parentaliza.Application.Mediator;
using Parentaliza.Domain.InterfacesRepository;
using System.Net;

namespace Parentaliza.Application.CasosDeUso.ControleMamadeiraCasoDeUso.Obter;

public class ObterControleMamadeiraCommandHandler : IRequestHandler<ObterControleMamadeiraCommand, CommandResponse<ObterControleMamadeiraCommandResponse>>
{
    private readonly IControleMamadeiraRepository _controleMamadeiraRepository;
    private readonly ILogger<ObterControleMamadeiraCommandHandler> _logger;

    public ObterControleMamadeiraCommandHandler(
        IControleMamadeiraRepository controleMamadeiraRepository,
        ILogger<ObterControleMamadeiraCommandHandler> logger)
    {
        _controleMamadeiraRepository = controleMamadeiraRepository;
        _logger = logger;
    }

    public async Task<CommandResponse<ObterControleMamadeiraCommandResponse>> Handle(ObterControleMamadeiraCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var controleMamadeira = await _controleMamadeiraRepository.ObterPorId(request.Id);

            if (controleMamadeira == null)
            {
                return CommandResponse<ObterControleMamadeiraCommandResponse>.AdicionarErro(
                    statusCode: HttpStatusCode.NotFound,
                    mensagem: "Controle de mamadeira não encontrado.");
            }

            var response = new ObterControleMamadeiraCommandResponse(
                controleMamadeira.Id,
                controleMamadeira.BebeNascidoId,
                controleMamadeira.Data,
                controleMamadeira.Hora,
                controleMamadeira.QuantidadeLeite,
                controleMamadeira.Anotacao
            );

            return CommandResponse<ObterControleMamadeiraCommandResponse>.Sucesso(response, HttpStatusCode.OK);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao obter controle de mamadeira");
            return CommandResponse<ObterControleMamadeiraCommandResponse>.ErroCritico(
                mensagem: $"Ocorreu um erro ao obter o controle de mamadeira: {ex.Message}");
        }
    }
}