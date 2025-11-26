using MediatR;
using Microsoft.Extensions.Logging;
using Parentaliza.Application.Mediator;
using Parentaliza.Domain.Entidades;
using Parentaliza.Domain.InterfacesRepository;
using System.Net;
using System.Reflection;

namespace Parentaliza.Application.CasosDeUso.ControleFraldaCasoDeUso.Editar;

public class EditarControleFraldaCommandHandler : IRequestHandler<EditarControleFraldaCommand, CommandResponse<EditarControleFraldaCommandResponse>>
{
    private readonly IControleFraldaRepository _controleFraldaRepository;
    private readonly IBebeNascidoRepository _bebeNascidoRepository;
    private readonly ILogger<EditarControleFraldaCommandHandler> _logger;

    public EditarControleFraldaCommandHandler(
        IControleFraldaRepository controleFraldaRepository,
        IBebeNascidoRepository bebeNascidoRepository,
        ILogger<EditarControleFraldaCommandHandler> logger)
    {
        _controleFraldaRepository = controleFraldaRepository;
        _bebeNascidoRepository = bebeNascidoRepository;
        _logger = logger;
    }

    public async Task<CommandResponse<EditarControleFraldaCommandResponse>> Handle(EditarControleFraldaCommand request, CancellationToken cancellationToken)
    {
        if (!request.Validar())
        {
            return CommandResponse<EditarControleFraldaCommandResponse>.ErroValidacao(request.ResultadoDasValidacoes);
        }

        try
        {
            var controleFralda = await _controleFraldaRepository.ObterPorId(request.Id);

            if (controleFralda == null)
            {
                return CommandResponse<EditarControleFraldaCommandResponse>.AdicionarErro(
                    statusCode: HttpStatusCode.NotFound,
                    mensagem: "Controle de fralda não encontrado.");
            }

            var bebe = await _bebeNascidoRepository.ObterPorId(request.BebeNascidoId);
            if (bebe == null)
            {
                return CommandResponse<EditarControleFraldaCommandResponse>.AdicionarErro(
                    statusCode: HttpStatusCode.NotFound,
                    mensagem: "Bebê não encontrado.");
            }

            // Atualizar a entidade existente usando reflection para acessar propriedades privadas
            var tipo = typeof(ControleFralda);
            tipo.GetProperty(nameof(ControleFralda.BebeNascidoId))?.SetValue(controleFralda, request.BebeNascidoId);
            tipo.GetProperty(nameof(ControleFralda.HoraTroca))?.SetValue(controleFralda, request.HoraTroca);
            tipo.GetProperty(nameof(ControleFralda.TipoFralda))?.SetValue(controleFralda, request.TipoFralda);
            tipo.GetProperty(nameof(ControleFralda.Observacoes))?.SetValue(controleFralda, request.Observacoes);

            await _controleFraldaRepository.Atualizar(controleFralda);

            var response = new EditarControleFraldaCommandResponse(controleFralda.Id);

            return CommandResponse<EditarControleFraldaCommandResponse>.Sucesso(response, HttpStatusCode.OK);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao editar controle de fralda");
            return CommandResponse<EditarControleFraldaCommandResponse>.ErroCritico(
                mensagem: $"Ocorreu um erro ao editar o controle de fralda: {ex.Message}");
        }
    }
}