using MediatR;
using Microsoft.AspNetCore.Mvc;
using Parentaliza.API.Controller.Base;
using Parentaliza.API.Controller.Dtos;
using Parentaliza.Application.CasosDeUso.BebeNascidoCasoDeUso.Criar;


namespace Parentaliza.API.Controller.EntidadesControllers;

[ApiController]
[Route("[controller]")]
public class BebeNascidoController : BaseController
{
    private readonly IMediator _mediator; // Mediator faz a mediação entre o controlador e os casos de uso da aplicação

    public BebeNascidoController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> AdicionarInformacoesBebeNascido(
            [FromBody] CriarBebeNascidoDtos request)
    {
        var command = new CriarBebeNascidoCommand(
            nome: request.Nome,
            dataNascimento: request.DataNascimento,
            sexo: request.Sexo,
            tipoSanguineo: request.TipoSanguineo,
            idadeMeses: request.IdadeMeses,
            peso: request.Peso,
            altura: request.Altura
            );

        var response = await _mediator.Send(command);

        return StatusCode((int)response.StatusCode, response);
    }
}
