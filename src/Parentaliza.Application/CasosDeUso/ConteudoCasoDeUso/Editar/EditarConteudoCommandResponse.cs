namespace Parentaliza.Application.CasosDeUso.ConteudoCasoDeUso.Editar;

public class EditarConteudoCommandResponse
{
    public Guid Id { get; private set; }

    public EditarConteudoCommandResponse(Guid id)
    {
        Id = id;
    }
}
