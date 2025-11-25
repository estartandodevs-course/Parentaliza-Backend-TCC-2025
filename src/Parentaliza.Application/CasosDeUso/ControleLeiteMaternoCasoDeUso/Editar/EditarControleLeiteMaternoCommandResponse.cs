namespace Parentaliza.Application.CasosDeUso.ControleLeiteMaternoCasoDeUso.Editar;

public class EditarControleLeiteMaternoCommandResponse
{
    public Guid Id { get; private set; }

    public EditarControleLeiteMaternoCommandResponse(Guid id)
    {
        Id = id;
    }
}

