namespace Parentaliza.Application.CasosDeUso.ResponsavelCasoDeUso.Excluir;

public class ExcluirResponsavelCommandResponse
{
    public Guid Id { get; private set; }

    public ExcluirResponsavelCommandResponse(Guid id)
    {
        Id = id;
    }
}