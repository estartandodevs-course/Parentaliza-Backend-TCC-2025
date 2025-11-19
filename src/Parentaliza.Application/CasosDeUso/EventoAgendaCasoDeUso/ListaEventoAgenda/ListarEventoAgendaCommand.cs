using MediatR;
using Parentaliza.Application.Mediator;

namespace Parentaliza.Application.CasosDeUso.EventoAgendaCasoDeUso.ListaEventoAgenda;

public class ListarEventoAgendaCommand : IRequest<CommandResponse<List<ListarEventoAgendaCommandResponse>>>
{
    public string? Evento { get; set; }
    public string? Especialidade { get; set; }
    public string? Localizacao { get; set; }
    public DateTime Data { get; set; }
    public TimeSpan Hora { get; set; }
    public string? Anotacao { get; set; }

    public ListarEventoAgendaCommand(string? evento, string? especialidade, string? localizacao, DateTime data, TimeSpan hora, string? anotacao)
    {
        Evento = evento;
        Especialidade = especialidade;
        Localizacao = localizacao;
        Data = data;
        Hora = hora;
        Anotacao = anotacao;
    }
}
