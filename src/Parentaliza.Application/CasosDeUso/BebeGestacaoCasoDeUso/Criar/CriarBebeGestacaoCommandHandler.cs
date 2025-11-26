using MediatR;
using Microsoft.Extensions.Logging;
using Parentaliza.Application.Mediator;
using Parentaliza.Domain.Entidades;
using Parentaliza.Domain.InterfacesRepository;
using System.Net;

namespace Parentaliza.Application.CasosDeUso.BebeGestacaoCasoDeUso.Criar
{
    public class CriarBebeGestacaoCommandHandler : IRequestHandler<CriarBebeGestacaoCommand, CommandResponse<CriarBebeGestacaoCommandResponse>>
    {
        private readonly IBebeGestacaoRepository _bebeGestacaoRepository;
        private readonly IResponsavelRepository _responsavelRepository;
        private readonly ILogger<CriarBebeGestacaoCommandHandler> _logger;

        public CriarBebeGestacaoCommandHandler(
            IBebeGestacaoRepository bebeGestacaoRepository,
            IResponsavelRepository responsavelRepository,
            ILogger<CriarBebeGestacaoCommandHandler> logger)
        {
            _bebeGestacaoRepository = bebeGestacaoRepository;
            _responsavelRepository = responsavelRepository;
            _logger = logger;
        }

        public async Task<CommandResponse<CriarBebeGestacaoCommandResponse>> Handle(CriarBebeGestacaoCommand request, CancellationToken cancellationToken)
        {
            if (!request.Validar())
            {
                return CommandResponse<CriarBebeGestacaoCommandResponse>.ErroValidacao(request.ResultadoDasValidacoes);
            }

            try
            {
                var responsavel = await _responsavelRepository.ObterPorId(request.ResponsavelId);
                if (responsavel == null)
                {
                    return CommandResponse<CriarBebeGestacaoCommandResponse>.AdicionarErro(
                        statusCode: HttpStatusCode.NotFound,
                        mensagem: "Responsável não encontrado.");
                }

                var nomeJaUtilizado = await _bebeGestacaoRepository.NomeJaUtilizado(request.Nome);

                if (nomeJaUtilizado)
                {
                    return CommandResponse<CriarBebeGestacaoCommandResponse>.AdicionarErro(statusCode: HttpStatusCode.Conflict, mensagem: "O nome do bebê já está em uso.");
                }

                var bebeGestacao = new BebeGestacao(
                    responsavelId: request.ResponsavelId,
                    nome: request.Nome,
                    dataPrevista: request.DataPrevista,
                    diasDeGestacao: request.DiasDeGestacao,
                    pesoEstimado: request.PesoEstimado,
                    comprimentoEstimado: request.ComprimentoEstimado);

                await _bebeGestacaoRepository.Adicionar(bebeGestacao);

                var response = new CriarBebeGestacaoCommandResponse(bebeGestacao.Id);

                return CommandResponse<CriarBebeGestacaoCommandResponse>.Sucesso(response, statusCode: HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao criar bebê em gestação");
                return CommandResponse<CriarBebeGestacaoCommandResponse>.ErroCritico(mensagem: $"Ocorreu um erro ao criar o bebê em gestação: {ex.Message}");
            }
        }
    }
}