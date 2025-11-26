using MediatR;
using Microsoft.AspNetCore.Mvc;
using Parentaliza.API.Controller.Base;
using Parentaliza.API.Controller.Dtos;
using Parentaliza.Application.CasosDeUso.ControleFraldaCasoDeUso.Criar;
using Parentaliza.Application.CasosDeUso.ControleFraldaCasoDeUso.Editar;
using Parentaliza.Application.CasosDeUso.ControleFraldaCasoDeUso.Excluir;
using Parentaliza.Application.CasosDeUso.ControleFraldaCasoDeUso.Listar;
using Parentaliza.Application.CasosDeUso.ControleFraldaCasoDeUso.ListarPorBebe;
using Parentaliza.Application.CasosDeUso.ControleFraldaCasoDeUso.Obter;
using Parentaliza.Application.Mediator;

namespace Parentaliza.API.Controller.EntidadesControllers;

/// <summary>
/// Controller responsável pelo gerenciamento de controles de fralda
/// </summary>
[ApiController]
[Route("api/ControleFralda")]
[Produces("application/json")]
public class ControleFraldaController : BaseController
{
    private readonly IMediator _mediator;

    public ControleFraldaController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Cria um novo controle de fralda
    /// </summary>
    /// <param name="request">Dados do controle de fralda a ser criado</param>
    /// <returns>ID do controle criado</returns>
    /// <response code="201">Controle criado com sucesso</response>
    /// <response code="400">Dados inválidos fornecidos</response>
    /// <response code="500">Erro interno do servidor</response>
    [HttpPost]
    [Route("Criar")]
    [ProducesResponseType(typeof(CriarControleFraldaCommandResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CriarControleFralda([FromBody] CriarControleFraldaDtos request)
    {
        var command = new CriarControleFraldaCommand(
            bebeNascidoId: request.BebeNascidoId,
            horaTroca: request.HoraTroca,
            tipoFralda: request.TipoFralda,
            observacoes: request.Observacoes
        );

        var response = await _mediator.Send(command);
        return StatusCode((int)response.StatusCode, response);
    }

    /// <summary>
    /// Lista todos os controles de fralda com paginação, filtros e ordenação
    /// </summary>
    /// <param name="page">Número da página (padrão: 1)</param>
    /// <param name="pageSize">Quantidade de itens por página (padrão: 10, máximo: 100)</param>
    /// <param name="bebeNascidoId">Filtrar por ID do bebê</param>
    /// <param name="dataInicio">Filtrar por data inicial (formato: yyyy-MM-dd)</param>
    /// <param name="dataFim">Filtrar por data final (formato: yyyy-MM-dd)</param>
    /// <param name="tipoFralda">Filtrar por tipo de fralda (busca parcial)</param>
    /// <param name="sortBy">Campo para ordenar: "horaTroca" ou "tipoFralda" (padrão: "horaTroca")</param>
    /// <param name="sortOrder">Direção da ordenação: "asc" ou "desc" (padrão: "desc")</param>
    /// <returns>Lista paginada de controles de fralda cadastrados</returns>
    /// <response code="200">Lista de controles retornada com sucesso</response>
    /// <response code="500">Erro interno do servidor</response>
    [HttpGet]
    [Route("Listar")]
    [ProducesResponseType(typeof(PagedResult<ListarControleFraldaCommandResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ListarControlesFralda(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] Guid? bebeNascidoId = null,
        [FromQuery] DateTime? dataInicio = null,
        [FromQuery] DateTime? dataFim = null,
        [FromQuery] string? tipoFralda = null,
        [FromQuery] string? sortBy = null,
        [FromQuery] string sortOrder = "desc")
    {
        var command = new ListarControleFraldaCommand(
            page: page,
            pageSize: pageSize,
            bebeNascidoId: bebeNascidoId,
            dataInicio: dataInicio,
            dataFim: dataFim,
            tipoFralda: tipoFralda,
            sortBy: sortBy,
            sortOrder: sortOrder);
        var response = await _mediator.Send(command);
        return StatusCode((int)response.StatusCode, response);
    }

    /// <summary>
    /// Obtém um controle de fralda específico pelo ID
    /// </summary>
    /// <param name="id">ID do controle a ser obtido</param>
    /// <returns>Dados do controle encontrado</returns>
    /// <response code="200">Controle encontrado e retornado com sucesso</response>
    /// <response code="404">Controle não encontrado</response>
    /// <response code="500">Erro interno do servidor</response>
    [HttpGet]
    [Route("Obter/{id}")]
    [ProducesResponseType(typeof(ObterControleFraldaCommandResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ObterControleFraldaPorId([FromRoute] Guid id)
    {
        var command = new ObterControleFraldaCommand(id);
        var response = await _mediator.Send(command);
        return StatusCode((int)response.StatusCode, response);
    }

    /// <summary>
    /// Atualiza um controle de fralda existente
    /// </summary>
    /// <param name="id">ID do controle a ser atualizado</param>
    /// <param name="request">Dados a serem atualizados</param>
    /// <returns>ID do controle atualizado</returns>
    /// <response code="200">Controle atualizado com sucesso</response>
    /// <response code="400">Dados inválidos fornecidos</response>
    /// <response code="404">Controle não encontrado</response>
    /// <response code="500">Erro interno do servidor</response>
    [HttpPut]
    [Route("Editar/{id}")]
    [ProducesResponseType(typeof(EditarControleFraldaCommandResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> EditarControleFralda([FromRoute] Guid id, [FromBody] EditarControleFraldaDtos request)
    {
        var command = new EditarControleFraldaCommand(
            id: id,
            bebeNascidoId: request.BebeNascidoId,
            horaTroca: request.HoraTroca,
            tipoFralda: request.TipoFralda,
            observacoes: request.Observacoes
        );

        var response = await _mediator.Send(command);
        return StatusCode((int)response.StatusCode, response);
    }

    /// <summary>
    /// Lista todos os controles de fralda de um bebê específico
    /// </summary>
    /// <param name="bebeNascidoId">ID do bebê</param>
    /// <returns>Lista de controles de fralda do bebê</returns>
    /// <response code="200">Lista retornada com sucesso</response>
    /// <response code="404">Bebê não encontrado</response>
    /// <response code="500">Erro interno do servidor</response>
    [HttpGet]
    [Route("ListarPorBebe/{bebeNascidoId}")]
    [ProducesResponseType(typeof(List<ListarControlesFraldaPorBebeCommandResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ListarControlesFraldaPorBebe([FromRoute] Guid bebeNascidoId)
    {
        var command = new ListarControlesFraldaPorBebeCommand(bebeNascidoId);
        var response = await _mediator.Send(command);
        return StatusCode((int)response.StatusCode, response);
    }

    /// <summary>
    /// Exclui um controle de fralda
    /// </summary>
    /// <param name="id">ID do controle a ser excluído</param>
    /// <returns>Sem conteúdo</returns>
    /// <response code="204">Controle excluído com sucesso</response>
    /// <response code="404">Controle não encontrado</response>
    /// <response code="500">Erro interno do servidor</response>
    [HttpDelete]
    [Route("Excluir/{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ExcluirControleFralda([FromRoute] Guid id)
    {
        var command = new ExcluirControleFraldaCommand(id);
        var response = await _mediator.Send(command);
        return StatusCode((int)response.StatusCode, response);
    }
}