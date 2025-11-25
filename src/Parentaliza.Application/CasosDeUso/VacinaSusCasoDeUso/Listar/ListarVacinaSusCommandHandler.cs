using MediatR;
using Microsoft.Extensions.Logging;
using Parentaliza.Application.Mediator;
using Parentaliza.Domain.InterfacesRepository;
using System.Net;

namespace Parentaliza.Application.CasosDeUso.VacinaSusCasoDeUso.Listar;

public class ListarVacinaSusCommandHandler : IRequestHandler<ListarVacinaSusCommand, CommandResponse<List<ListarVacinaSusCommandResponse>>>
{
    private readonly IVacinaSusRepository _vacinaSusRepository;
    private readonly ILogger<ListarVacinaSusCommandHandler> _logger;

    public ListarVacinaSusCommandHandler(
        IVacinaSusRepository vacinaSusRepository,
        ILogger<ListarVacinaSusCommandHandler> logger)
    {
        _vacinaSusRepository = vacinaSusRepository;
        _logger = logger;
    }

    public async Task<CommandResponse<List<ListarVacinaSusCommandResponse>>> Handle(ListarVacinaSusCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var todasVacinas = await _vacinaSusRepository.ObterTodos();
            var response = todasVacinas.Select(vacina => new ListarVacinaSusCommandResponse(
                vacina.Id,
                vacina.NomeVacina,
                vacina.Descricao,
                vacina.CategoriaFaixaEtaria,
                vacina.IdadeMinMesesVacina,
                vacina.IdadeMaxMesesVacina
            )).ToList();

            return CommandResponse<List<ListarVacinaSusCommandResponse>>.Sucesso(response, HttpStatusCode.OK);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao listar vacinas SUS");
            return CommandResponse<List<ListarVacinaSusCommandResponse>>.ErroCritico(
                mensagem: $"Ocorreu um erro ao listar as vacinas SUS: {ex.Message}");
        }
    }
}

