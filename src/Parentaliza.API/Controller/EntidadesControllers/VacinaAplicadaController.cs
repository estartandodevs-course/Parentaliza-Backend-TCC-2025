using MediatR;
using Microsoft.AspNetCore.Mvc;
using Parentaliza.API.Controller.Base;
using Parentaliza.API.Controller.Dtos;
using Parentaliza.Application.CasosDeUso.VacinaAplicadaCasoDeUso.Desmarcar;
using Parentaliza.Application.CasosDeUso.VacinaAplicadaCasoDeUso.ListarPorBebe;
using Parentaliza.Application.CasosDeUso.VacinaAplicadaCasoDeUso.MarcarAplicada;

namespace Parentaliza.API.Controller.EntidadesControllers;

/// <summary>
/// Controller responsável pelo gerenciamento de vacinas aplicadas aos bebês
/// </summary>
[ApiController]
[Route("api/VacinaAplicada")]
[Produces("application/json")]
public class VacinaAplicadaController : BaseController
{
    private readonly IMediator _mediator;

    public VacinaAplicadaController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Marca uma vacina SUS como aplicada para um bebê
    /// </summary>
    /// <param name="bebeNascidoId">ID do bebê</param>
    /// <param name="vacinaSusId">ID da vacina SUS</param>
    /// <param name="request">Dados da aplicação da vacina</param>
    /// <returns>ID do registro criado/atualizado</returns>
    /// <response code="200">Vacina marcada como aplicada com sucesso</response>
    /// <response code="400">Dados inválidos fornecidos</response>
    /// <response code="404">Bebê ou vacina não encontrado</response>
    /// <response code="500">Erro interno do servidor</response>
    [HttpPost]
    [Route("MarcarAplicada/{bebeNascidoId}/{vacinaSusId}")]
    [ProducesResponseType(typeof(MarcarVacinaAplicadaCommandResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> MarcarVacinaAplicada(
        [FromRoute] Guid bebeNascidoId,
        [FromRoute] Guid vacinaSusId,
        [FromBody] MarcarVacinaAplicadaDtos request)
    {
        var command = new MarcarVacinaAplicadaCommand(
            bebeNascidoId: bebeNascidoId,
            vacinaSusId: vacinaSusId,
            dataAplicacao: request.DataAplicacao,
            lote: request.Lote,
            localAplicacao: request.LocalAplicacao,
            observacoes: request.Observacoes
        );

        var response = await _mediator.Send(command);
        return StatusCode((int)response.StatusCode, response);
    }

    /// <summary>
    /// Lista todas as vacinas SUS obrigatórias com o status de aplicação para um bebê específico
    /// </summary>
    /// <param name="bebeNascidoId">ID do bebê</param>
    /// <returns>Lista de vacinas SUS com status de aplicação</returns>
    /// <response code="200">Lista retornada com sucesso</response>
    /// <response code="404">Bebê não encontrado</response>
    /// <response code="500">Erro interno do servidor</response>
    [HttpGet]
    [Route("ListarPorBebe/{bebeNascidoId}")]
    [ProducesResponseType(typeof(List<ListarVacinasPorBebeCommandResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ListarVacinasPorBebe([FromRoute] Guid bebeNascidoId)
    {
        var command = new ListarVacinasPorBebeCommand(bebeNascidoId);
        var response = await _mediator.Send(command);
        return StatusCode((int)response.StatusCode, response);
    }

    /// <summary>
    /// Desmarca uma vacina SUS (marca como não aplicada) para um bebê
    /// </summary>
    /// <param name="bebeNascidoId">ID do bebê</param>
    /// <param name="vacinaSusId">ID da vacina SUS</param>
    /// <returns>ID do registro atualizado</returns>
    /// <response code="200">Vacina desmarcada com sucesso</response>
    /// <response code="400">Dados inválidos fornecidos</response>
    /// <response code="404">Bebê ou vacina não encontrado</response>
    /// <response code="500">Erro interno do servidor</response>
    [HttpPut]
    [Route("Desmarcar/{bebeNascidoId}/{vacinaSusId}")]
    [ProducesResponseType(typeof(DesmarcarVacinaAplicadaCommandResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DesmarcarVacinaAplicada(
        [FromRoute] Guid bebeNascidoId,
        [FromRoute] Guid vacinaSusId)
    {
        var command = new DesmarcarVacinaAplicadaCommand(bebeNascidoId, vacinaSusId);
        var response = await _mediator.Send(command);
        return StatusCode((int)response.StatusCode, response);
    }
}