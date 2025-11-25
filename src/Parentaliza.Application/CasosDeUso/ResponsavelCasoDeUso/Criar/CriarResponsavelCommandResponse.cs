namespace Parentaliza.Application.CasosDeUso.ResponsavelCasoDeUso.Criar;

public class CriarResponsavelCommandResponse
{
    public Guid Id { get; private set; }

    public CriarResponsavelCommandResponse(Guid id)
    {
        Id = id;
    }
}

