using MediatR;
using Microsoft.Extensions.Logging;
using Parentaliza.Application.Mediator;
using Parentaliza.Domain.InterfacesRepository;
using System.Net;

namespace Parentaliza.Application.CasosDeUso.ConteudoCasoDeUso.Obter;

public class ObterConteudoCommandHandler : IRequestHandler<ObterConteudoCommand, CommandResponse<ObterConteudoCommandResponse>>
{
    private readonly IConteudoRepository _conteudoRepository;
    private readonly ILogger<ObterConteudoCommandHandler> _logger;

    public ObterConteudoCommandHandler(
        IConteudoRepository conteudoRepository,
        ILogger<ObterConteudoCommandHandler> logger)
    {
        _conteudoRepository = conteudoRepository;
        _logger = logger;
    }

    public async Task<CommandResponse<ObterConteudoCommandResponse>> Handle(ObterConteudoCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var conteudo = await _conteudoRepository.ObterPorId(request.Id);

            if (conteudo == null)
            {
                return CommandResponse<ObterConteudoCommandResponse>.AdicionarErro(
                    statusCode: HttpStatusCode.NotFound,
                    mensagem: "Conteúdo não encontrado.");
            }

            var response = new ObterConteudoCommandResponse(
                conteudo.Id,
                conteudo.Titulo,
                conteudo.Categoria,
                conteudo.DataPublicacao,
                conteudo.Descricao
            );

            return CommandResponse<ObterConteudoCommandResponse>.Sucesso(response, HttpStatusCode.OK);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao obter conteúdo");
            return CommandResponse<ObterConteudoCommandResponse>.ErroCritico(
                mensagem: $"Ocorreu um erro ao obter o conteúdo: {ex.Message}");
        }
    }
}