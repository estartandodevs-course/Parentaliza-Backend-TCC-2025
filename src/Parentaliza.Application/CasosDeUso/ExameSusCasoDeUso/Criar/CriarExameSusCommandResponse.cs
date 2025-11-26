namespace Parentaliza.Application.CasosDeUso.ExameSusCasoDeUso.Criar;

public class CriarExameSusCommandResponse
{
    public Guid Id { get; private set; }

    public CriarExameSusCommandResponse(Guid id)
    {
        Id = id;
    }
}