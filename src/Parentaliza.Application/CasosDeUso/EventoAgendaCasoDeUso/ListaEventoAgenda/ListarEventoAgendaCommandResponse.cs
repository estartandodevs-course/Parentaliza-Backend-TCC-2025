namespace Parentaliza.Application.CasosDeUso.EventoAgendaCasoDeUso.ListaEventoAgenda;

public class ListarEventoAgendaCommandResponse
{
    public string? Evento { get; set; } = string.Empty;
    public string? Especialidade { get; set; } = string.Empty;
    public string? Localizacao { get; set; } = string.Empty;
    public DateTime Data { get; set; } = DateTime.Now;
    public DateTime Hora { get; set; } = DateTime.Now;
    public string? Anotacao { get; set; } = string.Empty;

    public ListarEventoAgendaCommandResponse(string? evento, string? especialidade, string? localizacao, DateTime data, DateTime hora, string? anotacao)
    {
        Evento = evento;
        Especialidade = especialidade;
        Localizacao = localizacao;
        Data = data;
        Hora = hora;
        Anotacao = anotacao;
    }
}
