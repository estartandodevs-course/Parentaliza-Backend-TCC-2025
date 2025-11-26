using MediatR;
using Microsoft.Extensions.Logging;
using Parentaliza.Application.Mediator;
using Parentaliza.Domain.Entidades;
using Parentaliza.Domain.InterfacesRepository;
using System.Net;

namespace Parentaliza.Application.CasosDeUso.ConteudoCasoDeUso.Editar;

public class EditarConteudoCommandHandler : IRequestHandler<EditarConteudoCommand, CommandResponse<EditarConteudoCommandResponse>>
{
    private readonly IConteudoRepository _conteudoRepository;
    private readonly ILogger<EditarConteudoCommandHandler> _logger;

    public EditarConteudoCommandHandler(
        IConteudoRepository conteudoRepository,
        ILogger<EditarConteudoCommandHandler> logger)
    {
        _conteudoRepository = conteudoRepository;
        _logger = logger;
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
            var tipo = typeof(Conteudo);
            tipo.GetProperty(nameof(Conteudo.Titulo))?.SetValue(conteudo, request.Titulo);
            tipo.GetProperty(nameof(Conteudo.Categoria))?.SetValue(conteudo, request.Categoria);
            tipo.GetProperty(nameof(Conteudo.DataPublicacao))?.SetValue(conteudo, request.DataPublicacao);
            tipo.GetProperty(nameof(Conteudo.Descricao))?.SetValue(conteudo, request.Descricao);

            await _conteudoRepository.Atualizar(conteudo);

            var response = new EditarConteudoCommandResponse(conteudo.Id);

            return CommandResponse<EditarConteudoCommandResponse>.Sucesso(response, HttpStatusCode.OK);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao editar conteúdo");
            return CommandResponse<EditarConteudoCommandResponse>.ErroCritico(
                mensagem: $"Ocorreu um erro ao editar o conteudo: {ex.Message}");
        }
    }
}