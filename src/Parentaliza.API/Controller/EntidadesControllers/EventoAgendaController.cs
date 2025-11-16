using MediatR;
using Microsoft.AspNetCore.Mvc;
using Parentaliza.API.Controller.Base;
using Parentaliza.API.Controller.Dtos;
using Parentaliza.Application.CasosDeUso.EventoAgendaCasoDeUso.Criar;
using Parentaliza.Application.CasosDeUso.EventoAgendaCasoDeUso.ListaEventoAgenda;
using Parentaliza.Application.CasosDeUso.EventoAgendaCasoDeUso.Obter;

namespace Parentaliza.API.Controller.EntidadesControllers;

[ApiController]
[Route("api/agendamento/[controller]")]
public class EventoAgendaController : BaseController
{
    private readonly IMediator _mediator;

    public EventoAgendaController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet]
    [Route("EventoAgenda")]

    public async Task<IActionResult> ObterEventoAgenda()
    {
        var query = new ListarEventoAgendaCommand();
        var response = await _mediator.Send(query);
        return StatusCode((int)response.StatusCode, response);
    }

    [HttpGet]
    [Route("EventoAgendaId/{id}")]
    public async Task<IActionResult> GetEventoAgendaId(Guid id)
    {
        var command = new ObterEventoAgendaCommand(id);
        var response = await _mediator.Send(command, CancellationToken.None);
        if (response == null) //retorna 404 se não encontrar. Retornando direto para response não funciona.
            return NotFound();
        return Ok(response);

    }
    [HttpPost]
    public async Task<IActionResult> AdicionaEventoAgenda(
            [FromBody] CriarEventoAgendaDtos request)
    {
        var command = new CriarEventoAgendaCommand(
            evento: request.Evento,
            especialidade: request.Especialidade,
            localizacao: request.Localizacao,
            data: request.Data = DateTime.Now,
            hora: request.Hora,
            anotacao: request.Anotacao
            );

        var response = await _mediator.Send(command);

        return StatusCode((int)response.StatusCode, response);
    }
}
