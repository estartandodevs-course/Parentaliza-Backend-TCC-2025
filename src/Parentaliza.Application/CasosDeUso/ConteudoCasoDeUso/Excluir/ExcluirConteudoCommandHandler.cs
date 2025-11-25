using MediatR;
using Microsoft.Extensions.Logging;
using Parentaliza.Application.Mediator;
using Parentaliza.Domain.InterfacesRepository;
using System.Net;

namespace Parentaliza.Application.CasosDeUso.ConteudoCasoDeUso.Excluir;

public class ExcluirConteudoCommandHandler : IRequestHandler<ExcluirConteudoCommand, CommandResponse<Unit>>
{
    private readonly IConteudoRepository _conteudoRepository;
    private readonly ILogger<ExcluirConteudoCommandHandler> _logger;

    public ExcluirConteudoCommandHandler(
        IConteudoRepository conteudoRepository,
        ILogger<ExcluirConteudoCommandHandler> logger)
    {
        _conteudoRepository = conteudoRepository;
        _logger = logger;
    }

    public async Task<CommandResponse<Unit>> Handle(ExcluirConteudoCommand request, CancellationToken cancellationToken)
    {
        if (!request.Validar())
        {
            return CommandResponse<Unit>.ErroValidacao(request.ResultadoDasValidacoes);
        }

        try
        {
            var conteudoExiste = await _conteudoRepository.ObterPorId(request.Id);

            if (conteudoExiste == null)
            {
                return CommandResponse<Unit>.AdicionarErro("Conteudo não encontrado.", HttpStatusCode.NotFound);
            }

            await _conteudoRepository.Remover(conteudoExiste.Id);

            return CommandResponse<Unit>.Sucesso(null, HttpStatusCode.NoContent);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao excluir conteúdo");
            return CommandResponse<Unit>.ErroCritico(
                mensagem: $"Ocorreu um erro ao excluir o conteúdo: {ex.Message}");
        }
    }
}