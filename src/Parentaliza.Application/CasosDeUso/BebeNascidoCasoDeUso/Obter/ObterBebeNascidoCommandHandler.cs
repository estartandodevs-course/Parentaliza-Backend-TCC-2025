using MediatR;
using Microsoft.Extensions.Logging;
using Parentaliza.Application.CasosDeUso.BebeNascidoCasoDeUso.Obter;
using Parentaliza.Application.Mediator;
using Parentaliza.Domain.InterfacesRepository;
using System.Net;

namespace Parentaliza.Application.CasosDeUso.BebeNascidoCasoDeUso.Obter;

public class ObterBebeNascidoCommandHandler : IRequestHandler<ObterBebeNascidoCommand, CommandResponse<ObterBebeNascidoCommandResponse>>
{
    private readonly IBebeNascidoRepository _bebeNascidoRepository;
    private readonly ILogger<ObterBebeNascidoCommandHandler> _logger;

    public ObterBebeNascidoCommandHandler(
        IBebeNascidoRepository bebeNascidoRepository,
        ILogger<ObterBebeNascidoCommandHandler> logger)
    {
        _bebeNascidoRepository = bebeNascidoRepository;
        _logger = logger;
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
            _logger.LogError(ex, "Erro ao obter bebê nascido");
            return CommandResponse<ObterBebeNascidoCommandResponse>.ErroCritico(mensagem: $"Ocorreu um erro ao obter o bebê nascido: {ex.Message}");
        }
    }
}