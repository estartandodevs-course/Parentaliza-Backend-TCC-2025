using MediatR;
using Parentaliza.Application.Mediator;

namespace Parentaliza.Application.CasosDeUso.ResponsavelCasoDeUso.Excluir;

public class ExcluirResponsavelCommand : IRequest<CommandResponse<ExcluirResponsavelCommandResponse>>
{
    public Guid Id { get; private set; }

    public ExcluirResponsavelCommand(Guid id)
    {
        Id = id;
    }
}

