using MediatR;
using Microsoft.Extensions.Logging;
using Parentaliza.Application.Mediator;
using Parentaliza.Domain.InterfacesRepository;
using System.Net;

namespace Parentaliza.Application.CasosDeUso.VacinaSusCasoDeUso.Excluir;

public class ExcluirVacinaSusCommandHandler : IRequestHandler<ExcluirVacinaSusCommand, CommandResponse<ExcluirVacinaSusCommandResponse>>
{
    private readonly IVacinaSusRepository _vacinaSusRepository;
    private readonly ILogger<ExcluirVacinaSusCommandHandler> _logger;

    public ExcluirVacinaSusCommandHandler(
        IVacinaSusRepository vacinaSusRepository,
        ILogger<ExcluirVacinaSusCommandHandler> logger)
    {
        _vacinaSusRepository = vacinaSusRepository;
        _logger = logger;
    }

    public async Task<CommandResponse<ExcluirVacinaSusCommandResponse>> Handle(ExcluirVacinaSusCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var vacinaSus = await _vacinaSusRepository.ObterPorId(request.Id);

            if (vacinaSus == null)
            {
                return CommandResponse<ExcluirVacinaSusCommandResponse>.AdicionarErro(
                    statusCode: HttpStatusCode.NotFound,
                    mensagem: "Vacina SUS não encontrada.");
            }

            await _vacinaSusRepository.Remover(request.Id);

            return CommandResponse<ExcluirVacinaSusCommandResponse>.Sucesso(string.Empty, HttpStatusCode.NoContent);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao excluir vacina SUS");
            return CommandResponse<ExcluirVacinaSusCommandResponse>.ErroCritico(
                mensagem: $"Ocorreu um erro ao excluir a vacina SUS: {ex.Message}");
        }
    }
}