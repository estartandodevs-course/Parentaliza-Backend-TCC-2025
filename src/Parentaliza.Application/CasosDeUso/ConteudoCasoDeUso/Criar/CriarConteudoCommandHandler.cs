using MediatR;
using Microsoft.Extensions.Logging;
using Parentaliza.Application.Mediator;
using Parentaliza.Domain.Entidades;
using Parentaliza.Domain.InterfacesRepository;
using System.Net;

namespace Parentaliza.Application.CasosDeUso.ConteudoCasoDeUso.Criar;

public class CriarConteudoCommandHandler : IRequestHandler<CriarConteudoCommand, CommandResponse<CriarConteudoCommandResponse>>
{
    private readonly IConteudoRepository _conteudoRepository;
    private readonly ILogger<CriarConteudoCommandHandler> _logger;

    public CriarConteudoCommandHandler(
        IConteudoRepository conteudoRepository,
        ILogger<CriarConteudoCommandHandler> logger)
    {
        _conteudoRepository = conteudoRepository;
        _logger = logger;
    }
    public async Task<CommandResponse<CriarConteudoCommandResponse>> Handle(CriarConteudoCommand request, CancellationToken cancellationToken)
    {
        if (!request.Validar())
        {
            return CommandResponse<CriarConteudoCommandResponse>.ErroValidacao(request.ResultadoDasValidacoes);
        }

        try
        {
            var nomeJaUtilizado = await _conteudoRepository.NomeJaUtilizado(request.Titulo);

            if (nomeJaUtilizado)
            {
                return CommandResponse<CriarConteudoCommandResponse>.AdicionarErro(statusCode: HttpStatusCode.Conflict, mensagem: "O titulo do conteudo já está em uso.");
            }

            var conteudo = new Conteudo(request.Titulo, request.Categoria, request.DataPublicacao, request.Descricao);

            await _conteudoRepository.Adicionar(conteudo);

            var response = new CriarConteudoCommandResponse(conteudo.Id);


            return CommandResponse<CriarConteudoCommandResponse>.Sucesso(response, statusCode: HttpStatusCode.Created);

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao criar conteúdo");
            return CommandResponse<CriarConteudoCommandResponse>.ErroCritico(mensagem: $"Ocorreu um erro ao criar o conteudo: {ex.Message}");
        }

    }
}