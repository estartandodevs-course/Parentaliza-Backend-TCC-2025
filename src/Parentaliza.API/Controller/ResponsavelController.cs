using MediatR;
using Microsoft.AspNetCore.Mvc;
using Parentaliza.API.Controller.Base;

namespace Parentaliza.API.Controller;

[Route ("api/")]
public class ResponsavelController : BaseController
{
    private readonly IMediator _mediator;

    public ResponsavelController(IMediator mediator)
    {
        _mediator = mediator;
    }


}
