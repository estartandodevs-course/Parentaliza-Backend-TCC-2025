namespace Parentaliza.Application.CasosDeUso.EventoAgendaCasoDeUso.Excluir;

public class ExcluirEventoAgendaCommandResponse
{
    public Guid Id { get; private set; }

    public ExcluirEventoAgendaCommandResponse(Guid id)
    {
        Id = id;
    }
}
