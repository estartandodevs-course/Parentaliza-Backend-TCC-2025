using MediatR;
using Microsoft.AspNetCore.Mvc;
using Parentaliza.API.Controller.Base;
using Parentaliza.API.Controller.Dtos;
using Parentaliza.Application.CasosDeUso.BebeNascidoCasoDeUso.Criar;
using Parentaliza.Application.CasosDeUso.BebeNascidoCasoDeUso.Editar;
using Parentaliza.Application.CasosDeUso.BebeNascidoCasoDeUso.Excluir;
using Parentaliza.Application.CasosDeUso.BebeNascidoCasoDeUso.ListarPorResponsavel;
using Parentaliza.Application.CasosDeUso.BebeNascidoCasoDeUso.Obter;

namespace Parentaliza.API.Controller.EntidadesControllers;

/// <summary>
/// Controller responsável pelo armazenamento das informações do bebê nascido
/// </summary>
[ApiController]
[Route("api/BebeNascido")]
[Produces("application/json")]
public class BebeNascidoController : BaseController
{
    private readonly IMediator _mediator;

    public BebeNascidoController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Armazena as informações do bebê nascido
    /// </summary>
    /// <param name="request">Dados do bebê a ser criado</param>
    /// <response code="201">Dados armazenados com sucesso</response>
    /// <response code="400">Dados inválidos fornecidos</response>
    /// <response code="500">Erro interno do servidor</response>
    [HttpPost]
    [Route("Criar")]
    [ProducesResponseType(typeof(CriarBebeNascidoCommandResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CriarBebeNascido(
            [FromBody] CriarBebeNascidoDtos request)
    {
        var command = new CriarBebeNascidoCommand(
            responsavelId: request.ResponsavelId,
            nome: request.Nome,
            dataNascimento: request.DataNascimento,
            sexo: request.Sexo,
            tipoSanguineo: request.TipoSanguineo,
            idadeMeses: request.IdadeMeses,
            peso: request.Peso,
            altura: request.Altura
            );

        var response = await _mediator.Send(command);

        return StatusCode((int)response.StatusCode, response);
    }

    /// <summary>
    /// Busca as informações do bebê nascido no banco de dados
    /// </summary>
    /// <param name="id">Obter dados do bebê pelo ID</param>
    /// <response code="200">Busca realizada com sucesso</response>
    /// <response code="400">Dados inválidos fornecidos</response>
    /// <response code="404">Bebê não encontrado</response>
    /// <response code="500">Erro interno do servidor</response>
    [HttpGet]
    [Route("Obter/{id}")]
    [ProducesResponseType(typeof(ObterBebeNascidoCommandResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ObterBebeNascidoPorId([FromRoute] Guid id)
    {
        var command = new ObterBebeNascidoCommand(id);
        var response = await _mediator.Send(command);
        return StatusCode((int)response.StatusCode, response);
    }

    /// <summary>
    /// Atualiza as informações do bebê nascido no banco de dados
    /// </summary>
    /// <param name="id">ID do bebê a ser atualizado</param>
    /// <param name="request">Dados atualizados do bebê</param>
    /// <returns>ID do bebê atualizado</returns>
    /// <response code="200">Informações atualizadas com sucesso</response>
    /// <response code="400">Dados inválidos fornecidos</response>
    /// <response code="404">Bebê não encontrado</response>
    /// <response code="500">Erro interno do servidor</response>
    [HttpPut]
    [Route("Editar/{id}")]
    [ProducesResponseType(typeof(EditarBebeNascidoCommandResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> EditarBebeNascido([FromRoute] Guid id,
        [FromBody] EditarBebeNascidoDtos request)
    {
        var command = new EditarBebeNascidoCommand(
            id: id,
            responsavelId: request.ResponsavelId,
            nome: request.Nome,
            dataNascimento: request.DataNascimento,
            sexo: request.Sexo,
            tipoSanguineo: request.TipoSanguineo,
            idadeMeses: request.IdadeMeses,
            peso: request.Peso,
            altura: request.Altura
        );

        var response = await _mediator.Send(command);
        return StatusCode((int)response.StatusCode, response);
    }

    /// <summary>
    /// Exclui um bebê nascido do banco de dados
    /// </summary>
    /// <param name="id">ID do bebê a ser excluído</param>
    /// <returns>Sem conteúdo</returns>
    /// <response code="204">Bebê excluído com sucesso</response>
    /// <response code="404">Bebê não encontrado</response>
    /// <response code="500">Erro interno do servidor</response>
    [HttpDelete]
    [Route("Excluir/{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ExcluirBebeNascido([FromRoute] Guid id)
    {
        var command = new ExcluirBebeNascidoCommand(id);
        var response = await _mediator.Send(command);
        return StatusCode((int)response.StatusCode, response);
    }

    /// <summary>
    /// Lista todos os bebês nascidos de um responsável específico
    /// </summary>
    /// <param name="responsavelId">ID do responsável</param>
    /// <returns>Lista de bebês nascidos do responsável</returns>
    /// <response code="200">Lista retornada com sucesso</response>
    /// <response code="404">Responsável não encontrado</response>
    /// <response code="500">Erro interno do servidor</response>
    [HttpGet]
    [Route("ListarPorResponsavel/{responsavelId}")]
    [ProducesResponseType(typeof(List<ListarBebeNascidoPorResponsavelCommandResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ListarBebeNascidoPorResponsavel([FromRoute] Guid responsavelId)
    {
        var command = new ListarBebeNascidoPorResponsavelCommand(responsavelId);
        var response = await _mediator.Send(command);
        return StatusCode((int)response.StatusCode, response);
    }
}