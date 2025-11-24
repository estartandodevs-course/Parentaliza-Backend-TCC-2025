using MediatR;
using Microsoft.Extensions.Logging;
using Parentaliza.Application.Mediator;
using Parentaliza.Domain.Entidades;
using Parentaliza.Domain.InterfacesRepository;
using System.Net;

namespace Parentaliza.Application.CasosDeUso.VacinaAplicadaCasoDeUso.MarcarAplicada;

public class MarcarVacinaAplicadaCommandHandler : IRequestHandler<MarcarVacinaAplicadaCommand, CommandResponse<MarcarVacinaAplicadaCommandResponse>>
{
    private readonly IVacinaAplicadaRepository _vacinaAplicadaRepository;
    private readonly IBebeNascidoRepository _bebeNascidoRepository;
    private readonly IVacinaSusRepository _vacinaSusRepository;
    private readonly ILogger<MarcarVacinaAplicadaCommandHandler> _logger;

    public MarcarVacinaAplicadaCommandHandler(
        IVacinaAplicadaRepository vacinaAplicadaRepository,
        IBebeNascidoRepository bebeNascidoRepository,
        IVacinaSusRepository vacinaSusRepository,
        ILogger<MarcarVacinaAplicadaCommandHandler> logger)
    {
        _vacinaAplicadaRepository = vacinaAplicadaRepository;
        _bebeNascidoRepository = bebeNascidoRepository;
        _vacinaSusRepository = vacinaSusRepository;
        _logger = logger;
    }

    public async Task<CommandResponse<MarcarVacinaAplicadaCommandResponse>> Handle(MarcarVacinaAplicadaCommand request, CancellationToken cancellationToken)
    {
        if (!request.Validar())
        {
            return CommandResponse<MarcarVacinaAplicadaCommandResponse>.ErroValidacao(request.ResultadoDasValidacoes);
        }

        try
        {
            var bebe = await _bebeNascidoRepository.ObterPorId(request.BebeNascidoId);
            if (bebe == null)
            {
                return CommandResponse<MarcarVacinaAplicadaCommandResponse>.AdicionarErro(
                    statusCode: HttpStatusCode.NotFound,
                    mensagem: "Bebê não encontrado.");
            }

            var vacinaSus = await _vacinaSusRepository.ObterPorId(request.VacinaSusId);
            if (vacinaSus == null)
            {
                return CommandResponse<MarcarVacinaAplicadaCommandResponse>.AdicionarErro(
                    statusCode: HttpStatusCode.NotFound,
                    mensagem: "Vacina SUS não encontrada.");
            }

            var vacinaAplicada = await _vacinaAplicadaRepository.ObterVacinaAplicadaPorBebeEVacina(request.BebeNascidoId, request.VacinaSusId);

            if (vacinaAplicada == null)
            {
                vacinaAplicada = new VacinaAplicada(
                    bebeNascidoId: request.BebeNascidoId,
                    vacinaSusId: request.VacinaSusId,
                    dataAplicacao: request.DataAplicacao,
                    aplicada: true,
                    observacoes: request.Observacoes,
                    lote: request.Lote,
                    localAplicacao: request.LocalAplicacao
                );
                await _vacinaAplicadaRepository.Adicionar(vacinaAplicada);
            }
            else
            {
                vacinaAplicada.MarcarComoAplicada(request.DataAplicacao, request.Lote, request.LocalAplicacao, request.Observacoes);
                await _vacinaAplicadaRepository.Atualizar(vacinaAplicada);
            }

            var response = new MarcarVacinaAplicadaCommandResponse(vacinaAplicada.Id);

            return CommandResponse<MarcarVacinaAplicadaCommandResponse>.Sucesso(response, HttpStatusCode.OK);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao marcar vacina como aplicada");
            return CommandResponse<MarcarVacinaAplicadaCommandResponse>.ErroCritico(
                mensagem: $"Ocorreu um erro ao marcar a vacina como aplicada: {ex.Message}");
        }
    }
}

