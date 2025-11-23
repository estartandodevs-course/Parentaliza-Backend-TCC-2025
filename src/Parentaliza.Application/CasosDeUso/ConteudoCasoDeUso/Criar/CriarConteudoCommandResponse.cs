namespace Parentaliza.Application.CasosDeUso.ConteudoCasoDeUso.Criar;

public class CriarConteudoCommandResponse
{
    public Guid Id { get; private set; }

    public CriarConteudoCommandResponse(Guid id)
    {
        Id = id;
    }
}
