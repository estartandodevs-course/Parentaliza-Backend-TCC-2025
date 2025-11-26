using MediatR;
using Parentaliza.Application.Mediator;

namespace Parentaliza.Application.CasosDeUso.VacinaSusCasoDeUso.Excluir;

public class ExcluirVacinaSusCommand : IRequest<CommandResponse<ExcluirVacinaSusCommandResponse>>
{
    public Guid Id { get; private set; }

    public ExcluirVacinaSusCommand(Guid id)
    {
        Id = id;
    }
}