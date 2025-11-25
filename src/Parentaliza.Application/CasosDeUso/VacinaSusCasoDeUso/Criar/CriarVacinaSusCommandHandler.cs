using MediatR;
using Microsoft.Extensions.Logging;
using Parentaliza.Application.Mediator;
using Parentaliza.Domain.Entidades;
using Parentaliza.Domain.InterfacesRepository;
using System.Net;

namespace Parentaliza.Application.CasosDeUso.VacinaSusCasoDeUso.Criar;

public class CriarVacinaSusCommandHandler : IRequestHandler<CriarVacinaSusCommand, CommandResponse<CriarVacinaSusCommandResponse>>
{
    private readonly IVacinaSusRepository _vacinaSusRepository;
    private readonly ILogger<CriarVacinaSusCommandHandler> _logger;

    public CriarVacinaSusCommandHandler(
        IVacinaSusRepository vacinaSusRepository,
        ILogger<CriarVacinaSusCommandHandler> logger)
    {
        _vacinaSusRepository = vacinaSusRepository;
        _logger = logger;
    }

    public async Task<CommandResponse<CriarVacinaSusCommandResponse>> Handle(CriarVacinaSusCommand request, CancellationToken cancellationToken)
    {
        if (!request.Validar())
        {
            return CommandResponse<CriarVacinaSusCommandResponse>.ErroValidacao(request.ResultadoDasValidacoes);
        }

        try
        {
            var vacinaSus = new VacinaSus(
                nomeVacina: request.NomeVacina,
                descricao: request.Descricao,
                categoriaFaixaEtaria: request.CategoriaFaixaEtaria,
                idadeMinMesesVacina: request.IdadeMinMesesVacina,
                idadeMaxMesesVacina: request.IdadeMaxMesesVacina
            );

            await _vacinaSusRepository.Adicionar(vacinaSus);

            var response = new CriarVacinaSusCommandResponse(vacinaSus.Id);

            return CommandResponse<CriarVacinaSusCommandResponse>.Sucesso(response, HttpStatusCode.Created);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao criar vacina SUS");
            return CommandResponse<CriarVacinaSusCommandResponse>.ErroCritico(
                mensagem: $"Ocorreu um erro ao criar a vacina SUS: {ex.Message}");
        }
    }
}

