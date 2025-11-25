using MediatR;
using Microsoft.Extensions.Logging;
using Parentaliza.Application.Mediator;
using Parentaliza.Domain.Entidades;
using Parentaliza.Domain.InterfacesRepository;
using System.Net;
using System.Reflection;

namespace Parentaliza.Application.CasosDeUso.BebeGestacaoCasoDeUso.Editar;

public class EditarBebeGestacaoCommandHandler : IRequestHandler<EditarBebeGestacaoCommand, CommandResponse<EditarBebeGestacaoCommandResponse>>
{

    private readonly IBebeGestacaoRepository _bebeGestacaoRepository;
    private readonly IResponsavelRepository _responsavelRepository;
    private readonly ILogger<EditarBebeGestacaoCommandHandler> _logger;

    public EditarBebeGestacaoCommandHandler(
        IBebeGestacaoRepository bebeGestacaoRepository,
        IResponsavelRepository responsavelRepository,
        ILogger<EditarBebeGestacaoCommandHandler> logger)
    {
        _bebeGestacaoRepository = bebeGestacaoRepository;
        _responsavelRepository = responsavelRepository;
        _logger = logger;
    }

    public async Task<CommandResponse<EditarBebeGestacaoCommandResponse>> Handle(EditarBebeGestacaoCommand request, CancellationToken cancellationToken)
    {
        if (!request.Validar())
        {
            return CommandResponse<EditarBebeGestacaoCommandResponse>.ErroValidacao(request.ResultadoDasValidacoes);
        }

        try
        {
            var bebeGestacao = await _bebeGestacaoRepository.ObterPorId(request.Id);

            if (bebeGestacao == null)
            {
                return CommandResponse<EditarBebeGestacaoCommandResponse>.AdicionarErro(statusCode: HttpStatusCode.NotFound, mensagem: "Bebê não encontrado.");
            }

            var responsavel = await _responsavelRepository.ObterPorId(request.ResponsavelId);
            if (responsavel == null)
            {
                return CommandResponse<EditarBebeGestacaoCommandResponse>.AdicionarErro(
                    statusCode: HttpStatusCode.NotFound,
                    mensagem: "Responsável não encontrado.");
            }

            // Atualizar a entidade existente usando reflection para acessar propriedades privadas
            var tipo = typeof(BebeGestacao);
            tipo.GetProperty(nameof(BebeGestacao.ResponsavelId))?.SetValue(bebeGestacao, request.ResponsavelId);
            tipo.GetProperty(nameof(BebeGestacao.Nome))?.SetValue(bebeGestacao, request.Nome);
            tipo.GetProperty(nameof(BebeGestacao.DataPrevista))?.SetValue(bebeGestacao, request.DataPrevista);
            tipo.GetProperty(nameof(BebeGestacao.DiasDeGestacao))?.SetValue(bebeGestacao, request.DiasDeGestacao);
            tipo.GetProperty(nameof(BebeGestacao.PesoEstimado))?.SetValue(bebeGestacao, request.PesoEstimado);
            tipo.GetProperty(nameof(BebeGestacao.ComprimentoEstimado))?.SetValue(bebeGestacao, request.ComprimentoEstimado);

            await _bebeGestacaoRepository.Atualizar(bebeGestacao);

            var response = new EditarBebeGestacaoCommandResponse(bebeGestacao.Id);

            return CommandResponse<EditarBebeGestacaoCommandResponse>.Sucesso(response, HttpStatusCode.OK);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao editar bebê em gestação");
            return CommandResponse<EditarBebeGestacaoCommandResponse>.ErroCritico(
                mensagem: $"Ocorreu um erro ao editar o bebê: {ex.Message}");
        }
    }
}