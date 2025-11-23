namespace Parentaliza.Application.CasosDeUso.BebeNascidoCasoDeUso.Editar;

public class EditarBebeNascidoCommandResponse
{
    public Guid Id { get; private set; }

    public EditarBebeNascidoCommandResponse(Guid id)
    {
        Id = id;
    }
}
