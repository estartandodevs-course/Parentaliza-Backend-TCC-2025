using MediatR;
using Microsoft.AspNetCore.Mvc;
using Parentaliza.API.Controller.Base;
using Parentaliza.API.Controller.Dtos;
using Parentaliza.Application.CasosDeUso.BebeGestacaoCasoDeUso.Criar;
using Parentaliza.Application.CasosDeUso.BebeNascidoCasoDeUso.Criar;
using Parentaliza.Application.CasosDeUso.EventoAgendaCasoDeUso.Editar;
using Parentaliza.Application.CasosDeUso.PerfilBebeGestacaoCasoDeUso.Editar;
using Parentaliza.Application.CasosDeUso.PerfilBebeGestacaoCasoDeUso.Obter;

namespace Parentaliza.API.Controller.EntidadesControllers;

/// <summary>
/// Controller responsável pelo aramazinamento das informações do bebê gestação
/// </summary>
[ApiController]
[Route("[controller]")]
public class BebeGestacaoController : BaseController
{
    private readonly IMediator _mediator;

    public BebeGestacaoController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Armazena as informações do bebê gestação
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
    public async Task<IActionResult> AdicionarInformacoesBebeGestacao(
            [FromBody] CriarBebeGestacaoDtos request)
    {
        var command = new CriarBebeGestacaoCommand(
            nome: request.Nome,
            dataPrevista: request.DataPrevista,
            diasDeGestacao: request.DiasDeGestacao,
            pesoEstimado: request.PesoEstimado,
            comprimentoEstimado: request.ComprimentoEstimado
            );

        var response = await _mediator.Send(command);

        return StatusCode((int)response.StatusCode, response);
    }

    /// <summary>
    /// Busca as informações do bebê em fase de gestação no banco de dados
    /// </summary>
    /// <param name="id">Obter dados do bebê em fase de gestação pelo ID</param>
    /// <response code="201">Busca realizada com sucesso</response>
    /// <response code="400">Dados inválidos fornecidos</response>
    /// <response code="500">Erro interno do servidor</response>
    [HttpGet]
    [Route("ObterInformacoes/{id}")]
    [ProducesResponseType(typeof(CriarBebeNascidoCommandResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ObterInformacoesBebeGestacaoPorId(Guid id)
    {
        var command = new ObterBebeGestacaoCommand(id);
        var response = await _mediator.Send(command);
        return StatusCode((int)response.StatusCode, response);
    }

    /// <summary>
    /// Atualiza as informações do bebê em fase de gestação no banco de dados
    /// </summary>
    /// <param name="idBebeGestacao">ID do bebê a ser atualizado</param>
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
    public async Task<IActionResult> EditarBebeGestacao(Guid idBebeGestacao,
        [FromBody] EditarBebeGestacaoDtos request)
    {
        var command = new EditarBebeGestacaoCommand(
            id: idBebeGestacao,
            request.Nome,
            request.DataPrevista,
            request.DiasDeGestacao,
            request.PesoEstimado,
            request.ComprimentoEstimado
        );

        var response = await _mediator.Send(command);
        return StatusCode((int)response.StatusCode, response);
    }
}