using MediatR;
using Microsoft.AspNetCore.Mvc;
using Parentaliza.API.Controller.Base;
using Parentaliza.API.Controller.Dtos;
using Parentaliza.Application.CasosDeUso.EventoAgendaCasoDeUso.Criar;
using Parentaliza.Application.CasosDeUso.EventoAgendaCasoDeUso.ListaEventoAgenda;
using Parentaliza.Application.CasosDeUso.EventoAgendaCasoDeUso.Obter;

namespace Parentaliza.API.Controller.EntidadesControllers;

[ApiController]
[Route("api/EventoAgenda/[controller]")]
public class EventoAgendaController : BaseController
{
    private readonly IMediator _mediator;

    public EventoAgendaController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Route("Adicionar")]
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

    [HttpGet]
    [Route("ObterTodos")]
    public async Task<IActionResult> ObterTodosEventoAgenda()
    {
        var command = new ListarEventoAgendaCommandResponse();
        var response = await _mediator.Send(command, CancellationToken.None);
        return StatusCode((int)response.StatusCode, response);
    }

    /*
     * [HttpGet]
    [Route("BuscarId/EventoAgendaId/{id}")]
    public async Task<ActionResult> GetEventoAgendaId(Guid id)
    {
        var command = new ObterEventoAgendaCommandResponse(id);
        var response = await _mediator.Send(command, CancellationToken.None);
        return StatusCode((int)response.StatusCode, response);
    }
    */
}