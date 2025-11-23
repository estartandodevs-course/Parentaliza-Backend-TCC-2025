using MediatR;
using Parentaliza.Application.Mediator;
using Parentaliza.Domain.InterfacesRepository;
using System.Net;

namespace Parentaliza.Application.CasosDeUso.ConteudoCasoDeUso.Excluir;

public class ExcluirConteudoCommandHandler : IRequestHandler<ExcluirConteudoCommand, CommandResponse<Unit>>
{
    private readonly IConteudoRepository _conteudoRepository;
    public ExcluirConteudoCommandHandler(IConteudoRepository conteudoRepository)
    {
        _conteudoRepository = conteudoRepository;
    }

    public async Task<CommandResponse<Unit>> Handle(ExcluirConteudoCommand request, CancellationToken cancellationToken)
    {

        if (!request.Validar())
        {
            return CommandResponse<Unit>.ErroValidacao(request.ResultadoDasValidacoes);
        }

        var conteudoExiste = await _conteudoRepository.ObterPorId(request.Id);

        if (conteudoExiste == null)
        {
            return CommandResponse<Unit>.AdicionarErro("Conteudo não encontrado.", HttpStatusCode.NotFound);
        }

        await _conteudoRepository.Remover(conteudoExiste.Id);

        return CommandResponse<Unit>.Sucesso(null, HttpStatusCode.NoContent);
    }
}