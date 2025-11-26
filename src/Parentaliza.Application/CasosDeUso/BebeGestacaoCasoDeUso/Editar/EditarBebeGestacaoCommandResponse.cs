namespace Parentaliza.Application.CasosDeUso.BebeGestacaoCasoDeUso.Editar;

public class EditarBebeGestacaoCommandResponse
{
    public Guid Id { get; private set; }

    public EditarBebeGestacaoCommandResponse(Guid id)
    {
        Id = id;
    }
}