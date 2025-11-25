using MediatR;
using Parentaliza.Application.Mediator;

namespace Parentaliza.Application.CasosDeUso.ControleLeiteMaternoCasoDeUso.Obter;

public class ObterControleLeiteMaternoCommand : IRequest<CommandResponse<ObterControleLeiteMaternoCommandResponse>>
{
    public Guid Id { get; private set; }

    public ObterControleLeiteMaternoCommand(Guid id)
    {
        Id = id;
    }
}

