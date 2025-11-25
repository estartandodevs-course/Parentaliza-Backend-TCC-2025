using MediatR;
using Microsoft.AspNetCore.Mvc;
using Parentaliza.API.Controller.Base;
using Parentaliza.API.Controller.Dtos;
using Parentaliza.Application.Mediator;
using Parentaliza.Application.CasosDeUso.ResponsavelCasoDeUso.Criar;
using Parentaliza.Application.CasosDeUso.ResponsavelCasoDeUso.Editar;
using Parentaliza.Application.CasosDeUso.ResponsavelCasoDeUso.Excluir;
using Parentaliza.Application.CasosDeUso.ResponsavelCasoDeUso.Listar;
using Parentaliza.Application.CasosDeUso.ResponsavelCasoDeUso.Obter;

namespace Parentaliza.API.Controller.EntidadesControllers;

/// <summary>
/// Controller responsável pelo gerenciamento de responsáveis
/// </summary>
[ApiController]
[Route("api/Responsavel")]
[Produces("application/json")]
public class ResponsavelController : BaseController
{
    private readonly IMediator _mediator;

    public ResponsavelController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Cria um novo responsável
    /// </summary>
    /// <param name="request">Dados do responsável a ser criado</param>
    /// <returns>Retorna o ID do responsável criado</returns>
    /// <response code="201">Responsável criado com sucesso</response>
    /// <response code="400">Dados inválidos fornecidos</response>
    /// <response code="409">Email já está em uso</response>
    /// <response code="500">Erro interno do servidor</response>
    [HttpPost]
    [Route("Criar")]
    [ProducesResponseType(typeof(CriarResponsavelCommandResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CriarResponsavel(
        [FromBody] CriarResponsavelDtos request)
    {
        var command = new CriarResponsavelCommand(
            nome: request.Nome,
            email: request.Email,
            tipoResponsavel: request.TipoResponsavel,
            senha: request.Senha,
            faseNascimento: request.FaseNascimento
        );

        var response = await _mediator.Send(command);

        return StatusCode((int)response.StatusCode, response);
    }

    /// <summary>
    /// Lista todos os responsáveis com paginação, filtros e ordenação
    /// </summary>
    /// <param name="page">Número da página (padrão: 1)</param>
    /// <param name="pageSize">Quantidade de itens por página (padrão: 10, máximo: 100)</param>
    /// <param name="nome">Filtrar por nome (busca parcial)</param>
    /// <param name="email">Filtrar por email (busca parcial)</param>
    /// <param name="tipoResponsavel">Filtrar por tipo de responsável (0, 1, 2, etc.)</param>
    /// <param name="sortBy">Campo para ordenar: "nome" ou "email" (padrão: "nome")</param>
    /// <param name="sortOrder">Direção da ordenação: "asc" ou "desc" (padrão: "asc")</param>
    /// <returns>Lista paginada de todos os responsáveis cadastrados</returns>
    /// <response code="200">Lista de responsáveis retornada com sucesso</response>
    /// <response code="500">Erro interno do servidor</response>
    [HttpGet]
    [Route("Listar")]
    [ProducesResponseType(typeof(PagedResult<ListarResponsavelCommandResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ListarResponsaveis(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] string? nome = null,
        [FromQuery] string? email = null,
        [FromQuery] int? tipoResponsavel = null,
        [FromQuery] string? sortBy = null,
        [FromQuery] string sortOrder = "asc")
    {
        var command = new ListarResponsavelCommand(
            page: page,
            pageSize: pageSize,
            nome: nome,
            email: email,
            tipoResponsavel: tipoResponsavel.HasValue ? (Domain.Enums.TipoResponsavel)tipoResponsavel.Value : null,
            sortBy: sortBy,
            sortOrder: sortOrder);
        var response = await _mediator.Send(command);
        return StatusCode((int)response.StatusCode, response);
    }

    /// <summary>
    /// Obtém um responsável específico pelo ID
    /// </summary>
    /// <param name="id">ID do responsável a ser obtido</param>
    /// <returns>Dados do responsável encontrado</returns>
    /// <response code="200">Responsável encontrado e retornado com sucesso</response>
    /// <response code="404">Responsável não encontrado</response>
    /// <response code="500">Erro interno do servidor</response>
    [HttpGet]
    [Route("Obter/{id}")]
    [ProducesResponseType(typeof(ObterResponsavelCommandResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ObterResponsavelPorId([FromRoute] Guid id)
    {
        var command = new ObterResponsavelCommand(id);
        var response = await _mediator.Send(command);
        return StatusCode((int)response.StatusCode, response);
    }

    /// <summary>
    /// Atualiza um responsável existente
    /// </summary>
    /// <param name="id">ID do responsável a ser atualizado</param>
    /// <param name="request">Dados atualizados do responsável</param>
    /// <returns>ID do responsável atualizado</returns>
    /// <response code="200">Responsável atualizado com sucesso</response>
    /// <response code="400">Dados inválidos fornecidos</response>
    /// <response code="404">Responsável não encontrado</response>
    /// <response code="409">Email já está em uso por outro responsável</response>
    /// <response code="500">Erro interno do servidor</response>
    [HttpPut]
    [Route("Editar/{id}")]
    [ProducesResponseType(typeof(EditarResponsavelCommandResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> EditarResponsavel(
        [FromRoute] Guid id,
        [FromBody] EditarResponsavelDtos request)
    {
        var command = new EditarResponsavelCommand(
            id: id,
            nome: request.Nome,
            email: request.Email,
            tipoResponsavel: request.TipoResponsavel,
            senha: request.Senha,
            faseNascimento: request.FaseNascimento
        );

        var response = await _mediator.Send(command);
        return StatusCode((int)response.StatusCode, response);
    }

    /// <summary>
    /// Exclui um responsável
    /// </summary>
    /// <param name="id">ID do responsável a ser excluído</param>
    /// <returns>Sem conteúdo</returns>
    /// <response code="204">Responsável excluído com sucesso</response>
    /// <response code="404">Responsável não encontrado</response>
    /// <response code="500">Erro interno do servidor</response>
    [HttpDelete]
    [Route("Excluir/{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ExcluirResponsavel([FromRoute] Guid id)
    {
        var command = new ExcluirResponsavelCommand(id);
        var response = await _mediator.Send(command);
        return StatusCode((int)response.StatusCode, response);
    }
}
