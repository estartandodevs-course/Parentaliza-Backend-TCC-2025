namespace Parentaliza.Application.CasosDeUso.ExameSusCasoDeUso.Excluir;

public class ExcluirExameSusCommandResponse
{
    public Guid Id { get; private set; }

    public ExcluirExameSusCommandResponse(Guid id)
    {
        Id = id;
    }
}

