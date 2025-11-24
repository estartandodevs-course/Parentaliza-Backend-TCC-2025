using MediatR;
using Microsoft.Extensions.Logging;
using Parentaliza.Application.Mediator;
using Parentaliza.Domain.Entidades;
using Parentaliza.Domain.InterfacesRepository;
using System.Net;
using System.Reflection;

namespace Parentaliza.Application.CasosDeUso.VacinaSusCasoDeUso.Editar;

public class EditarVacinaSusCommandHandler : IRequestHandler<EditarVacinaSusCommand, CommandResponse<EditarVacinaSusCommandResponse>>
{
    private readonly IVacinaSusRepository _vacinaSusRepository;
    private readonly ILogger<EditarVacinaSusCommandHandler> _logger;

    public EditarVacinaSusCommandHandler(
        IVacinaSusRepository vacinaSusRepository,
        ILogger<EditarVacinaSusCommandHandler> logger)
    {
        _vacinaSusRepository = vacinaSusRepository;
        _logger = logger;
    }

    public async Task<CommandResponse<EditarVacinaSusCommandResponse>> Handle(EditarVacinaSusCommand request, CancellationToken cancellationToken)
    {
        if (!request.Validar())
        {
            return CommandResponse<EditarVacinaSusCommandResponse>.ErroValidacao(request.ResultadoDasValidacoes);
        }

        try
        {
            var vacinaSus = await _vacinaSusRepository.ObterPorId(request.Id);

            if (vacinaSus == null)
            {
                return CommandResponse<EditarVacinaSusCommandResponse>.AdicionarErro(
                    statusCode: HttpStatusCode.NotFound,
                    mensagem: "Vacina SUS não encontrada.");
            }

            // Atualizar a entidade existente usando reflection para acessar propriedades privadas
            var tipo = typeof(VacinaSus);
            tipo.GetProperty(nameof(VacinaSus.NomeVacina))?.SetValue(vacinaSus, request.NomeVacina);
            tipo.GetProperty(nameof(VacinaSus.Descricao))?.SetValue(vacinaSus, request.Descricao);
            tipo.GetProperty(nameof(VacinaSus.CategoriaFaixaEtaria))?.SetValue(vacinaSus, request.CategoriaFaixaEtaria);
            tipo.GetProperty(nameof(VacinaSus.IdadeMinMesesVacina))?.SetValue(vacinaSus, request.IdadeMinMesesVacina);
            tipo.GetProperty(nameof(VacinaSus.IdadeMaxMesesVacina))?.SetValue(vacinaSus, request.IdadeMaxMesesVacina);

            await _vacinaSusRepository.Atualizar(vacinaSus);

            var response = new EditarVacinaSusCommandResponse(vacinaSus.Id);

            return CommandResponse<EditarVacinaSusCommandResponse>.Sucesso(response, HttpStatusCode.OK);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao editar vacina SUS");
            return CommandResponse<EditarVacinaSusCommandResponse>.ErroCritico(
                mensagem: $"Ocorreu um erro ao editar a vacina SUS: {ex.Message}");
        }
    }
}

