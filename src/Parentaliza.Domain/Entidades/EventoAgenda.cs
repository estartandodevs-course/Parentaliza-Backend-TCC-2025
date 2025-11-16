namespace Parentaliza.Domain.Entidades;
public class EventoAgenda : Entity
{
    public string? Evento { get; set; }
    public string? Especialidade { get; set; }
    public string? Localizacao { get; set; }
    public DateTime Data { get; set; } = DateTime.Now;
    public DateTime Hora { get; set; } = DateTime.Now;
    public string? Anotacao { get; set; }

    public EventoAgenda(string? evento, string? especialidade, string? localizacao, DateTime hora, DateTime data, string? anotacao)
    {
        Evento = evento;
        Especialidade = especialidade;
        Localizacao = localizacao;
        Hora = hora;
        Data = data;
        Anotacao = anotacao;
    }
}