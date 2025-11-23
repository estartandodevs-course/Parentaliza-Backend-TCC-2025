namespace Parentaliza.Application.CasosDeUso.EventoAgendaCasoDeUso.Editar;

public class EditarEventoAgendaCommandResponse
{
    public Guid Id { get; private set; }

    public EditarEventoAgendaCommandResponse(Guid id)
    {
        Id = id;
    }
}
