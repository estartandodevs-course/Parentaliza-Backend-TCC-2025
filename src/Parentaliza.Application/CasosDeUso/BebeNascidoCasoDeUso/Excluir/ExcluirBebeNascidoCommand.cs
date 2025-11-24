using MediatR;
using Parentaliza.Application.Mediator;

namespace Parentaliza.Application.CasosDeUso.BebeNascidoCasoDeUso.Excluir;

public class ExcluirBebeNascidoCommand : IRequest<CommandResponse<ExcluirBebeNascidoCommandResponse>>
{
    public Guid Id { get; private set; }

    public ExcluirBebeNascidoCommand(Guid id)
    {
        Id = id;
    }
}
