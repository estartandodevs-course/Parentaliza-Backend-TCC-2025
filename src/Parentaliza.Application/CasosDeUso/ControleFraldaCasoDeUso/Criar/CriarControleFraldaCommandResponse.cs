namespace Parentaliza.Application.CasosDeUso.ControleFraldaCasoDeUso.Criar;

public class CriarControleFraldaCommandResponse
{
    public Guid Id { get; private set; }

    public CriarControleFraldaCommandResponse(Guid id)
    {
        Id = id;
    }
}