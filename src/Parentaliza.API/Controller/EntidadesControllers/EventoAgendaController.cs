using MediatR;
using Microsoft.AspNetCore.Mvc;
using Parentaliza.API.Controller.Base;
using Parentaliza.API.Controller.Dtos;
using Parentaliza.Application.Mediator;
using Parentaliza.Application.CasosDeUso.EventoAgendaCasoDeUso.Criar;
using Parentaliza.Application.CasosDeUso.EventoAgendaCasoDeUso.Editar;
using Parentaliza.Application.CasosDeUso.EventoAgendaCasoDeUso.Excluir;
using Parentaliza.Application.CasosDeUso.EventoAgendaCasoDeUso.Listar;
using Parentaliza.Application.CasosDeUso.EventoAgendaCasoDeUso.ListarPorResponsavel;
using Parentaliza.Application.CasosDeUso.EventoAgendaCasoDeUso.Obter;

namespace Parentaliza.API.Controller.EntidadesControllers;

/// <summary>
/// Controller responsável pelo gerenciamento de eventos da agenda
/// </summary>
[ApiController]
[Route("api/EventoAgenda")]
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
    [Route("Criar")]
    [ProducesResponseType(typeof(CriarEventoAgendaCommandResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CriarEventoAgenda(
            [FromBody] CriarEventoAgendaDtos request)
    {
        var command = new CriarEventoAgendaCommand(
            responsavelId: request.ResponsavelId,
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
    /// Lista todos os eventos da agenda com paginação, filtros e ordenação
    /// </summary>
    /// <param name="page">Número da página (padrão: 1)</param>
    /// <param name="pageSize">Quantidade de itens por página (padrão: 10, máximo: 100)</param>
    /// <param name="responsavelId">Filtrar por ID do responsável</param>
    /// <param name="dataInicio">Filtrar por data inicial (formato: yyyy-MM-dd)</param>
    /// <param name="dataFim">Filtrar por data final (formato: yyyy-MM-dd)</param>
    /// <param name="especialidade">Filtrar por especialidade (busca parcial)</param>
    /// <param name="localizacao">Filtrar por localização (busca parcial)</param>
    /// <param name="sortBy">Campo para ordenar: "data", "evento" ou "especialidade" (padrão: "data")</param>
    /// <param name="sortOrder">Direção da ordenação: "asc" ou "desc" (padrão: "desc")</param>
    /// <returns>Lista paginada de todos os eventos cadastrados</returns>
    /// <response code="200">Lista de eventos retornada com sucesso</response>
    /// <response code="500">Erro interno do servidor</response>
    [HttpGet]
    [Route("Listar")]
    [ProducesResponseType(typeof(PagedResult<ListarEventoAgendaCommandResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ListarEventoAgenda(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] Guid? responsavelId = null,
        [FromQuery] DateTime? dataInicio = null,
        [FromQuery] DateTime? dataFim = null,
        [FromQuery] string? especialidade = null,
        [FromQuery] string? localizacao = null,
        [FromQuery] string? sortBy = null,
        [FromQuery] string sortOrder = "desc")
    {
        var command = new ListarEventoAgendaCommand(
            page: page,
            pageSize: pageSize,
            responsavelId: responsavelId,
            dataInicio: dataInicio,
            dataFim: dataFim,
            especialidade: especialidade,
            localizacao: localizacao,
            sortBy: sortBy,
            sortOrder: sortOrder);
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
    public async Task<IActionResult> ObterEventoAgendaPorId([FromRoute] Guid id)
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
        [FromRoute] Guid id,
        [FromBody] EditarEventoAgendaDtos request)
    {
        var command = new EditarEventoAgendaCommand(
            id: id,
            responsavelId: request.ResponsavelId,
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
    /// <returns>Sem conteúdo</returns>
    /// <response code="204">Evento excluído com sucesso</response>
    /// <response code="404">Evento não encontrado</response>
    /// <response code="500">Erro interno do servidor</response>
    [HttpDelete]
    [Route("Excluir/{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ExcluirEventoAgenda([FromRoute] Guid id)
    {
        var command = new ExcluirEventoAgendaCommand(id);
        var response = await _mediator.Send(command);
        return StatusCode((int)response.StatusCode, response);
    }

    /// <summary>
    /// Lista todos os eventos da agenda de um responsável específico
    /// </summary>
    /// <param name="responsavelId">ID do responsável</param>
    /// <returns>Lista de eventos do responsável</returns>
    /// <response code="200">Lista retornada com sucesso</response>
    /// <response code="404">Responsável não encontrado</response>
    /// <response code="500">Erro interno do servidor</response>
    [HttpGet]
    [Route("ListarPorResponsavel/{responsavelId}")]
    [ProducesResponseType(typeof(List<ListarEventoAgendaPorResponsavelCommandResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ListarEventoAgendaPorResponsavel([FromRoute] Guid responsavelId)
    {
        var command = new ListarEventoAgendaPorResponsavelCommand(responsavelId);
        var response = await _mediator.Send(command);
        return StatusCode((int)response.StatusCode, response);
    }
}