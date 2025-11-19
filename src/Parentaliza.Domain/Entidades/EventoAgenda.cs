namespace Parentaliza.Domain.Entidades;
public class EventoAgenda : Entity
{
    public string? Evento { get; private set; }
    public string? Especialidade { get; private set; }
    public string? Localizacao { get; private set; }
    public DateTime Data { get; private set; }
    public TimeSpan Hora { get; private set; }
    public string? Anotacao { get; private set; }

    public EventoAgenda() { }
    public EventoAgenda(string? evento, string? especialidade, string? localizacao, TimeSpan hora, DateTime data, string? anotacao)
    {
        Evento = evento;
        Especialidade = especialidade;
        Localizacao = localizacao;
        Hora = hora;
        Data = data;
        Anotacao = anotacao;
    }
}