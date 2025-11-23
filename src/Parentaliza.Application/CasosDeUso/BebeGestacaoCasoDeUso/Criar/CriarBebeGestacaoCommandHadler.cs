using MediatR;
using Parentaliza.Application.Mediator;
using Parentaliza.Domain.Entidades;
using Parentaliza.Domain.InterfacesRepository;
using System.Net;

namespace Parentaliza.Application.CasosDeUso.BebeGestacaoCasoDeUso.Criar
{
    public class CriarBebeGestacaoCommandHadler : IRequestHandler<CriarBebeGestacaoCommand, CommandResponse<CriarBebeGestacaoCommandResponse>>
    {
        private readonly IBebeGestacaoRepository _bebeGestacaoRepository;

        public CriarBebeGestacaoCommandHadler(IBebeGestacaoRepository bebeGestacaoRepository)
        {
            _bebeGestacaoRepository = bebeGestacaoRepository;
        }

        public async Task<CommandResponse<CriarBebeGestacaoCommandResponse>> Handle(CriarBebeGestacaoCommand request, CancellationToken cancellationToken)
        {
            if (!request.Validar())
            {
                return CommandResponse<CriarBebeGestacaoCommandResponse>.ErroValidacao(request.ResultadoDasValidacoes);
            }

            try
            {
                var nomeJaUtilizado = await _bebeGestacaoRepository.NomeJaUtilizado(request.Nome);

                if (nomeJaUtilizado)
                {
                    return CommandResponse<CriarBebeGestacaoCommandResponse>.AdicionarErro(statusCode: HttpStatusCode.Conflict, mensagem: "O nome do bebê já está em uso.");
                }

                var bebeGestacao = new BebeGestacao(request.Nome, request.DataPrevista, request.DiasDeGestacao, request.PesoEstimado, request.ComprimentoEstimado);

                await _bebeGestacaoRepository.Adicionar(bebeGestacao);

                var response = new CriarBebeGestacaoCommandResponse(bebeGestacao.Id);

                return CommandResponse<CriarBebeGestacaoCommandResponse>.Sucesso(response, statusCode: HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                return CommandResponse<CriarBebeGestacaoCommandResponse>.ErroCritico(mensagem: $"Ocorreu um erro ao registrar o bebê gestacao, {ex.Message}");
            }
        }
    }
}