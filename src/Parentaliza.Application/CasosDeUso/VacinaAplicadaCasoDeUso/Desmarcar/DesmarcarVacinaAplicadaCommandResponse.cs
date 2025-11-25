namespace Parentaliza.Application.CasosDeUso.VacinaAplicadaCasoDeUso.Desmarcar;

public class DesmarcarVacinaAplicadaCommandResponse
{
    public Guid Id { get; private set; }

    public DesmarcarVacinaAplicadaCommandResponse(Guid id)
    {
        Id = id;
    }
}

