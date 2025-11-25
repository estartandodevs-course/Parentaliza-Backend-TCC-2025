namespace Parentaliza.Application.CasosDeUso.VacinaSusCasoDeUso.Editar;

public class EditarVacinaSusCommandResponse
{
    public Guid Id { get; private set; }

    public EditarVacinaSusCommandResponse(Guid id)
    {
        Id = id;
    }
}

