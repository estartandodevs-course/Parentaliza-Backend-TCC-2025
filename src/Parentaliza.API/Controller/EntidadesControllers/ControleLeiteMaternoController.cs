using MediatR;
using Microsoft.AspNetCore.Mvc;
using Parentaliza.API.Controller.Base;
using Parentaliza.API.Controller.Dtos;
using Parentaliza.Application.CasosDeUso.ControleLeiteMaternoCasoDeUso.Criar;
using Parentaliza.Application.CasosDeUso.ControleLeiteMaternoCasoDeUso.Editar;
using Parentaliza.Application.CasosDeUso.ControleLeiteMaternoCasoDeUso.Excluir;
using Parentaliza.Application.CasosDeUso.ControleLeiteMaternoCasoDeUso.Listar;
using Parentaliza.Application.CasosDeUso.ControleLeiteMaternoCasoDeUso.ListarPorBebe;
using Parentaliza.Application.CasosDeUso.ControleLeiteMaternoCasoDeUso.Obter;
using Parentaliza.Application.Mediator;

namespace Parentaliza.API.Controller.EntidadesControllers;

/// <summary>
/// Controller responsável pelo gerenciamento de controles de leite materno
/// </summary>
[ApiController]
[Route("api/ControleLeiteMaterno")]
[Produces("application/json")]
public class ControleLeiteMaternoController : BaseController
{
    private readonly IMediator _mediator;

    public ControleLeiteMaternoController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Cria um novo controle de leite materno
    /// </summary>
    /// <param name="request">Dados do controle de leite materno a ser criado</param>
    /// <returns>ID do controle criado</returns>
    /// <response code="201">Controle criado com sucesso</response>
    /// <response code="400">Dados inválidos fornecidos</response>
    /// <response code="500">Erro interno do servidor</response>
    [HttpPost]
    [Route("Criar")]
    [ProducesResponseType(typeof(CriarControleLeiteMaternoCommandResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CriarControleLeiteMaterno([FromBody] CriarControleLeiteMaternoDtos request)
    {
        var command = new CriarControleLeiteMaternoCommand(
            bebeNascidoId: request.BebeNascidoId,
            cronometro: request.Cronometro,
            ladoDireito: request.LadoDireito,
            ladoEsquerdo: request.LadoEsquerdo
        );

        var response = await _mediator.Send(command);
        return StatusCode((int)response.StatusCode, response);
    }

    /// <summary>
    /// Lista todos os controles de leite materno com paginação, filtros e ordenação
    /// </summary>
    /// <param name="page">Número da página (padrão: 1)</param>
    /// <param name="pageSize">Quantidade de itens por página (padrão: 10, máximo: 100)</param>
    /// <param name="bebeNascidoId">Filtrar por ID do bebê</param>
    /// <param name="dataInicio">Filtrar por data inicial (formato: yyyy-MM-dd)</param>
    /// <param name="dataFim">Filtrar por data final (formato: yyyy-MM-dd)</param>
    /// <param name="sortBy">Campo para ordenar: "cronometro" (padrão: "cronometro")</param>
    /// <param name="sortOrder">Direção da ordenação: "asc" ou "desc" (padrão: "desc")</param>
    /// <returns>Lista paginada de controles de leite materno cadastrados</returns>
    /// <response code="200">Lista de controles retornada com sucesso</response>
    /// <response code="500">Erro interno do servidor</response>
    [HttpGet]
    [Route("Listar")]
    [ProducesResponseType(typeof(PagedResult<ListarControleLeiteMaternoCommandResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ListarControlesLeiteMaterno(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] Guid? bebeNascidoId = null,
        [FromQuery] DateTime? dataInicio = null,
        [FromQuery] DateTime? dataFim = null,
        [FromQuery] string? sortBy = null,
        [FromQuery] string sortOrder = "desc")
    {
        var command = new ListarControleLeiteMaternoCommand(
            page: page,
            pageSize: pageSize,
            bebeNascidoId: bebeNascidoId,
            dataInicio: dataInicio,
            dataFim: dataFim,
            sortBy: sortBy,
            sortOrder: sortOrder);
        var response = await _mediator.Send(command);
        return StatusCode((int)response.StatusCode, response);
    }

    /// <summary>
    /// Obtém um controle de leite materno específico pelo ID
    /// </summary>
    /// <param name="id">ID do controle a ser obtido</param>
    /// <returns>Dados do controle encontrado</returns>
    /// <response code="200">Controle encontrado e retornado com sucesso</response>
    /// <response code="404">Controle não encontrado</response>
    /// <response code="500">Erro interno do servidor</response>
    [HttpGet]
    [Route("Obter/{id}")]
    [ProducesResponseType(typeof(ObterControleLeiteMaternoCommandResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ObterControleLeiteMaternoPorId([FromRoute] Guid id)
    {
        var command = new ObterControleLeiteMaternoCommand(id);
        var response = await _mediator.Send(command);
        return StatusCode((int)response.StatusCode, response);
    }

    /// <summary>
    /// Atualiza um controle de leite materno existente
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
    [ProducesResponseType(typeof(EditarControleLeiteMaternoCommandResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> EditarControleLeiteMaterno([FromRoute] Guid id, [FromBody] EditarControleLeiteMaternoDtos request)
    {
        var command = new EditarControleLeiteMaternoCommand(
            id: id,
            bebeNascidoId: request.BebeNascidoId,
            cronometro: request.Cronometro,
            ladoDireito: request.LadoDireito,
            ladoEsquerdo: request.LadoEsquerdo
        );

        var response = await _mediator.Send(command);
        return StatusCode((int)response.StatusCode, response);
    }

    /// <summary>
    /// Lista todos os controles de leite materno de um bebê específico
    /// </summary>
    /// <param name="bebeNascidoId">ID do bebê</param>
    /// <returns>Lista de controles de leite materno do bebê</returns>
    /// <response code="200">Lista retornada com sucesso</response>
    /// <response code="404">Bebê não encontrado</response>
    /// <response code="500">Erro interno do servidor</response>
    [HttpGet]
    [Route("ListarPorBebe/{bebeNascidoId}")]
    [ProducesResponseType(typeof(List<ListarControlesLeiteMaternoPorBebeCommandResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ListarControlesLeiteMaternoPorBebe([FromRoute] Guid bebeNascidoId)
    {
        var command = new ListarControlesLeiteMaternoPorBebeCommand(bebeNascidoId);
        var response = await _mediator.Send(command);
        return StatusCode((int)response.StatusCode, response);
    }

    /// <summary>
    /// Exclui um controle de leite materno
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
    public async Task<IActionResult> ExcluirControleLeiteMaterno([FromRoute] Guid id)
    {
        var command = new ExcluirControleLeiteMaternoCommand(id);
        var response = await _mediator.Send(command);
        return StatusCode((int)response.StatusCode, response);
    }
}