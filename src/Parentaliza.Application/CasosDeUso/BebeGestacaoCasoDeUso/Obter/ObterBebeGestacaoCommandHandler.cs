using MediatR;
using Parentaliza.Application.Mediator;
using Parentaliza.Domain.InterfacesRepository;
using System.Net;

namespace Parentaliza.Application.CasosDeUso.PerfilBebeGestacaoCasoDeUso.Obter;

public class ObterBebeGestacaoCommandHandler : IRequestHandler<ObterBebeGestacaoCommand, CommandResponse<ObterBebeGestacaoCommandResponse>>
{
    private readonly IBebeGestacaoRepository _bebeGestacaoRepository;
    public ObterBebeGestacaoCommandHandler(IBebeGestacaoRepository bebeGestacaoRepository)
    {
        _bebeGestacaoRepository = bebeGestacaoRepository;
    }

    public async Task<CommandResponse<ObterBebeGestacaoCommandResponse>> Handle (ObterBebeGestacaoCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var bebeGestacao = await _bebeGestacaoRepository.ObterPorId(request.Id);

            if (bebeGestacao == null)
            {
                return CommandResponse<ObterBebeGestacaoCommandResponse>.AdicionarErro(
                    statusCode: HttpStatusCode.NotFound,
                    mensagem: "Bebê não encontrado."
                    );
            }

            var bebeGestacaoResponse = new ObterBebeGestacaoCommandResponse(
                bebeGestacao.Id,
                bebeGestacao.Nome,
                bebeGestacao.DataPrevista,
                bebeGestacao.DiasDeGestacao,
                bebeGestacao.PesoEstimado,
                bebeGestacao.ComprimentoEstimado
                );

            return CommandResponse<ObterBebeGestacaoCommandResponse>.Sucesso(bebeGestacaoResponse, HttpStatusCode.OK);
        }
        catch (Exception ex)
        {
            return CommandResponse<ObterBebeGestacaoCommandResponse>.ErroCritico(mensagem: $"Ocorreu um erro ao obter o bêbe: {ex.Message}");
        }
    }
}