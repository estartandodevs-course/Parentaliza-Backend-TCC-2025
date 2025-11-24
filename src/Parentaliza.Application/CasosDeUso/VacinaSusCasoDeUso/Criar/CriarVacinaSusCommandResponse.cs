namespace Parentaliza.Application.CasosDeUso.VacinaSusCasoDeUso.Criar;

public class CriarVacinaSusCommandResponse
{
    public Guid Id { get; private set; }

    public CriarVacinaSusCommandResponse(Guid id)
    {
        Id = id;
    }
}

