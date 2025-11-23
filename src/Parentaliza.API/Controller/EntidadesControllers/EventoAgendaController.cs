using MediatR;
using Microsoft.AspNetCore.Mvc;
using Parentaliza.API.Controller.Base;
using Parentaliza.API.Controller.Dtos;
using Parentaliza.Application.CasosDeUso.EventoAgendaCasoDeUso.Criar;
using Parentaliza.Application.CasosDeUso.EventoAgendaCasoDeUso.Editar;
using Parentaliza.Application.CasosDeUso.EventoAgendaCasoDeUso.Excluir;
using Parentaliza.Application.CasosDeUso.EventoAgendaCasoDeUso.ListaEventoAgenda;
using Parentaliza.Application.CasosDeUso.EventoAgendaCasoDeUso.Obter;

namespace Parentaliza.API.Controller.EntidadesControllers;

/// <summary>
/// Controller responsável pelo gerenciamento de eventos da agenda
/// </summary>
[ApiController]
[Route("api/EventoAgenda/[controller]")]
[Produces("application/json")]
public class EventoAgendaController : BaseController
{
    private readonly IMediator _mediator;

    public EventoAgendaController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Cria um novo evento na agenda
    /// </summary>
    /// <param name="request">Dados do evento a ser criado</param>
    /// <returns>Retorna o ID do evento criado</returns>
    /// <response code="201">Evento criado com sucesso</response>
    /// <response code="400">Dados inválidos fornecidos</response>
    /// <response code="409">Nome do evento já está em uso</response>
    /// <response code="500">Erro interno do servidor</response>
    [HttpPost]
    [Route("Adicionar")]
    [ProducesResponseType(typeof(CriarEventoAgendaCommandResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> AdicionaEventoAgenda(
            [FromBody] CriarEventoAgendaDtos request)
    {
        var command = new CriarEventoAgendaCommand(
            evento: request.Evento,
            especialidade: request.Especialidade,
            localizacao: request.Localizacao,
            data: request.Data,
            hora: request.Hora,
            anotacao: request.Anotacao
            );

        var response = await _mediator.Send(command);

        return StatusCode((int)response.StatusCode, response);
    }

    /// <summary>
    /// Obtém todos os eventos da agenda
    /// </summary>
    /// <returns>Lista de todos os eventos cadastrados</returns>
    /// <response code="200">Lista de eventos retornada com sucesso</response>
    /// <response code="500">Erro interno do servidor</response>
    [HttpGet]
    [Route("ObterTodos")]
    [ProducesResponseType(typeof(List<ListarEventoAgendaCommandResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ObterTodosEventoAgenda()
    {
        var command = new ListarEventoAgendaCommand();
        var response = await _mediator.Send(command);
        return StatusCode((int)response.StatusCode, response);
    }

    /// <summary>
    /// Obtém um evento específico da agenda pelo ID
    /// </summary>
    /// <param name="id">ID do evento a ser obtido</param>
    /// <returns>Dados do evento encontrado</returns>
    /// <response code="200">Evento encontrado e retornado com sucesso</response>
    /// <response code="404">Evento não encontrado</response>
    /// <response code="500">Erro interno do servidor</response>
    [HttpGet]
    [Route("Obter/{id}")]
    [ProducesResponseType(typeof(ObterEventoAgendaCommandResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ObterEventoAgendaPorId(Guid id)
    {
        var command = new ObterEventoAgendaCommand(id);
        var response = await _mediator.Send(command);
        return StatusCode((int)response.StatusCode, response);
    }

    /// <summary>
    /// Atualiza um evento existente na agenda
    /// </summary>
    /// <param name="id">ID do evento a ser atualizado</param>
    /// <param name="request">Dados atualizados do evento</param>
    /// <returns>ID do evento atualizado</returns>
    /// <response code="200">Evento atualizado com sucesso</response>
    /// <response code="400">Dados inválidos fornecidos</response>
    /// <response code="404">Evento não encontrado</response>
    /// <response code="409">Nome do evento já está em uso por outro evento</response>
    /// <response code="500">Erro interno do servidor</response>
    [HttpPut]
    [Route("Editar/{id}")]
    [ProducesResponseType(typeof(EditarEventoAgendaCommandResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> EditarEventoAgenda(
        Guid id,
        [FromBody] EditarEventoAgendaDtos request)
    {
        var command = new EditarEventoAgendaCommand(
            id: id,
            evento: request.Evento,
            especialidade: request.Especialidade,
            localizacao: request.Localizacao,
            data: request.Data,
            hora: request.Hora,
            anotacao: request.Anotacao
        );

        var response = await _mediator.Send(command);
        return StatusCode((int)response.StatusCode, response);
    }

    /// <summary>
    /// Exclui um evento da agenda
    /// </summary>
    /// <param name="id">ID do evento a ser excluído</param>
    /// <returns>ID do evento excluído</returns>
    /// <response code="200">Evento excluído com sucesso</response>
    /// <response code="404">Evento não encontrado</response>
    /// <response code="500">Erro interno do servidor</response>
    [HttpDelete]
    [Route("Excluir/{id}")]
    [ProducesResponseType(typeof(ExcluirEventoAgendaCommandResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ExcluirEventoAgenda(Guid id)
    {
        var command = new ExcluirEventoAgendaCommand(id);
        var response = await _mediator.Send(command);
        return StatusCode((int)response.StatusCode, response);
    }
}