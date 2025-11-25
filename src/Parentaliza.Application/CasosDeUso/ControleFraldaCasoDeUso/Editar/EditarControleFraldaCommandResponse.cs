namespace Parentaliza.Application.CasosDeUso.ControleFraldaCasoDeUso.Editar;

public class EditarControleFraldaCommandResponse
{
    public Guid Id { get; private set; }

    public EditarControleFraldaCommandResponse(Guid id)
    {
        Id = id;
    }
}

