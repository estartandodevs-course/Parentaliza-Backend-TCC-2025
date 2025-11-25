using MediatR;
using Parentaliza.Application.Mediator;

namespace Parentaliza.Application.CasosDeUso.BebeGestacaoCasoDeUso.Excluir;

public class ExcluirBebeGestacaoCommand : IRequest<CommandResponse<ExcluirBebeGestacaoCommandResponse>>
{
    public Guid Id { get; private set; }

    public ExcluirBebeGestacaoCommand(Guid id)
    {
        Id = id;
    }
}
