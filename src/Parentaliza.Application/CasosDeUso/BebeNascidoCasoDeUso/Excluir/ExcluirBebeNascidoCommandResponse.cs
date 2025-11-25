namespace Parentaliza.Application.CasosDeUso.BebeNascidoCasoDeUso.Excluir;

public class ExcluirBebeNascidoCommandResponse
{
    public Guid Id { get; private set; }

    public ExcluirBebeNascidoCommandResponse(Guid id)
    {
        Id = id;
    }
}
