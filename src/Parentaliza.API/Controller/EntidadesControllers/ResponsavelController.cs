using MediatR;

namespace Parentaliza.API.Controller.EntidadesControllers;

public class ResponsavelController
{
    private readonly IMediator _mediator;

    public ResponsavelController(IMediator mediator)
    {
        _mediator = mediator;
    }
}