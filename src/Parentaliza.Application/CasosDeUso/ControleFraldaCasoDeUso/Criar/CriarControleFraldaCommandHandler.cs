using MediatR;
using Microsoft.Extensions.Logging;
using Parentaliza.Application.Mediator;
using Parentaliza.Domain.Entidades;
using Parentaliza.Domain.InterfacesRepository;
using System.Net;

namespace Parentaliza.Application.CasosDeUso.ControleFraldaCasoDeUso.Criar;

public class CriarControleFraldaCommandHandler : IRequestHandler<CriarControleFraldaCommand, CommandResponse<CriarControleFraldaCommandResponse>>
{
    private readonly IControleFraldaRepository _controleFraldaRepository;
    private readonly IBebeNascidoRepository _bebeNascidoRepository;
    private readonly ILogger<CriarControleFraldaCommandHandler> _logger;

    public CriarControleFraldaCommandHandler(
        IControleFraldaRepository controleFraldaRepository,
        IBebeNascidoRepository bebeNascidoRepository,
        ILogger<CriarControleFraldaCommandHandler> logger)
    {
        _controleFraldaRepository = controleFraldaRepository;
        _bebeNascidoRepository = bebeNascidoRepository;
        _logger = logger;
    }

    public async Task<CommandResponse<CriarControleFraldaCommandResponse>> Handle(CriarControleFraldaCommand request, CancellationToken cancellationToken)
    {
        if (!request.Validar())
        {
            return CommandResponse<CriarControleFraldaCommandResponse>.ErroValidacao(request.ResultadoDasValidacoes);
        }

        try
        {
            var bebe = await _bebeNascidoRepository.ObterPorId(request.BebeNascidoId);
            if (bebe == null)
            {
                return CommandResponse<CriarControleFraldaCommandResponse>.AdicionarErro(
                    statusCode: HttpStatusCode.NotFound,
                    mensagem: "Bebê não encontrado.");
            }

            var controleFralda = new ControleFralda(
                bebeNascidoId: request.BebeNascidoId,
                horaTroca: request.HoraTroca,
                tipoFralda: request.TipoFralda,
                observacoes: request.Observacoes
            );

            await _controleFraldaRepository.Adicionar(controleFralda);

            var response = new CriarControleFraldaCommandResponse(controleFralda.Id);

            return CommandResponse<CriarControleFraldaCommandResponse>.Sucesso(response, HttpStatusCode.Created);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao criar controle de fralda");
            return CommandResponse<CriarControleFraldaCommandResponse>.ErroCritico(
                mensagem: $"Ocorreu um erro ao criar o controle de fralda: {ex.Message}");
        }
    }
}