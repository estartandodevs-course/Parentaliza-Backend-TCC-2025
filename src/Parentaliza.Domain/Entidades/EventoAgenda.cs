namespace Parentaliza.Domain.Entidades;
public class EventoAgenda : Entity
{
    public string? Evento { get; set; }
    public string? Especialidade { get; set; }
    public string? Localizacao { get; set; }
    public  DateTime  Data { get; set; }
    public DateTime Horario { get; set; }
    public string? Anotacao { get; set; }

    public EventoAgenda(string? evento, string? especialidade, string? localizacao, DateTime horario, DateTime data, string? anotacao)
    {
        Evento = evento;
        Especialidade = especialidade;
        Localizacao = localizacao;
        Horario = horario;
        Data = data;
        Anotacao = anotacao;
    }
}