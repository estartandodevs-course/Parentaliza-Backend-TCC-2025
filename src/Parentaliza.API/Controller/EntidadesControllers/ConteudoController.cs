using MediatR;
using Microsoft.AspNetCore.Mvc;
using Parentaliza.API.Controller.Base;
using Parentaliza.API.Controller.Dtos;
using Parentaliza.Application.Mediator;
using Parentaliza.Application.CasosDeUso.ConteudoCasoDeUso.Criar;
using Parentaliza.Application.CasosDeUso.ConteudoCasoDeUso.Editar;
using Parentaliza.Application.CasosDeUso.ConteudoCasoDeUso.Excluir;
using Parentaliza.Application.CasosDeUso.ConteudoCasoDeUso.Listar;
using Parentaliza.Application.CasosDeUso.ConteudoCasoDeUso.Obter;

namespace Parentaliza.API.Controller.EntidadesControllers;

[ApiController]
[Route("api/Conteudo")]
[Produces("application/json")]
public class ConteudoController : BaseController
{
    private readonly IMediator _mediator;

    public ConteudoController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Armazena conteúdos importantes no banco de dados
    /// </summary>
    /// <param name="request">Parametros importantes para criar um conteudo.</param>
    /// <response code="201">Dados armazenados com sucesso</response>
    /// <response code="400">Dados inválidos fornecidos</response>
    /// <response code="409">O titulo do conteudo já está em uso</response>
    /// <response code="500">Erro interno do servidor</response>
    [HttpPost]
    [Route("Criar")]
    [ProducesResponseType(typeof(CriarConteudoCommandResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CriarConteudo(
        [FromBody] CriarConteudoDtos request)
    {
        var command = new CriarConteudoCommand(
        titulo: request.Titulo,
        categoria: request.Categoria,
        dataPublicacao: request.DataPublicacao,
        descricao: request.Descricao
        );

        var response = await _mediator.Send(command);

        return StatusCode((int)response.StatusCode, response);
    }

    /// <summary>
    /// Obtém todos os conteúdos do banco de dados com paginação, filtros e ordenação
    /// </summary>
    /// <param name="page">Número da página (padrão: 1)</param>
    /// <param name="pageSize">Quantidade de itens por página (padrão: 10, máximo: 100)</param>
    /// <param name="categoria">Filtrar por categoria (busca parcial)</param>
    /// <param name="dataInicio">Filtrar por data inicial de publicação (formato: yyyy-MM-dd)</param>
    /// <param name="dataFim">Filtrar por data final de publicação (formato: yyyy-MM-dd)</param>
    /// <param name="titulo">Filtrar por título (busca parcial)</param>
    /// <param name="sortBy">Campo para ordenar: "dataPublicacao", "titulo" ou "categoria" (padrão: "dataPublicacao")</param>
    /// <param name="sortOrder">Direção da ordenação: "asc" ou "desc" (padrão: "desc")</param>
    /// <returns>Lista paginada de todos os conteúdos cadastrados</returns>
    /// <response code="200">Lista de conteúdos retornada com sucesso</response>
    /// <response code="500">Erro interno do servidor</response>
    [HttpGet]
    [Route("Listar")]
    [ProducesResponseType(typeof(PagedResult<ListarConteudoCommandResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ListarConteudos(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] string? categoria = null,
        [FromQuery] DateTime? dataInicio = null,
        [FromQuery] DateTime? dataFim = null,
        [FromQuery] string? titulo = null,
        [FromQuery] string? sortBy = null,
        [FromQuery] string sortOrder = "desc")
    {
        var command = new ListarConteudoCommand(
            page: page,
            pageSize: pageSize,
            categoria: categoria,
            dataInicio: dataInicio,
            dataFim: dataFim,
            titulo: titulo,
            sortBy: sortBy,
            sortOrder: sortOrder);
        var response = await _mediator.Send(command);
        return StatusCode((int)response.StatusCode, response);
    }

    /// <summary>
    /// Obtém um conteúdo específico pelo ID
    /// </summary>
    /// <param name="id">ID do conteúdo a ser obtido</param>
    /// <returns>Dados do conteúdo encontrado</returns>
    /// <response code="200">Conteúdo encontrado e retornado com sucesso</response>
    /// <response code="404">Conteúdo não encontrado</response>
    /// <response code="500">Erro interno do servidor</response>
    [HttpGet]
    [Route("Obter/{id}")]
    [ProducesResponseType(typeof(ObterConteudoCommandResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ObterConteudoPorId([FromRoute] Guid id)
    {
        var command = new ObterConteudoCommand(id);
        var response = await _mediator.Send(command);
        return StatusCode((int)response.StatusCode, response);
    }

    /// <summary>
    /// Atualiza um conteudo existente no banco de dados
    /// </summary>
    /// <param name="id">ID do conteudo a ser atualizado</param>
    /// <param name="request">Dados a serem atualizado do conteudo</param>
    /// <returns>ID do conteudo atualizado</returns>
    /// <response code="200">Conteudo atualizado com sucesso</response>
    /// <response code="400">Dados inválidos fornecidos</response>
    /// <response code="404">Conteudo não encontrado</response>
    /// <response code="409">Nome do conteudo já está em uso por outro conteudo</response>
    /// <response code="500">Erro interno do servidor</response>
    [HttpPut]
    [Route("Editar/{id}")]
    [ProducesResponseType(typeof(EditarConteudoCommandResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> EditarConteudo(
        [FromRoute] Guid id,
        [FromBody] EditarConteudoDtos request)
    {
        var command = new EditarConteudoCommand(
            id: id,
            request.Titulo,
            request.Categoria,
            request.DataPublicacao,
            request.Descricao
        );

        var response = await _mediator.Send(command);
        return StatusCode((int)response.StatusCode, response);
    }

    /// <summary>
    /// Exclui um conteudo do banco de dados
    /// </summary>
    /// <param name="id">ID do conteudo a ser excluído</param>
    /// <returns>ID do conteudo excluído</returns>
    /// <response code="200">Conteudo excluído com sucesso</response>
    /// <response code="404">Conteudo não encontrado</response>
    /// <response code="500">Erro interno do servidor</response>
    [HttpDelete]
    [Route("Excluir/{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ExcluirConteudo([FromRoute] Guid id)
    {
        var command = new ExcluirConteudoCommand(id);
        var response = await _mediator.Send(command);
        return StatusCode((int)response.StatusCode, response);
    }

    // Aqui é o post de conteudo multimidia, não será amazenado no banco de dados, apenas enviado para o serviço de armazenamento de mídia.
    /*
        [HttpPost]
        public async Task<IActionResult> AdicionaConteudoMultimidia(
            [FromBody] CriarConteudoMultimidiaDtos
            request)
        {
            // Implementar a lógica para adicionar conteúdo multimídia  
            var command = new CriarConteudoMultimidiaCommand(
            conteudoId: request.ConteudoId,
            tipoMultimidia: request.TipoMultimidia,
            url: request.Url
            );
            var response = await _mediator.Send(command);
            return Ok();
        }
    */
}