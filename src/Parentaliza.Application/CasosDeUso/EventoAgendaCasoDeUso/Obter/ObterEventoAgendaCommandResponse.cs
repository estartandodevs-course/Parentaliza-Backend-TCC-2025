namespace Parentaliza.Application.CasosDeUso.EventoAgendaCasoDeUso.Obter;

public class ObterEventoAgendaCommandResponse
{
    public Guid Id { get; private set; }

    public ObterEventoAgendaCommandResponse(Guid id)
    {
        Id = id;
    }
}