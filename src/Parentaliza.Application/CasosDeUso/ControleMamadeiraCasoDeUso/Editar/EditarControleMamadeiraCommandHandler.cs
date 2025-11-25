using MediatR;
using Microsoft.Extensions.Logging;
using Parentaliza.Application.Mediator;
using Parentaliza.Domain.Entidades;
using Parentaliza.Domain.InterfacesRepository;
using System.Net;
using System.Reflection;

namespace Parentaliza.Application.CasosDeUso.ControleMamadeiraCasoDeUso.Editar;

public class EditarControleMamadeiraCommandHandler : IRequestHandler<EditarControleMamadeiraCommand, CommandResponse<EditarControleMamadeiraCommandResponse>>
{
    private readonly IControleMamadeiraRepository _controleMamadeiraRepository;
    private readonly IBebeNascidoRepository _bebeNascidoRepository;
    private readonly ILogger<EditarControleMamadeiraCommandHandler> _logger;

    public EditarControleMamadeiraCommandHandler(
        IControleMamadeiraRepository controleMamadeiraRepository,
        IBebeNascidoRepository bebeNascidoRepository,
        ILogger<EditarControleMamadeiraCommandHandler> logger)
    {
        _controleMamadeiraRepository = controleMamadeiraRepository;
        _bebeNascidoRepository = bebeNascidoRepository;
        _logger = logger;
    }

    public async Task<CommandResponse<EditarControleMamadeiraCommandResponse>> Handle(EditarControleMamadeiraCommand request, CancellationToken cancellationToken)
    {
        if (!request.Validar())
        {
            return CommandResponse<EditarControleMamadeiraCommandResponse>.ErroValidacao(request.ResultadoDasValidacoes);
        }

        try
        {
            var controleMamadeira = await _controleMamadeiraRepository.ObterPorId(request.Id);

            if (controleMamadeira == null)
            {
                return CommandResponse<EditarControleMamadeiraCommandResponse>.AdicionarErro(
                    statusCode: HttpStatusCode.NotFound,
                    mensagem: "Controle de mamadeira não encontrado.");
            }

            var bebe = await _bebeNascidoRepository.ObterPorId(request.BebeNascidoId);
            if (bebe == null)
            {
                return CommandResponse<EditarControleMamadeiraCommandResponse>.AdicionarErro(
                    statusCode: HttpStatusCode.NotFound,
                    mensagem: "Bebê não encontrado.");
            }

            // Atualizar a entidade existente usando reflection para acessar propriedades privadas
            var tipo = typeof(ControleMamadeira);
            tipo.GetProperty(nameof(ControleMamadeira.BebeNascidoId))?.SetValue(controleMamadeira, request.BebeNascidoId);
            tipo.GetProperty(nameof(ControleMamadeira.Data))?.SetValue(controleMamadeira, request.Data);
            tipo.GetProperty(nameof(ControleMamadeira.Hora))?.SetValue(controleMamadeira, request.Hora);
            tipo.GetProperty(nameof(ControleMamadeira.QuantidadeLeite))?.SetValue(controleMamadeira, request.QuantidadeLeite);
            tipo.GetProperty(nameof(ControleMamadeira.Anotacao))?.SetValue(controleMamadeira, request.Anotacao);

            await _controleMamadeiraRepository.Atualizar(controleMamadeira);

            var response = new EditarControleMamadeiraCommandResponse(controleMamadeira.Id);

            return CommandResponse<EditarControleMamadeiraCommandResponse>.Sucesso(response, HttpStatusCode.OK);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao editar controle de mamadeira");
            return CommandResponse<EditarControleMamadeiraCommandResponse>.ErroCritico(
                mensagem: $"Ocorreu um erro ao editar o controle de mamadeira: {ex.Message}");
        }
    }
}

