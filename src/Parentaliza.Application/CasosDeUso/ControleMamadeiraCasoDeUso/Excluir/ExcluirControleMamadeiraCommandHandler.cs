using MediatR;
using Microsoft.Extensions.Logging;
using Parentaliza.Application.Mediator;
using Parentaliza.Domain.InterfacesRepository;
using System.Net;

namespace Parentaliza.Application.CasosDeUso.ControleMamadeiraCasoDeUso.Excluir;

public class ExcluirControleMamadeiraCommandHandler : IRequestHandler<ExcluirControleMamadeiraCommand, CommandResponse<Unit>>
{
    private readonly IControleMamadeiraRepository _controleMamadeiraRepository;
    private readonly ILogger<ExcluirControleMamadeiraCommandHandler> _logger;

    public ExcluirControleMamadeiraCommandHandler(
        IControleMamadeiraRepository controleMamadeiraRepository,
        ILogger<ExcluirControleMamadeiraCommandHandler> logger)
    {
        _controleMamadeiraRepository = controleMamadeiraRepository;
        _logger = logger;
    }

    public async Task<CommandResponse<Unit>> Handle(ExcluirControleMamadeiraCommand request, CancellationToken cancellationToken)
    {
        if (!request.Validar())
        {
            return CommandResponse<Unit>.ErroValidacao(request.ResultadoDasValidacoes);
        }

        try
        {
            var controleMamadeira = await _controleMamadeiraRepository.ObterPorId(request.Id);

            if (controleMamadeira == null)
            {
                return CommandResponse<Unit>.AdicionarErro(
                    statusCode: HttpStatusCode.NotFound,
                    mensagem: "Controle de mamadeira não encontrado.");
            }

            await _controleMamadeiraRepository.Remover(request.Id);

            return CommandResponse<Unit>.Sucesso(Unit.Value, HttpStatusCode.NoContent);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao excluir controle de mamadeira");
            return CommandResponse<Unit>.ErroCritico(
                mensagem: $"Ocorreu um erro ao excluir o controle de mamadeira: {ex.Message}");
        }
    }
}

