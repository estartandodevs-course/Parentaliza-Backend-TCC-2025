using MediatR;
using Microsoft.Extensions.Logging;
using Parentaliza.Application.Mediator;
using Parentaliza.Domain.Entidades;
using Parentaliza.Domain.InterfacesRepository;
using System.Net;

namespace Parentaliza.Application.CasosDeUso.BebeNascidoCasoDeUso.Criar;

public class CriarBebeNascidoCommandHandler : IRequestHandler<CriarBebeNascidoCommand, CommandResponse<CriarBebeNascidoCommandResponse>>
{
    private readonly IBebeNascidoRepository _criarBebeNascidoRepository;
    private readonly IResponsavelRepository _responsavelRepository;
    private readonly ILogger<CriarBebeNascidoCommandHandler> _logger;

    public CriarBebeNascidoCommandHandler(
        IBebeNascidoRepository criarBebeNascidoRepository,
        IResponsavelRepository responsavelRepository,
        ILogger<CriarBebeNascidoCommandHandler> logger)
    {
        _criarBebeNascidoRepository = criarBebeNascidoRepository;
        _responsavelRepository = responsavelRepository;
        _logger = logger;
    }

    public async Task<CommandResponse<CriarBebeNascidoCommandResponse>> Handle(CriarBebeNascidoCommand request, CancellationToken cancellationToken)
    {
        if (!request.Validar())
        {
            return CommandResponse<CriarBebeNascidoCommandResponse>.ErroValidacao(request.ResultadoDasValidacoes);
        }
        try
        {
            var responsavel = await _responsavelRepository.ObterPorId(request.ResponsavelId);
            if (responsavel == null)
            {
                return CommandResponse<CriarBebeNascidoCommandResponse>.AdicionarErro(
                    statusCode: HttpStatusCode.NotFound,
                    mensagem: "Responsável não encontrado.");
            }

            var nomeJaUtilizado = await _criarBebeNascidoRepository.NomeJaUtilizado(request.Nome);

            if (nomeJaUtilizado)
            {
                return CommandResponse<CriarBebeNascidoCommandResponse>.AdicionarErro(statusCode: HttpStatusCode.Conflict, mensagem: "O nome do bebê já está em uso.");
            }

            var bebeNascido = new BebeNascido(
                responsavelId: request.ResponsavelId,
                nome: request.Nome,
                dataNascimento: request.DataNascimento,
                sexo: request.Sexo,
                tipoSanguineo: request.TipoSanguineo,
                idadeMeses: request.IdadeMeses,
                peso: request.Peso,
                altura: request.Altura
            );

            await _criarBebeNascidoRepository.Adicionar(bebeNascido);

            var response = new CriarBebeNascidoCommandResponse(bebeNascido.Id);

            return CommandResponse<CriarBebeNascidoCommandResponse>.Sucesso(response, statusCode: HttpStatusCode.Created);

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao criar bebê nascido");
            return CommandResponse<CriarBebeNascidoCommandResponse>.ErroCritico(mensagem: $"Ocorreu um erro ao criar o bebê: {ex.Message}");
        }
    }
}
