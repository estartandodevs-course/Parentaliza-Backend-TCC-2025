using MediatR;
using Parentaliza.Application.CasosDeUso.PerfilBebe.Obter;
using Parentaliza.Application.Mediator;
using Parentaliza.Domain.InterfacesRepository;
using System.Net;

namespace Parentaliza.Application.CasosDeUso.BebeNascidoCasoDeUso.Obter;

public class ObterBebeNascidoCommandHandler : IRequestHandler<ObterBebeNascidoCommand, CommandResponse<ObterBebeNascidoCommandResponse>>
{
    private readonly IBebeNascidoRepository _bebeNascidoRepository;

    public ObterBebeNascidoCommandHandler(IBebeNascidoRepository bebeNascidoRepository)
    {
        _bebeNascidoRepository = bebeNascidoRepository;
    }

    public async Task<CommandResponse<ObterBebeNascidoCommandResponse>> Handle(ObterBebeNascidoCommand request, CancellationToken cancellationToken)
    {
        if(!request.Validar())
        {
            return CommandResponse<ObterBebeNascidoCommandResponse>.ErroValidacao(request.ResultadoDasValidacoes);
        }
        try
        {
            var bebeNascido = await _bebeNascidoRepository.ObterBebeNascido(request.Id);

            if(bebeNascido == null)
            {
                return CommandResponse<ObterBebeNascidoCommandResponse>.AdicionarErro("Bebê nascido não encontrado", HttpStatusCode.NotFound);
            }

            var bebeNascidoResponse = new ObterBebeNascidoCommandResponse
            (
                bebeNascido.Id,
                bebeNascido.Nome,
                bebeNascido.DataNascimento,
                bebeNascido.Sexo,
                bebeNascido.TipoSanguineo,
                bebeNascido.IdadeMeses,
                bebeNascido.Peso,
                bebeNascido.Altura
            );

            return CommandResponse<ObterBebeNascidoCommandResponse>.Sucesso(bebeNascidoResponse, HttpStatusCode.OK);
        }
        catch(Exception ex)
        {
            return CommandResponse<ObterBebeNascidoCommandResponse>.ErroCritico($"Erro ao obter bebê nascido: {ex.Message}");
        }
    }
}