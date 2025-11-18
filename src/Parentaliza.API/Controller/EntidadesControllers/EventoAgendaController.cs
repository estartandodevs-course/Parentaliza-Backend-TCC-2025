using MediatR;
using Microsoft.AspNetCore.Mvc;
using Parentaliza.API.Controller.Base;
using Parentaliza.API.Controller.Dtos;
using Parentaliza.Application.CasosDeUso.EventoAgendaCasoDeUso.Criar;

namespace Parentaliza.API.Controller.EntidadesControllers;

[ApiController]
[Route("[controller]")]
public class EventoAgendaController : BaseController
{
    private readonly IMediator _mediator;

    public EventoAgendaController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> AdicionaEventoAgenda(
            [FromBody] CriarEventoAgendaDtos request) 
    {
        // Combinar Data e Hora em um DateTime para o horário
        var horarioCompleto = request.Data.Date.Add(request.Hora);

        var command = new CriarEventoAgendaCommand(
            evento: request.Evento,
            especialidade: request.Especialidade,
            localizacao: request.Localizacao,
            data: request.Data,
            horario: horarioCompleto,
            anotacao: request.Anotacao
            );

        var response = await _mediator.Send(command);

        return StatusCode((int)response.StatusCode, response);
    }
}
