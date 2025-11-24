namespace Parentaliza.Application.CasosDeUso.ExameRealizadoCasoDeUso.MarcarRealizado;

public class MarcarExameRealizadoCommandResponse
{
    public Guid Id { get; private set; }

    public MarcarExameRealizadoCommandResponse(Guid id)
    {
        Id = id;
    }
}

