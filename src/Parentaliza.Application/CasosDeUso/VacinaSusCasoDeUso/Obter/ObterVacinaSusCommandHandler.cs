using MediatR;
using Microsoft.Extensions.Logging;
using Parentaliza.Application.Mediator;
using Parentaliza.Domain.InterfacesRepository;
using System.Net;

namespace Parentaliza.Application.CasosDeUso.VacinaSusCasoDeUso.Obter;

public class ObterVacinaSusCommandHandler : IRequestHandler<ObterVacinaSusCommand, CommandResponse<ObterVacinaSusCommandResponse>>
{
    private readonly IVacinaSusRepository _vacinaSusRepository;
    private readonly ILogger<ObterVacinaSusCommandHandler> _logger;

    public ObterVacinaSusCommandHandler(
        IVacinaSusRepository vacinaSusRepository,
        ILogger<ObterVacinaSusCommandHandler> logger)
    {
        _vacinaSusRepository = vacinaSusRepository;
        _logger = logger;
    }

    public async Task<CommandResponse<ObterVacinaSusCommandResponse>> Handle(ObterVacinaSusCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var vacinaSus = await _vacinaSusRepository.ObterPorId(request.Id);

            if (vacinaSus == null)
            {
                return CommandResponse<ObterVacinaSusCommandResponse>.AdicionarErro(
                    statusCode: HttpStatusCode.NotFound,
                    mensagem: "Vacina SUS não encontrada.");
            }

            var response = new ObterVacinaSusCommandResponse(
                vacinaSus.Id,
                vacinaSus.NomeVacina,
                vacinaSus.Descricao,
                vacinaSus.CategoriaFaixaEtaria,
                vacinaSus.IdadeMinMesesVacina,
                vacinaSus.IdadeMaxMesesVacina
            );

            return CommandResponse<ObterVacinaSusCommandResponse>.Sucesso(response, HttpStatusCode.OK);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao obter vacina SUS");
            return CommandResponse<ObterVacinaSusCommandResponse>.ErroCritico(
                mensagem: $"Ocorreu um erro ao obter a vacina SUS: {ex.Message}");
        }
    }
}