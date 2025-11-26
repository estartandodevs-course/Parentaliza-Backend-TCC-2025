namespace Parentaliza.Application.CasosDeUso.EventoAgendaCasoDeUso.Criar;

public class CriarEventoAgendaCommandResponse
{
    public Guid Id { get; private set; }

    public CriarEventoAgendaCommandResponse(Guid id)
    {
        Id = id;
    }
}