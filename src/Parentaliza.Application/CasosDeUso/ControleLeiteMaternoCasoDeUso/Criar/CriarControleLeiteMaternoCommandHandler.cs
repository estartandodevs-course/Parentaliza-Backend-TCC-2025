using MediatR;
using Microsoft.Extensions.Logging;
using Parentaliza.Application.Mediator;
using Parentaliza.Domain.Entidades;
using Parentaliza.Domain.InterfacesRepository;
using System.Net;

namespace Parentaliza.Application.CasosDeUso.ControleLeiteMaternoCasoDeUso.Criar;

public class CriarControleLeiteMaternoCommandHandler : IRequestHandler<CriarControleLeiteMaternoCommand, CommandResponse<CriarControleLeiteMaternoCommandResponse>>
{
    private readonly IControleLeiteMaternoRepository _controleLeiteMaternoRepository;
    private readonly IBebeNascidoRepository _bebeNascidoRepository;
    private readonly ILogger<CriarControleLeiteMaternoCommandHandler> _logger;

    public CriarControleLeiteMaternoCommandHandler(
        IControleLeiteMaternoRepository controleLeiteMaternoRepository,
        IBebeNascidoRepository bebeNascidoRepository,
        ILogger<CriarControleLeiteMaternoCommandHandler> logger)
    {
        _controleLeiteMaternoRepository = controleLeiteMaternoRepository;
        _bebeNascidoRepository = bebeNascidoRepository;
        _logger = logger;
    }

    public async Task<CommandResponse<CriarControleLeiteMaternoCommandResponse>> Handle(CriarControleLeiteMaternoCommand request, CancellationToken cancellationToken)
    {
        if (!request.Validar())
        {
            return CommandResponse<CriarControleLeiteMaternoCommandResponse>.ErroValidacao(request.ResultadoDasValidacoes);
        }

        try
        {
            var bebe = await _bebeNascidoRepository.ObterPorId(request.BebeNascidoId);
            if (bebe == null)
            {
                return CommandResponse<CriarControleLeiteMaternoCommandResponse>.AdicionarErro(
                    statusCode: HttpStatusCode.NotFound,
                    mensagem: "Bebê não encontrado.");
            }

            var controleLeiteMaterno = new ControleLeiteMaterno(
                bebeNascidoId: request.BebeNascidoId,
                cronometro: request.Cronometro,
                ladoDireito: request.LadoDireito,
                ladoEsquerdo: request.LadoEsquerdo
            );

            await _controleLeiteMaternoRepository.Adicionar(controleLeiteMaterno);

            var response = new CriarControleLeiteMaternoCommandResponse(controleLeiteMaterno.Id);

            return CommandResponse<CriarControleLeiteMaternoCommandResponse>.Sucesso(response, HttpStatusCode.Created);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao criar controle de leite materno");
            return CommandResponse<CriarControleLeiteMaternoCommandResponse>.ErroCritico(
                mensagem: $"Ocorreu um erro ao criar o controle de leite materno: {ex.Message}");
        }
    }
}

