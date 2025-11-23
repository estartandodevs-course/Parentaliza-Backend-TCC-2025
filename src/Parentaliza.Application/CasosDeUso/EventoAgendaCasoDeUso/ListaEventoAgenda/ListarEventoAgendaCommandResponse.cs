namespace Parentaliza.Application.CasosDeUso.EventoAgendaCasoDeUso.ListaEventoAgenda;

public class ListarEventoAgendaCommandResponse
{
    public Guid Id { get; private set; }
    public string? Evento { get; private set; }
    public string? Especialidade { get; private set; }
    public string? Localizacao { get; private set; }
    public DateTime Data { get; private set; }
    public TimeSpan Hora { get; private set; }
    public string? Anotacao { get; private set; }

    public ListarEventoAgendaCommandResponse(Guid id, string? evento, string? especialidade, string? localizacao, DateTime data, TimeSpan hora, string? anotacao)
    {
        Id = id;
        Evento = evento;
        Especialidade = especialidade;
        Localizacao = localizacao;
        Data = data;
        Hora = hora;
        Anotacao = anotacao;
    }
}