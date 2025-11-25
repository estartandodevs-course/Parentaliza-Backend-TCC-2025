namespace Parentaliza.Application.CasosDeUso.ResponsavelCasoDeUso.Editar;

public class EditarResponsavelCommandResponse
{
    public Guid Id { get; private set; }

    public EditarResponsavelCommandResponse(Guid id)
    {
        Id = id;
    }
}

