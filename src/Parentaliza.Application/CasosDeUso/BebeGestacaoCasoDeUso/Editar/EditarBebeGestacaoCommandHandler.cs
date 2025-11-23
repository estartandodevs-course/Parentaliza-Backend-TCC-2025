using MediatR;
using Parentaliza.Application.Mediator;
using Parentaliza.Domain.Entidades;
using Parentaliza.Domain.InterfacesRepository;
using System.Net;

namespace Parentaliza.Application.CasosDeUso.PerfilBebeGestacaoCasoDeUso.Editar;

public class EditarBebeGestacaoCommandHandler : IRequestHandler<EditarBebeGestacaoCommand, CommandResponse<EditarBebeGestacaoCommandResponse>>
{

    private readonly IBebeGestacaoRepository _bebeGestacaoRepository;

    public EditarBebeGestacaoCommandHandler(IBebeGestacaoRepository bebeGestacaoRepository)
    {
        _bebeGestacaoRepository = bebeGestacaoRepository;
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

            var bebeGestacaoAtualizado = new BebeGestacao(
                request.Nome,
                request.DataPrevista,
                request.DiasDeGestacao,
                request.PesoEstimado,
                request.ComprimetoEstimado
            );

            bebeGestacaoAtualizado.Id = request.Id;

            await _bebeGestacaoRepository.Atualizar(bebeGestacaoAtualizado);

            var response = new EditarBebeGestacaoCommandResponse(bebeGestacaoAtualizado.Id);

            return CommandResponse<EditarBebeGestacaoCommandResponse>.Sucesso(response, HttpStatusCode.OK);
        }
        catch (Exception ex)
        {
            return CommandResponse<EditarBebeGestacaoCommandResponse>.ErroCritico("Ocorreu um erro ao editar o bebê: " + ex.Message);
        }
    }
}