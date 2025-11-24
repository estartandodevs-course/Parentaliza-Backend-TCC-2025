namespace Parentaliza.Application.CasosDeUso.BebeGestacaoCasoDeUso.Excluir;

public class ExcluirBebeGestacaoCommandResponse
{
    public Guid Id { get; private set; }

    public ExcluirBebeGestacaoCommandResponse(Guid id)
    {
        Id = id;
    }
}
