namespace Parentaliza.Domain.Entidades;
public class EventoAgenda : Entity
{
    public Guid ResponsavelId { get; private set; }
    public string? Evento { get; private set; }
    public string? Especialidade { get; private set; }
    public string? Localizacao { get; private set; }
    public DateTime Data { get; private set; }
    public TimeSpan Hora { get; private set; }
    public string? Anotacao { get; private set; }
    public Responsavel? Responsavel { get; private set; }

    public EventoAgenda() { }
    public EventoAgenda(Guid responsavelId, string? evento, string? especialidade, string? localizacao, TimeSpan hora, DateTime data, string? anotacao)
    {
        if (responsavelId == Guid.Empty) throw new ArgumentException("O ID do responsável é obrigatório.", nameof(responsavelId));

        if (string.IsNullOrWhiteSpace(evento))
            throw new ArgumentException("O evento é obrigatório.", nameof(evento));

        if (string.IsNullOrWhiteSpace(especialidade))
            throw new ArgumentException("A especialidade é obrigatória.", nameof(especialidade));

        if (string.IsNullOrWhiteSpace(localizacao))
            throw new ArgumentException("A localização é obrigatória.", nameof(localizacao));

        var dataHoraCompleta = data.Date.Add(hora);
        if (dataHoraCompleta < DateTime.UtcNow)
        {
            throw new ArgumentException("A data e hora do evento não podem ser no passado.", nameof(data));
        }

        ResponsavelId = responsavelId;
        Evento = evento;
        Especialidade = especialidade;
        Localizacao = localizacao;
        Hora = hora;
        Data = data;
        Anotacao = anotacao;
    }
}