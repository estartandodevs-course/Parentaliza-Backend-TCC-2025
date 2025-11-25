namespace Parentaliza.Application.CasosDeUso.VacinaSusCasoDeUso.Excluir;

public class ExcluirVacinaSusCommandResponse
{
    public Guid Id { get; private set; }

    public ExcluirVacinaSusCommandResponse(Guid id)
    {
        Id = id;
    }
}

