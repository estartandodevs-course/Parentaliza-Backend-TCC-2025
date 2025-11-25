using MediatR;
using Parentaliza.Application.Mediator;

namespace Parentaliza.Application.CasosDeUso.BebeGestacaoCasoDeUso.Obter;

public class ObterBebeGestacaoCommand : IRequest<CommandResponse<ObterBebeGestacaoCommandResponse>>
{
    public Guid Id { get; private set; }

    public ObterBebeGestacaoCommand(Guid id)
    {
        Id = id;
    }
}
