namespace Parentaliza.Application.CasosDeUso.ExameSusCasoDeUso.Editar;

public class EditarExameSusCommandResponse
{
    public Guid Id { get; private set; }

    public EditarExameSusCommandResponse(Guid id)
    {
        Id = id;
    }
}

