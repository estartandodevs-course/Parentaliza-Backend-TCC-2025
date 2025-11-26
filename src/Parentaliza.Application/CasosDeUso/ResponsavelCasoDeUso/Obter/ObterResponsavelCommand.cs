using MediatR;
using Parentaliza.Application.Mediator;

namespace Parentaliza.Application.CasosDeUso.ResponsavelCasoDeUso.Obter;

public class ObterResponsavelCommand : IRequest<CommandResponse<ObterResponsavelCommandResponse>>
{
    public Guid Id { get; private set; }

    public ObterResponsavelCommand(Guid id)
    {
        Id = id;
    }
}