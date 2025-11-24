using System.Collections.Generic;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Parentaliza.API.Controller.Base;
using Parentaliza.API.Controller.Dtos;
using Parentaliza.Application.CasosDeUso.ExameRealizadoCasoDeUso.Desmarcar;
using Parentaliza.Application.CasosDeUso.ExameRealizadoCasoDeUso.ListarPorBebe;
using Parentaliza.Application.CasosDeUso.ExameRealizadoCasoDeUso.MarcarRealizado;

namespace Parentaliza.API.Controller.EntidadesControllers;

/// <summary>
/// Controller responsável pelo gerenciamento de exames realizados pelos bebês
/// </summary>
[ApiController]
[Route("api/ExameRealizado")]
[Produces("application/json")]
public class ExameRealizadoController : BaseController
{
    private readonly IMediator _mediator;

    public ExameRealizadoController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Marca um exame SUS como realizado para um bebê
    /// </summary>
    /// <param name="bebeNascidoId">ID do bebê</param>
    /// <param name="exameSusId">ID do exame SUS</param>
    /// <param name="request">Dados da realização do exame</param>
    /// <returns>ID do registro criado/atualizado</returns>
    /// <response code="200">Exame marcado como realizado com sucesso</response>
    /// <response code="400">Dados inválidos fornecidos</response>
    /// <response code="404">Bebê ou exame não encontrado</response>
    /// <response code="500">Erro interno do servidor</response>
    [HttpPost]
    [Route("MarcarRealizado/{bebeNascidoId}/{exameSusId}")]
    [ProducesResponseType(typeof(MarcarExameRealizadoCommandResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> MarcarExameRealizado(
        [FromRoute] Guid bebeNascidoId,
        [FromRoute] Guid exameSusId,
        [FromBody] MarcarExameRealizadoDtos request)
    {
        var command = new MarcarExameRealizadoCommand(
            bebeNascidoId: bebeNascidoId,
            exameSusId: exameSusId,
            dataRealizacao: request.DataRealizacao,
            observacoes: request.Observacoes
        );

        var response = await _mediator.Send(command);
        return StatusCode((int)response.StatusCode, response);
    }

    /// <summary>
    /// Lista todos os exames SUS obrigatórios com o status de realização para um bebê específico
    /// </summary>
    /// <param name="bebeNascidoId">ID do bebê</param>
    /// <returns>Lista de exames SUS com status de realização</returns>
    /// <response code="200">Lista retornada com sucesso</response>
    /// <response code="404">Bebê não encontrado</response>
    /// <response code="500">Erro interno do servidor</response>
    [HttpGet]
    [Route("ListarPorBebe/{bebeNascidoId}")]
    [ProducesResponseType(typeof(List<ListarExamesPorBebeCommandResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ListarExamesPorBebe([FromRoute] Guid bebeNascidoId)
    {
        var command = new ListarExamesPorBebeCommand(bebeNascidoId);
        var response = await _mediator.Send(command);
        return StatusCode((int)response.StatusCode, response);
    }

    /// <summary>
    /// Desmarca um exame SUS (marca como não realizado) para um bebê
    /// </summary>
    /// <param name="bebeNascidoId">ID do bebê</param>
    /// <param name="exameSusId">ID do exame SUS</param>
    /// <returns>ID do registro atualizado</returns>
    /// <response code="200">Exame desmarcado com sucesso</response>
    /// <response code="400">Dados inválidos fornecidos</response>
    /// <response code="404">Bebê ou exame não encontrado</response>
    /// <response code="500">Erro interno do servidor</response>
    [HttpPut]
    [Route("Desmarcar/{bebeNascidoId}/{exameSusId}")]
    [ProducesResponseType(typeof(DesmarcarExameRealizadoCommandResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DesmarcarExameRealizado(
        [FromRoute] Guid bebeNascidoId,
        [FromRoute] Guid exameSusId)
    {
        var command = new DesmarcarExameRealizadoCommand(bebeNascidoId, exameSusId);
        var response = await _mediator.Send(command);
        return StatusCode((int)response.StatusCode, response);
    }
}

