namespace Parentaliza.Application.CasosDeUso.VacinaAplicadaCasoDeUso.MarcarAplicada;

public class MarcarVacinaAplicadaCommandResponse
{
    public Guid Id { get; private set; }

    public MarcarVacinaAplicadaCommandResponse(Guid id)
    {
        Id = id;
    }
}

