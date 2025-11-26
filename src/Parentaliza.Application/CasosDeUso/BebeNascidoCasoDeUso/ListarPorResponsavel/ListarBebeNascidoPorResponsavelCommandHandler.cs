using MediatR;
using Microsoft.Extensions.Logging;
using Parentaliza.Application.Mediator;
using Parentaliza.Domain.InterfacesRepository;
using System.Net;

namespace Parentaliza.Application.CasosDeUso.BebeNascidoCasoDeUso.ListarPorResponsavel;

public class ListarBebeNascidoPorResponsavelCommandHandler : IRequestHandler<ListarBebeNascidoPorResponsavelCommand, CommandResponse<List<ListarBebeNascidoPorResponsavelCommandResponse>>>
{
    private readonly IBebeNascidoRepository _bebeNascidoRepository;
    private readonly IResponsavelRepository _responsavelRepository;
    private readonly ILogger<ListarBebeNascidoPorResponsavelCommandHandler> _logger;

    public ListarBebeNascidoPorResponsavelCommandHandler(
        IBebeNascidoRepository bebeNascidoRepository,
        IResponsavelRepository responsavelRepository,
        ILogger<ListarBebeNascidoPorResponsavelCommandHandler> logger)
    {
        _bebeNascidoRepository = bebeNascidoRepository;
        _responsavelRepository = responsavelRepository;
        _logger = logger;
    }

    public async Task<CommandResponse<List<ListarBebeNascidoPorResponsavelCommandResponse>>> Handle(
        ListarBebeNascidoPorResponsavelCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            var responsavel = await _responsavelRepository.ObterPorId(request.ResponsavelId);
            if (responsavel == null)
            {
                return CommandResponse<List<ListarBebeNascidoPorResponsavelCommandResponse>>.AdicionarErro(
                    statusCode: HttpStatusCode.NotFound,
                    mensagem: "Responsável não encontrado.");
            }

            var bebes = await _bebeNascidoRepository.ObterPorResponsavelId(request.ResponsavelId);

            var response = bebes.Select(bebe => new ListarBebeNascidoPorResponsavelCommandResponse(
                bebe.Id,
                bebe.ResponsavelId,
                bebe.Nome,
                bebe.DataNascimento,
                bebe.Sexo,
                bebe.TipoSanguineo,
                bebe.IdadeMeses,
                bebe.Peso,
                bebe.Altura
            )).ToList();

            return CommandResponse<List<ListarBebeNascidoPorResponsavelCommandResponse>>.Sucesso(response, HttpStatusCode.OK);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao listar bebês nascidos do responsável");
            return CommandResponse<List<ListarBebeNascidoPorResponsavelCommandResponse>>.ErroCritico(
                mensagem: $"Ocorreu um erro ao listar os bebês nascidos do responsável: {ex.Message}");
        }
    }
}