using MediatR;
using Microsoft.Extensions.Logging;
using Parentaliza.Application.Mediator;
using Parentaliza.Domain.InterfacesRepository;
using System.Net;

namespace Parentaliza.Application.CasosDeUso.ControleLeiteMaternoCasoDeUso.Excluir;

public class ExcluirControleLeiteMaternoCommandHandler : IRequestHandler<ExcluirControleLeiteMaternoCommand, CommandResponse<Unit>>
{
    private readonly IControleLeiteMaternoRepository _controleLeiteMaternoRepository;
    private readonly ILogger<ExcluirControleLeiteMaternoCommandHandler> _logger;

    public ExcluirControleLeiteMaternoCommandHandler(
        IControleLeiteMaternoRepository controleLeiteMaternoRepository,
        ILogger<ExcluirControleLeiteMaternoCommandHandler> logger)
    {
        _controleLeiteMaternoRepository = controleLeiteMaternoRepository;
        _logger = logger;
    }

    public async Task<CommandResponse<Unit>> Handle(ExcluirControleLeiteMaternoCommand request, CancellationToken cancellationToken)
    {
        if (!request.Validar())
        {
            return CommandResponse<Unit>.ErroValidacao(request.ResultadoDasValidacoes);
        }

        try
        {
            var controleLeiteMaterno = await _controleLeiteMaternoRepository.ObterPorId(request.Id);

            if (controleLeiteMaterno == null)
            {
                return CommandResponse<Unit>.AdicionarErro(
                    statusCode: HttpStatusCode.NotFound,
                    mensagem: "Controle de leite materno não encontrado.");
            }

            await _controleLeiteMaternoRepository.Remover(request.Id);

            return CommandResponse<Unit>.Sucesso(Unit.Value, HttpStatusCode.NoContent);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao excluir controle de leite materno");
            return CommandResponse<Unit>.ErroCritico(
                mensagem: $"Ocorreu um erro ao excluir o controle de leite materno: {ex.Message}");
        }
    }
}