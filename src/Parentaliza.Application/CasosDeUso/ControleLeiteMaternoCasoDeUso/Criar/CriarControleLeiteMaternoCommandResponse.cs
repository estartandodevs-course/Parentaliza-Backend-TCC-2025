namespace Parentaliza.Application.CasosDeUso.ControleLeiteMaternoCasoDeUso.Criar;

public class CriarControleLeiteMaternoCommandResponse
{
    public Guid Id { get; private set; }

    public CriarControleLeiteMaternoCommandResponse(Guid id)
    {
        Id = id;
    }
}

