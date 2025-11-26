using MediatR;
using Parentaliza.Application.Mediator;

namespace Parentaliza.Application.CasosDeUso.ExameSusCasoDeUso.Excluir;

public class ExcluirExameSusCommand : IRequest<CommandResponse<ExcluirExameSusCommandResponse>>
{
    public Guid Id { get; private set; }

    public ExcluirExameSusCommand(Guid id)
    {
        Id = id;
    }
}