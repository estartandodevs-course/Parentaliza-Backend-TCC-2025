using MediatR;
using Microsoft.AspNetCore.Mvc;
using Parentaliza.API.Controller.Base;
using Parentaliza.API.Controller.Dtos;
using Parentaliza.Application.CasosDeUso.ConteudoCasoDeUso.Criar;

namespace Parentaliza.API.Controller.EntidadesControllers;

[ApiController]
[Route("[controller]")]
public class ConteudoController : BaseController
{
    private readonly IMediator _mediator;

    public ConteudoController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> AdicionaConteudo(
        [FromBody] CriarConteudoDtos request)
    {
        var command = new CriarConteudoCommand(
        titulo: request.Titulo,
        categoria: request.Categoria,
        dataPublicacao: request.DataPublicacao,
        descricao: request.Descricao
        );

        var response = await _mediator.Send(command);

        return Ok();
    }

    // Aqui é o post de conteudo multimidia, não será amazenado no banco de dados, apenas enviado para o serviço de armazenamento de mídia.
    /*
        [HttpPost]
        public async Task<IActionResult> AdicionaConteudoMultimidia(
            [FromBody] CriarConteudoMultimidiaDtos
            request)
        {
            // Implementar a lógica para adicionar conteúdo multimídia  
            var command = new CriarConteudoMultimidiaCommand(
            conteudoId: request.ConteudoId,
            tipoMultimidia: request.TipoMultimidia,
            url: request.Url
            );
            var response = await _mediator.Send(command);
            return Ok();
        }
    */
}