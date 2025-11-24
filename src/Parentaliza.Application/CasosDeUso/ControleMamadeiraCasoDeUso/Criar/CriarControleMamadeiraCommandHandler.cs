using MediatR;
using Microsoft.Extensions.Logging;
using Parentaliza.Application.Mediator;
using Parentaliza.Domain.Entidades;
using Parentaliza.Domain.InterfacesRepository;
using System.Net;

namespace Parentaliza.Application.CasosDeUso.ControleMamadeiraCasoDeUso.Criar;

public class CriarControleMamadeiraCommandHandler : IRequestHandler<CriarControleMamadeiraCommand, CommandResponse<CriarControleMamadeiraCommandResponse>>
{
    private readonly IControleMamadeiraRepository _controleMamadeiraRepository;
    private readonly IBebeNascidoRepository _bebeNascidoRepository;
    private readonly ILogger<CriarControleMamadeiraCommandHandler> _logger;

    public CriarControleMamadeiraCommandHandler(
        IControleMamadeiraRepository controleMamadeiraRepository,
        IBebeNascidoRepository bebeNascidoRepository,
        ILogger<CriarControleMamadeiraCommandHandler> logger)
    {
        _controleMamadeiraRepository = controleMamadeiraRepository;
        _bebeNascidoRepository = bebeNascidoRepository;
        _logger = logger;
    }

    public async Task<CommandResponse<CriarControleMamadeiraCommandResponse>> Handle(CriarControleMamadeiraCommand request, CancellationToken cancellationToken)
    {
        if (!request.Validar())
        {
            return CommandResponse<CriarControleMamadeiraCommandResponse>.ErroValidacao(request.ResultadoDasValidacoes);
        }

        try
        {
            var bebe = await _bebeNascidoRepository.ObterPorId(request.BebeNascidoId);
            if (bebe == null)
            {
                return CommandResponse<CriarControleMamadeiraCommandResponse>.AdicionarErro(
                    statusCode: HttpStatusCode.NotFound,
                    mensagem: "Bebê não encontrado.");
            }

            var controleMamadeira = new ControleMamadeira(
                bebeNascidoId: request.BebeNascidoId,
                data: request.Data,
                hora: request.Hora,
                quantidadeLeite: request.QuantidadeLeite,
                anotacao: request.Anotacao
            );

            await _controleMamadeiraRepository.Adicionar(controleMamadeira);

            var response = new CriarControleMamadeiraCommandResponse(controleMamadeira.Id);

            return CommandResponse<CriarControleMamadeiraCommandResponse>.Sucesso(response, HttpStatusCode.Created);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao criar controle de mamadeira");
            return CommandResponse<CriarControleMamadeiraCommandResponse>.ErroCritico(
                mensagem: $"Ocorreu um erro ao criar o controle de mamadeira: {ex.Message}");
        }
    }
}

