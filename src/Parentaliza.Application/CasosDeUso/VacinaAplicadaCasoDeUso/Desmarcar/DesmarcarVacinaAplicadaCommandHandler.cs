using MediatR;
using Microsoft.Extensions.Logging;
using Parentaliza.Application.Mediator;
using Parentaliza.Domain.Entidades;
using Parentaliza.Domain.InterfacesRepository;
using System.Net;

namespace Parentaliza.Application.CasosDeUso.VacinaAplicadaCasoDeUso.Desmarcar;

public class DesmarcarVacinaAplicadaCommandHandler : IRequestHandler<DesmarcarVacinaAplicadaCommand, CommandResponse<DesmarcarVacinaAplicadaCommandResponse>>
{
    private readonly IVacinaAplicadaRepository _vacinaAplicadaRepository;
    private readonly IBebeNascidoRepository _bebeNascidoRepository;
    private readonly IVacinaSusRepository _vacinaSusRepository;
    private readonly ILogger<DesmarcarVacinaAplicadaCommandHandler> _logger;

    public DesmarcarVacinaAplicadaCommandHandler(
        IVacinaAplicadaRepository vacinaAplicadaRepository,
        IBebeNascidoRepository bebeNascidoRepository,
        IVacinaSusRepository vacinaSusRepository,
        ILogger<DesmarcarVacinaAplicadaCommandHandler> logger)
    {
        _vacinaAplicadaRepository = vacinaAplicadaRepository;
        _bebeNascidoRepository = bebeNascidoRepository;
        _vacinaSusRepository = vacinaSusRepository;
        _logger = logger;
    }

    public async Task<CommandResponse<DesmarcarVacinaAplicadaCommandResponse>> Handle(DesmarcarVacinaAplicadaCommand request, CancellationToken cancellationToken)
    {
        if (!request.Validar())
        {
            return CommandResponse<DesmarcarVacinaAplicadaCommandResponse>.ErroValidacao(request.ResultadoDasValidacoes);
        }

        try
        {
            var bebe = await _bebeNascidoRepository.ObterPorId(request.BebeNascidoId);
            if (bebe == null)
            {
                return CommandResponse<DesmarcarVacinaAplicadaCommandResponse>.AdicionarErro(
                    statusCode: HttpStatusCode.NotFound,
                    mensagem: "Bebê não encontrado.");
            }

            var vacinaSus = await _vacinaSusRepository.ObterPorId(request.VacinaSusId);
            if (vacinaSus == null)
            {
                return CommandResponse<DesmarcarVacinaAplicadaCommandResponse>.AdicionarErro(
                    statusCode: HttpStatusCode.NotFound,
                    mensagem: "Vacina SUS não encontrada.");
            }

            var vacinaAplicada = await _vacinaAplicadaRepository.ObterVacinaAplicadaPorBebeEVacina(request.BebeNascidoId, request.VacinaSusId);

            if (vacinaAplicada == null)
            {
                // Se não existe, cria um registro marcado como não aplicada
                vacinaAplicada = new VacinaAplicada(
                    bebeNascidoId: request.BebeNascidoId,
                    vacinaSusId: request.VacinaSusId,
                    dataAplicacao: null,
                    aplicada: false,
                    observacoes: null,
                    lote: null,
                    localAplicacao: null
                );
                await _vacinaAplicadaRepository.Adicionar(vacinaAplicada);
            }
            else
            {
                // Se existe, desmarca
                vacinaAplicada.MarcarComoNaoAplicada();
                await _vacinaAplicadaRepository.Atualizar(vacinaAplicada);
            }

            var response = new DesmarcarVacinaAplicadaCommandResponse(vacinaAplicada.Id);

            return CommandResponse<DesmarcarVacinaAplicadaCommandResponse>.Sucesso(response, HttpStatusCode.OK);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao desmarcar vacina");
            return CommandResponse<DesmarcarVacinaAplicadaCommandResponse>.ErroCritico(
                mensagem: $"Ocorreu um erro ao desmarcar a vacina: {ex.Message}");
        }
    }
}

