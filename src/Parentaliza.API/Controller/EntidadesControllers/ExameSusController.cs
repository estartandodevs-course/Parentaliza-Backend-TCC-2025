using MediatR;
using Microsoft.AspNetCore.Mvc;
using Parentaliza.API.Controller.Base;
using Parentaliza.Application.CasosDeUso.ExameSusCasoDeUso.Listar;
using Parentaliza.Application.CasosDeUso.ExameSusCasoDeUso.Obter;

namespace Parentaliza.API.Controller.EntidadesControllers;

/// <summary>
/// Controller responsável pela consulta de exames SUS obrigatórios
/// </summary>
/// <remarks>
/// Este controller fornece acesso apenas de leitura aos exames SUS obrigatórios.
/// Os dados são de referência do SUS e devem ser populados via seed/migration.
/// </remarks>
[ApiController]
[Route("api/ExameSus")]
[Produces("application/json")]
public class ExameSusController : BaseController
{
    private readonly IMediator _mediator;

    public ExameSusController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Lista todos os exames SUS obrigatórios disponíveis
    /// </summary>
    /// <returns>Lista de todos os exames SUS cadastrados</returns>
    /// <response code="200">Lista de exames retornada com sucesso</response>
    /// <response code="500">Erro interno do servidor</response>
    [HttpGet]
    [Route("Listar")]
    [ProducesResponseType(typeof(List<ListarExameSusCommandResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ListarExamesSus()
    {
        var command = new ListarExameSusCommand();
        var response = await _mediator.Send(command);
        return StatusCode((int)response.StatusCode, response);
    }

    /// <summary>
    /// Obtém um exame SUS específico pelo ID
    /// </summary>
    /// <param name="id">ID do exame a ser obtido</param>
    /// <returns>Dados do exame encontrado</returns>
    /// <response code="200">Exame encontrado e retornado com sucesso</response>
    /// <response code="404">Exame não encontrado</response>
    /// <response code="500">Erro interno do servidor</response>
    [HttpGet]
    [Route("Obter/{id}")]
    [ProducesResponseType(typeof(ObterExameSusCommandResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ObterExameSusPorId([FromRoute] Guid id)
    {
        var command = new ObterExameSusCommand(id);
        var response = await _mediator.Send(command);
        return StatusCode((int)response.StatusCode, response);
    }
}