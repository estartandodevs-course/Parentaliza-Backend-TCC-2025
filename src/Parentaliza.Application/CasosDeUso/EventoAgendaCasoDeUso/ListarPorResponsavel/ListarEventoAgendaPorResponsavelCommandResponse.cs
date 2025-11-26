namespace Parentaliza.Application.CasosDeUso.EventoAgendaCasoDeUso.ListarPorResponsavel;

public class ListarEventoAgendaPorResponsavelCommandResponse
{
    public Guid Id { get; private set; }
    public Guid ResponsavelId { get; private set; }
    public string? Evento { get; private set; }
    public string? Especialidade { get; private set; }
    public string? Localizacao { get; private set; }
    public DateTime Data { get; private set; }
    public TimeSpan Hora { get; private set; }
    public string? Anotacao { get; private set; }

    public ListarEventoAgendaPorResponsavelCommandResponse(
        Guid id,
        Guid responsavelId,
        string? evento,
        string? especialidade,
        string? localizacao,
        DateTime data,
        TimeSpan hora,
        string? anotacao)
    {
        Id = id;
        ResponsavelId = responsavelId;
        Evento = evento;
        Especialidade = especialidade;
        Localizacao = localizacao;
        Data = data;
        Hora = hora;
        Anotacao = anotacao;
    }
}