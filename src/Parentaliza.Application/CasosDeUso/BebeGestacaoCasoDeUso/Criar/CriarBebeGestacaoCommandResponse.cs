namespace Parentaliza.Application.CasosDeUso.BebeGestacaoCasoDeUso.Criar;

public class CriarBebeGestacaoCommandResponse
{
    public Guid Id { get; private set; }

    public CriarBebeGestacaoCommandResponse(Guid id)
    {
        Id = id;
    }
}
