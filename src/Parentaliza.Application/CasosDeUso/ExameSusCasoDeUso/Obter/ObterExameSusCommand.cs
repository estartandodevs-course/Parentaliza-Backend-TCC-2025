using MediatR;
using Parentaliza.Application.Mediator;

namespace Parentaliza.Application.CasosDeUso.ExameSusCasoDeUso.Obter;

public class ObterExameSusCommand : IRequest<CommandResponse<ObterExameSusCommandResponse>>
{
    public Guid Id { get; private set; }

    public ObterExameSusCommand(Guid id)
    {
        Id = id;
    }
}