using MediatR;
using Parentaliza.Application.Mediator;
using Parentaliza.Domain.InterfacesRepository;
using System.Net;

namespace Parentaliza.Application.CasosDeUso.ConteudoCasoDeUso.Listar;

public class ListarConteudoCommandHandler : IRequestHandler<ListarConteudoCommand, CommandResponse<List<ListarConteudoCommandResponse>>>
{
    private readonly IConteudoRepository _conteudoRepository;

    public ListarConteudoCommandHandler(IConteudoRepository conteudoRepository)
    {
        _conteudoRepository = conteudoRepository;
    }

    public async Task<CommandResponse<List<ListarConteudoCommandResponse>>> Handle(ListarConteudoCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var todosConteudos = await _conteudoRepository.ObterTodos();
            var response = todosConteudos.Select(conteudo => new ListarConteudoCommandResponse(
                conteudo.Id,
                conteudo.Titulo,
                conteudo.Categoria,
                conteudo.DataPublicacao,
                conteudo.Descricao
            )).ToList();
            return CommandResponse<List<ListarConteudoCommandResponse>>.Sucesso(response, HttpStatusCode.OK);
        }
        catch (Exception ex)
        {
            return CommandResponse<List<ListarConteudoCommandResponse>>.ErroCritico(mensagem: $"Ocorreu um erro ao listar os eventos da agenda: {ex.Message}");
        }
    }
}