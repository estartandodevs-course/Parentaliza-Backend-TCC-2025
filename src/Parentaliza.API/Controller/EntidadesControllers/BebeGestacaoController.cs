using MediatR;
using Microsoft.AspNetCore.Mvc;
using Parentaliza.API.Controller.Base;
using Parentaliza.API.Controller.Dtos;
using Parentaliza.Application.CasosDeUso.BebeGestacaoCasoDeUso.Criar;
using Parentaliza.Application.CasosDeUso.BebeGestacaoCasoDeUso.ConverterParaNascido;
using Parentaliza.Application.CasosDeUso.BebeGestacaoCasoDeUso.ListarPorResponsavel;
using Parentaliza.Application.CasosDeUso.BebeGestacaoCasoDeUso.Editar;
using Parentaliza.Application.CasosDeUso.BebeGestacaoCasoDeUso.Excluir;
using Parentaliza.Application.CasosDeUso.BebeGestacaoCasoDeUso.Obter;

namespace Parentaliza.API.Controller.EntidadesControllers;

/// <summary>
/// Controller responsável pelo armazenamento das informações do bebê gestação
/// </summary>
[ApiController]
[Route("api/BebeGestacao")]
[Produces("application/json")]
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
    [Route("Criar")]
    [ProducesResponseType(typeof(CriarBebeGestacaoCommandResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CriarBebeGestacao(
            [FromBody] CriarBebeGestacaoDtos request)
    {
        var command = new CriarBebeGestacaoCommand(
            responsavelId: request.ResponsavelId,
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
    /// <response code="200">Busca realizada com sucesso</response>
    /// <response code="400">Dados inválidos fornecidos</response>
    /// <response code="404">Bebê não encontrado</response>
    /// <response code="500">Erro interno do servidor</response>
    [HttpGet]
    [Route("Obter/{id}")]
    [ProducesResponseType(typeof(ObterBebeGestacaoCommandResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ObterBebeGestacaoPorId([FromRoute] Guid id)
    {
        var command = new ObterBebeGestacaoCommand(id);
        var response = await _mediator.Send(command);
        return StatusCode((int)response.StatusCode, response);
    }

    /// <summary>
    /// Atualiza as informações do bebê em fase de gestação no banco de dados
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
    [ProducesResponseType(typeof(EditarBebeGestacaoCommandResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> EditarBebeGestacao([FromRoute] Guid id,
        [FromBody] EditarBebeGestacaoDtos request)
    {
        var command = new EditarBebeGestacaoCommand(
            id: id,
            responsavelId: request.ResponsavelId,
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
    /// Exclui um bebê gestação do banco de dados
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
    public async Task<IActionResult> ExcluirBebeGestacao([FromRoute] Guid id)
    {
        var command = new ExcluirBebeGestacaoCommand(id);
        var response = await _mediator.Send(command);
        return StatusCode((int)response.StatusCode, response);
    }

    /// <summary>
    /// Converte um bebê em gestação para bebê nascido quando o bebê nasce
    /// </summary>
    /// <param name="id">ID do bebê em gestação a ser convertido</param>
    /// <param name="request">Dados adicionais do nascimento (data, sexo, tipo sanguíneo, peso, altura, etc)</param>
    /// <returns>ID do bebê nascido criado e status da exclusão do registro de gestação</returns>
    /// <response code="201">Bebê convertido com sucesso</response>
    /// <response code="400">Dados inválidos fornecidos</response>
    /// <response code="404">Bebê em gestação não encontrado</response>
    /// <response code="409">Já existe um bebê nascido com este nome</response>
    /// <response code="500">Erro interno do servidor</response>
    [HttpPost]
    [Route("ConverterParaNascido/{id}")]
    [ProducesResponseType(typeof(ConverterBebeGestacaoParaNascidoCommandResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ConverterBebeGestacaoParaNascido(
        [FromRoute] Guid id,
        [FromBody] ConverterBebeGestacaoParaNascidoDtos request)
    {
        var command = new ConverterBebeGestacaoParaNascidoCommand(
            bebeGestacaoId: id,
            dataNascimento: request.DataNascimento,
            sexo: request.Sexo,
            tipoSanguineo: request.TipoSanguineo,
            idadeMeses: request.IdadeMeses,
            peso: request.Peso,
            altura: request.Altura,
            excluirBebeGestacao: request.ExcluirBebeGestacao
        );

        var response = await _mediator.Send(command);
        return StatusCode((int)response.StatusCode, response);
    }

    /// <summary>
    /// Lista todos os bebês em gestação de um responsável específico
    /// </summary>
    /// <param name="responsavelId">ID do responsável</param>
    /// <returns>Lista de bebês em gestação do responsável</returns>
    /// <response code="200">Lista retornada com sucesso</response>
    /// <response code="404">Responsável não encontrado</response>
    /// <response code="500">Erro interno do servidor</response>
    [HttpGet]
    [Route("ListarPorResponsavel/{responsavelId}")]
    [ProducesResponseType(typeof(List<ListarBebeGestacaoPorResponsavelCommandResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ListarBebeGestacaoPorResponsavel([FromRoute] Guid responsavelId)
    {
        var command = new ListarBebeGestacaoPorResponsavelCommand(responsavelId);
        var response = await _mediator.Send(command);
        return StatusCode((int)response.StatusCode, response);
    }
}