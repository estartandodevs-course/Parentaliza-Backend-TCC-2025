using MediatR;
using Microsoft.AspNetCore.Mvc;
using Parentaliza.API.Controller.Base;
using Parentaliza.API.Controller.Dtos;
using Parentaliza.Application.CasosDeUso.BebeGestacaoCasoDeUso.Criar;

namespace Parentaliza.API.Controller.EntidadesControllers;

[ApiController]
[Route("[controller]")]
public class BebeGestacaoController : BaseController
{
    private readonly IMediator _mediator;

    public BebeGestacaoController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> AdicionarInformacoesBebeGestacao(
            [FromBody] CriarBebeGestacaoDtos request)
    {
        var command = new CriarBebeGestacaoCommand(
            nome: request.Nome,
            dataPrevista: request.DataPrevista,
            diasDeGestacao: request.DiasDeGestacao,
            pesoEstimado: request.PesoEstimado,
            comprimentoEstimado: request.ComprimentoEstimado
            );

        var response = await _mediator.Send(command);

        return StatusCode((int)response.StatusCode, response);
    }
}