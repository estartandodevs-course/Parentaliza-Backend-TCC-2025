using MediatR;
using Microsoft.AspNetCore.Mvc;
using Parentaliza.API.Controller.Base;
using Parentaliza.Application.CasosDeUso.VacinaSusCasoDeUso.Listar;
using Parentaliza.Application.CasosDeUso.VacinaSusCasoDeUso.Obter;

namespace Parentaliza.API.Controller.EntidadesControllers;

/// <summary>
/// Controller responsável pela consulta de vacinas SUS obrigatórias
/// </summary>
/// <remarks>
/// Este controller fornece acesso apenas de leitura às vacinas SUS obrigatórias.
/// Os dados são de referência do SUS e devem ser populados via seed/migration.
/// </remarks>
[ApiController]
[Route("api/VacinaSus")]
[Produces("application/json")]
public class VacinaSusController : BaseController
{
    private readonly IMediator _mediator;

    public VacinaSusController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Lista todas as vacinas SUS obrigatórias disponíveis
    /// </summary>
    /// <returns>Lista de todas as vacinas SUS cadastradas</returns>
    /// <response code="200">Lista de vacinas retornada com sucesso</response>
    /// <response code="500">Erro interno do servidor</response>
    [HttpGet]
    [Route("Listar")]
    [ProducesResponseType(typeof(List<ListarVacinaSusCommandResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ListarVacinasSus()
    {
        var command = new ListarVacinaSusCommand();
        var response = await _mediator.Send(command);
        return StatusCode((int)response.StatusCode, response);
    }

    /// <summary>
    /// Obtém uma vacina SUS específica pelo ID
    /// </summary>
    /// <param name="id">ID da vacina a ser obtida</param>
    /// <returns>Dados da vacina encontrada</returns>
    /// <response code="200">Vacina encontrada e retornada com sucesso</response>
    /// <response code="404">Vacina não encontrada</response>
    /// <response code="500">Erro interno do servidor</response>
    [HttpGet]
    [Route("Obter/{id}")]
    [ProducesResponseType(typeof(ObterVacinaSusCommandResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ObterVacinaSusPorId([FromRoute] Guid id)
    {
        var command = new ObterVacinaSusCommand(id);
        var response = await _mediator.Send(command);
        return StatusCode((int)response.StatusCode, response);
    }
}

