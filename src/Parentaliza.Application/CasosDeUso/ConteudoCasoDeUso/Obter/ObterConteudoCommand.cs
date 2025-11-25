using MediatR;
using Parentaliza.Application.Mediator;

namespace Parentaliza.Application.CasosDeUso.ConteudoCasoDeUso.Obter;

public class ObterConteudoCommand : IRequest<CommandResponse<ObterConteudoCommandResponse>>
{
    public Guid Id { get; private set; }

    public ObterConteudoCommand(Guid id)
    {
        Id = id;
    }
}
