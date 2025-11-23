using MediatR;
using Microsoft.AspNetCore.Mvc;
using Parentaliza.API.Controller.Base;
using Parentaliza.API.Controller.Dtos;
using Parentaliza.Application.CasosDeUso.BebeNascidoCasoDeUso.Criar;
using Parentaliza.Application.CasosDeUso.BebeNascidoCasoDeUso.Editar;
using Parentaliza.Application.CasosDeUso.BebeNascidoCasoDeUso.Obter;
using Parentaliza.Application.CasosDeUso.EventoAgendaCasoDeUso.Editar;
using Parentaliza.Application.CasosDeUso.PerfilBebe.Obter;

namespace Parentaliza.API.Controller.EntidadesControllers;

/// <summary>
/// Controller responsável pelo aramazinamento das informações do bebê nascido
/// </summary>
[ApiController]
[Route("api/BebeNascido/[controller]")]
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
    [Route("AdicionarInformacoes")]
    [ProducesResponseType(typeof(CriarBebeNascidoCommandResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> AdicionarInformacoesBebeNascido(
            [FromBody] CriarBebeNascidoDtos request)
    {
        var command = new CriarBebeNascidoCommand(
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
    /// <response code="201">Busca realizada com sucesso</response>
    /// <response code="400">Dados inválidos fornecidos</response>
    /// <response code="500">Erro interno do servidor</response>
    [HttpGet]
    [Route("ObterInformacoes/{id}")]
    [ProducesResponseType(typeof(ObterBebeNascidoCommandResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ObterInformacoesBebeNascidoPorId(Guid id)
    {
        var command = new ObterBebeNascidoCommand(id);
        var response = await _mediator.Send(command);
        return StatusCode((int)response.StatusCode, response);
    }

    /// <summary>
    /// Atualiza as informações do bebê nascido no banco de dados
    /// </summary>
    /// <param name="idBebeNascido">ID do bebê a ser atualizado</param>
    /// <param name="request">Dados atualizados do bebê</param>
    /// <returns>ID do bebê atualizado</returns>
    /// <response code="200">Informações atualizadas com sucesso</response>
    /// <response code="400">Dados inválidos fornecidos</response>
    /// <response code="404">Evento não encontrado</response>
    /// <response code="500">Erro interno do servidor</response>
    [HttpPut]
    [Route("EditarInformacoes/{id}")]
    [ProducesResponseType(typeof(EditarEventoAgendaCommandResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> EditarBebeNascido(Guid idBebeNascido,
        [FromBody] EditarBebeNascidoDtos request)
    {
        var command = new EditarBebeNascidoCommand(
            id: idBebeNascido,
            request.Nome,
            request.DataNascimento,
            request.Sexo,
            request.TipoSanguineo,
            request.IdadeMeses,
            request.Peso,
            request.Altura
        );

        var response = await _mediator.Send(command);
        return StatusCode((int)response.StatusCode, response);
    }
}