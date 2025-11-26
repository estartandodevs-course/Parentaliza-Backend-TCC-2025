namespace Parentaliza.Application.CasosDeUso.BebeNascidoCasoDeUso.Criar;

public class CriarBebeNascidoCommandResponse
{
    public Guid Id { get; private set; }

    public CriarBebeNascidoCommandResponse(Guid id)
    {
        Id = id;
    }
}