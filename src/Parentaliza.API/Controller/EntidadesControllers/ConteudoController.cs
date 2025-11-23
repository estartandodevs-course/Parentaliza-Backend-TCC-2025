using MediatR;
using Microsoft.AspNetCore.Mvc;
using Parentaliza.API.Controller.Base;
using Parentaliza.API.Controller.Dtos;
using Parentaliza.Application.CasosDeUso.BebeNascidoCasoDeUso.Criar;
using Parentaliza.Application.CasosDeUso.ConteudoCasoDeUso.Criar;
using Parentaliza.Application.CasosDeUso.ConteudoCasoDeUso.Editar;
using Parentaliza.Application.CasosDeUso.ConteudoCasoDeUso.Excluir;
using Parentaliza.Application.CasosDeUso.ConteudoCasoDeUso.Listar;

namespace Parentaliza.API.Controller.EntidadesControllers;

[ApiController]
[Route("[controller]")]
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
    [Route("AdicionarInformacoes")]
    [ProducesResponseType(typeof(CriarBebeNascidoCommandResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> AdicionaConteudo(
        [FromBody] CriarConteudoDtos request)
    {
        var command = new CriarConteudoCommand(
        titulo: request.Titulo,
        categoria: request.Categoria,
        dataPublicacao: request.DataPublicacao,
        descricao: request.Descricao
        );

        var response = await _mediator.Send(command);

        return Ok();
    }

    /// <summary>
    /// Obtém todos os conteudos do bando de dados
    /// </summary>
    /// <returns>Lista de todos os conteudos cadastrados</returns>
    /// <response code="200">Lista de conteudos retornada com sucesso</response>
    /// <response code="500">Erro interno do servidor</response>
    [HttpGet]
    [Route("ObterInformacoes")]
    [ProducesResponseType(typeof(List<ListarConteudoCommandResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ObterTodosConteudos()
    {
        var command = new ListarConteudoCommand();
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
    [Route("EditarInformacoes/{id}")]
    [ProducesResponseType(typeof(EditarConteudoCommandResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> EditarConteudo(
        Guid id,
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
    [Route("ExcluirInformacoes/{id}")]
    [ProducesResponseType(typeof(ExcluirConteudoCommand), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]

    public async Task<IActionResult> ExcluirConteudo(Guid id)
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