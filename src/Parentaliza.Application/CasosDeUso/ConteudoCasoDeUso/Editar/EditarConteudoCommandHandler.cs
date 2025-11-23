using MediatR;
using Parentaliza.Application.Mediator;
using Parentaliza.Domain.Entidades;
using Parentaliza.Domain.InterfacesRepository;
using System.Net;

namespace Parentaliza.Application.CasosDeUso.ConteudoCasoDeUso.Editar;

public class EditarConteudoCommandHandler : IRequestHandler<EditarConteudoCommand, CommandResponse<EditarConteudoCommandResponse>>
{
    private readonly IConteudoRepository _conteudoRepository;

    public EditarConteudoCommandHandler(IConteudoRepository conteudoRepository)
    {
        _conteudoRepository = conteudoRepository;
    }

    public async Task<CommandResponse<EditarConteudoCommandResponse>> Handle(EditarConteudoCommand request, CancellationToken cancellationToken)
    {
        if (!request.Validar())
        {
            return CommandResponse<EditarConteudoCommandResponse>.ErroValidacao(request.ResultadoDasValidacoes);
        }

        try
        {
            var conteudo = await _conteudoRepository.ObterPorId(request.Id);

            if (conteudo == null)
            {
                return CommandResponse<EditarConteudoCommandResponse>.AdicionarErro(
                    statusCode: HttpStatusCode.NotFound,
                    mensagem: "Conteudo não encontrado.");
            }

            if (conteudo.Titulo?.ToLower() != request.Titulo?.ToLower())
            {
                var nomeJaUtilizado = await _conteudoRepository.NomeJaUtilizado(request.Titulo);
                if (nomeJaUtilizado)
                {
                    return CommandResponse<EditarConteudoCommandResponse>.AdicionarErro(
                        statusCode: HttpStatusCode.Conflict,
                        mensagem: "O nome do conteudo já está em uso.");
                }
            }

            var conteudoAtualizado = new Conteudo(
                request.Titulo,
                request.Categoria,
                request.DataPublicacao,
                request.Descricao
            );

            conteudoAtualizado.Id = request.Id;

            await _conteudoRepository.Atualizar(conteudoAtualizado);

            var response = new EditarConteudoCommandResponse(conteudoAtualizado.Id);

            return CommandResponse<EditarConteudoCommandResponse>.Sucesso(response, HttpStatusCode.OK);
        }
        catch (Exception ex)
        {
            return CommandResponse<EditarConteudoCommandResponse>.ErroCritico(
                mensagem: $"Ocorreu um erro ao editar o conteudo: {ex.Message}");
        }
    }
}