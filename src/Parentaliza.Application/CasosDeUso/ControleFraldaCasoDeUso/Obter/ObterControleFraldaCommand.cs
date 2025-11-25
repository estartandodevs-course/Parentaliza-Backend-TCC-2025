using MediatR;
using Parentaliza.Application.Mediator;

namespace Parentaliza.Application.CasosDeUso.ControleFraldaCasoDeUso.Obter;

public class ObterControleFraldaCommand : IRequest<CommandResponse<ObterControleFraldaCommandResponse>>
{
    public Guid Id { get; private set; }

    public ObterControleFraldaCommand(Guid id)
    {
        Id = id;
    }
}

