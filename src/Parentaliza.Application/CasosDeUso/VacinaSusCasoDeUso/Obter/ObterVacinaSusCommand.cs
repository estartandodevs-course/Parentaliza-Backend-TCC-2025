using MediatR;
using Parentaliza.Application.Mediator;

namespace Parentaliza.Application.CasosDeUso.VacinaSusCasoDeUso.Obter;

public class ObterVacinaSusCommand : IRequest<CommandResponse<ObterVacinaSusCommandResponse>>
{
    public Guid Id { get; private set; }

    public ObterVacinaSusCommand(Guid id)
    {
        Id = id;
    }
}