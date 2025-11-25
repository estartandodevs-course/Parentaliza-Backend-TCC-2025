using MediatR;
using Microsoft.AspNetCore.Mvc;
using Parentaliza.API.Controller.Base;
using Parentaliza.API.Controller.Dtos;
using Parentaliza.Application.Mediator;
using Parentaliza.Application.CasosDeUso.ControleMamadeiraCasoDeUso.Criar;
using Parentaliza.Application.CasosDeUso.ControleMamadeiraCasoDeUso.Editar;
using Parentaliza.Application.CasosDeUso.ControleMamadeiraCasoDeUso.Excluir;
using Parentaliza.Application.CasosDeUso.ControleMamadeiraCasoDeUso.Listar;
using Parentaliza.Application.CasosDeUso.ControleMamadeiraCasoDeUso.ListarPorBebe;
using Parentaliza.Application.CasosDeUso.ControleMamadeiraCasoDeUso.Obter;

namespace Parentaliza.API.Controller.EntidadesControllers;

/// <summary>
/// Controller responsável pelo gerenciamento de controles de mamadeira
/// </summary>
[ApiController]
[Route("api/ControleMamadeira")]
[Produces("application/json")]
public class ControleMamadeiraController : BaseController
{
    private readonly IMediator _mediator;

    public ControleMamadeiraController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Cria um novo controle de mamadeira
    /// </summary>
    /// <param name="request">Dados do controle de mamadeira a ser criado</param>
    /// <returns>ID do controle criado</returns>
    /// <response code="201">Controle criado com sucesso</response>
    /// <response code="400">Dados inválidos fornecidos</response>
    /// <response code="500">Erro interno do servidor</response>
    [HttpPost]
    [Route("Criar")]
    [ProducesResponseType(typeof(CriarControleMamadeiraCommandResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CriarControleMamadeira([FromBody] CriarControleMamadeiraDtos request)
    {
        var command = new CriarControleMamadeiraCommand(
            bebeNascidoId: request.BebeNascidoId,
            data: request.Data,
            hora: request.Hora,
            quantidadeLeite: request.QuantidadeLeite,
            anotacao: request.Anotacao
        );

        var response = await _mediator.Send(command);
        return StatusCode((int)response.StatusCode, response);
    }

    /// <summary>
    /// Lista todos os controles de mamadeira com paginação, filtros e ordenação
    /// </summary>
    /// <param name="page">Número da página (padrão: 1)</param>
    /// <param name="pageSize">Quantidade de itens por página (padrão: 10, máximo: 100)</param>
    /// <param name="bebeNascidoId">Filtrar por ID do bebê</param>
    /// <param name="dataInicio">Filtrar por data inicial (formato: yyyy-MM-dd)</param>
    /// <param name="dataFim">Filtrar por data final (formato: yyyy-MM-dd)</param>
    /// <param name="quantidadeLeiteMin">Filtrar por quantidade mínima de leite</param>
    /// <param name="quantidadeLeiteMax">Filtrar por quantidade máxima de leite</param>
    /// <param name="sortBy">Campo para ordenar: "data" ou "quantidadeLeite" (padrão: "data")</param>
    /// <param name="sortOrder">Direção da ordenação: "asc" ou "desc" (padrão: "desc")</param>
    /// <returns>Lista paginada de controles de mamadeira cadastrados</returns>
    /// <response code="200">Lista de controles retornada com sucesso</response>
    /// <response code="500">Erro interno do servidor</response>
    [HttpGet]
    [Route("Listar")]
    [ProducesResponseType(typeof(PagedResult<ListarControleMamadeiraCommandResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ListarControlesMamadeira(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] Guid? bebeNascidoId = null,
        [FromQuery] DateTime? dataInicio = null,
        [FromQuery] DateTime? dataFim = null,
        [FromQuery] decimal? quantidadeLeiteMin = null,
        [FromQuery] decimal? quantidadeLeiteMax = null,
        [FromQuery] string? sortBy = null,
        [FromQuery] string sortOrder = "desc")
    {
        var command = new ListarControleMamadeiraCommand(
            page: page,
            pageSize: pageSize,
            bebeNascidoId: bebeNascidoId,
            dataInicio: dataInicio,
            dataFim: dataFim,
            quantidadeLeiteMin: quantidadeLeiteMin,
            quantidadeLeiteMax: quantidadeLeiteMax,
            sortBy: sortBy,
            sortOrder: sortOrder);
        var response = await _mediator.Send(command);
        return StatusCode((int)response.StatusCode, response);
    }

    /// <summary>
    /// Obtém um controle de mamadeira específico pelo ID
    /// </summary>
    /// <param name="id">ID do controle a ser obtido</param>
    /// <returns>Dados do controle encontrado</returns>
    /// <response code="200">Controle encontrado e retornado com sucesso</response>
    /// <response code="404">Controle não encontrado</response>
    /// <response code="500">Erro interno do servidor</response>
    [HttpGet]
    [Route("Obter/{id}")]
    [ProducesResponseType(typeof(ObterControleMamadeiraCommandResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ObterControleMamadeiraPorId([FromRoute] Guid id)
    {
        var command = new ObterControleMamadeiraCommand(id);
        var response = await _mediator.Send(command);
        return StatusCode((int)response.StatusCode, response);
    }

    /// <summary>
    /// Atualiza um controle de mamadeira existente
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
    [ProducesResponseType(typeof(EditarControleMamadeiraCommandResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> EditarControleMamadeira([FromRoute] Guid id, [FromBody] EditarControleMamadeiraDtos request)
    {
        var command = new EditarControleMamadeiraCommand(
            id: id,
            bebeNascidoId: request.BebeNascidoId,
            data: request.Data,
            hora: request.Hora,
            quantidadeLeite: request.QuantidadeLeite,
            anotacao: request.Anotacao
        );

        var response = await _mediator.Send(command);
        return StatusCode((int)response.StatusCode, response);
    }

    /// <summary>
    /// Lista todos os controles de mamadeira de um bebê específico
    /// </summary>
    /// <param name="bebeNascidoId">ID do bebê</param>
    /// <returns>Lista de controles de mamadeira do bebê</returns>
    /// <response code="200">Lista retornada com sucesso</response>
    /// <response code="404">Bebê não encontrado</response>
    /// <response code="500">Erro interno do servidor</response>
    [HttpGet]
    [Route("ListarPorBebe/{bebeNascidoId}")]
    [ProducesResponseType(typeof(List<ListarControlesMamadeiraPorBebeCommandResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ListarControlesMamadeiraPorBebe([FromRoute] Guid bebeNascidoId)
    {
        var command = new ListarControlesMamadeiraPorBebeCommand(bebeNascidoId);
        var response = await _mediator.Send(command);
        return StatusCode((int)response.StatusCode, response);
    }

    /// <summary>
    /// Exclui um controle de mamadeira
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
    public async Task<IActionResult> ExcluirControleMamadeira([FromRoute] Guid id)
    {
        var command = new ExcluirControleMamadeiraCommand(id);
        var response = await _mediator.Send(command);
        return StatusCode((int)response.StatusCode, response);
    }
}

