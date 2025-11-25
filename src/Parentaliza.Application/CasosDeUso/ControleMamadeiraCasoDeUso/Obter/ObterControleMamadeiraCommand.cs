using MediatR;
using Parentaliza.Application.Mediator;

namespace Parentaliza.Application.CasosDeUso.ControleMamadeiraCasoDeUso.Obter;

public class ObterControleMamadeiraCommand : IRequest<CommandResponse<ObterControleMamadeiraCommandResponse>>
{
    public Guid Id { get; private set; }

    public ObterControleMamadeiraCommand(Guid id)
    {
        Id = id;
    }
}

