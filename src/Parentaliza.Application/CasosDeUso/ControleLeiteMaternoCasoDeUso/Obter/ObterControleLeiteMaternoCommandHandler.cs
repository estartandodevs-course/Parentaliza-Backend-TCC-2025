using MediatR;
using Microsoft.Extensions.Logging;
using Parentaliza.Application.Mediator;
using Parentaliza.Domain.InterfacesRepository;
using System.Net;

namespace Parentaliza.Application.CasosDeUso.ControleLeiteMaternoCasoDeUso.Obter;

public class ObterControleLeiteMaternoCommandHandler : IRequestHandler<ObterControleLeiteMaternoCommand, CommandResponse<ObterControleLeiteMaternoCommandResponse>>
{
    private readonly IControleLeiteMaternoRepository _controleLeiteMaternoRepository;
    private readonly ILogger<ObterControleLeiteMaternoCommandHandler> _logger;

    public ObterControleLeiteMaternoCommandHandler(
        IControleLeiteMaternoRepository controleLeiteMaternoRepository,
        ILogger<ObterControleLeiteMaternoCommandHandler> logger)
    {
        _controleLeiteMaternoRepository = controleLeiteMaternoRepository;
        _logger = logger;
    }

    public async Task<CommandResponse<ObterControleLeiteMaternoCommandResponse>> Handle(ObterControleLeiteMaternoCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var controleLeiteMaterno = await _controleLeiteMaternoRepository.ObterPorId(request.Id);

            if (controleLeiteMaterno == null)
            {
                return CommandResponse<ObterControleLeiteMaternoCommandResponse>.AdicionarErro(
                    statusCode: HttpStatusCode.NotFound,
                    mensagem: "Controle de leite materno não encontrado.");
            }

            var response = new ObterControleLeiteMaternoCommandResponse(
                controleLeiteMaterno.Id,
                controleLeiteMaterno.BebeNascidoId,
                controleLeiteMaterno.Cronometro,
                controleLeiteMaterno.LadoDireito,
                controleLeiteMaterno.LadoEsquerdo
            );

            return CommandResponse<ObterControleLeiteMaternoCommandResponse>.Sucesso(response, HttpStatusCode.OK);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao obter controle de leite materno");
            return CommandResponse<ObterControleLeiteMaternoCommandResponse>.ErroCritico(
                mensagem: $"Ocorreu um erro ao obter o controle de leite materno: {ex.Message}");
        }
    }
}