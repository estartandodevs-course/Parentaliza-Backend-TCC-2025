using MediatR;
using Microsoft.Extensions.Logging;
using Parentaliza.Application.Mediator;
using Parentaliza.Domain.Entidades;
using Parentaliza.Domain.InterfacesRepository;
using System.Net;
using System.Reflection;

namespace Parentaliza.Application.CasosDeUso.BebeNascidoCasoDeUso.Editar;

public class EditarBebeNascidoCommandHandler : IRequestHandler<EditarBebeNascidoCommand, CommandResponse<EditarBebeNascidoCommandResponse>>
{

    private readonly IBebeNascidoRepository _bebeNascidoRepository;
    private readonly IResponsavelRepository _responsavelRepository;
    private readonly ILogger<EditarBebeNascidoCommandHandler> _logger;

    public EditarBebeNascidoCommandHandler(
        IBebeNascidoRepository bebeNascidoRepository,
        IResponsavelRepository responsavelRepository,
        ILogger<EditarBebeNascidoCommandHandler> logger)
    {
        _bebeNascidoRepository = bebeNascidoRepository;
        _responsavelRepository = responsavelRepository;
        _logger = logger;
    }

    public async Task<CommandResponse<EditarBebeNascidoCommandResponse>> Handle(EditarBebeNascidoCommand request, CancellationToken cancellationToken)
    {
        if (!request.Validar())
        {
            return CommandResponse<EditarBebeNascidoCommandResponse>.ErroValidacao(request.ResultadoDasValidacoes);
        }

        try
        {
            var bebeNascido = await _bebeNascidoRepository.ObterPorId(request.Id);

            if (bebeNascido == null)
            {
                return CommandResponse<EditarBebeNascidoCommandResponse>.AdicionarErro(statusCode: System.Net.HttpStatusCode.NotFound, mensagem: "Bebê não encontrado.");
            }

            var responsavel = await _responsavelRepository.ObterPorId(request.ResponsavelId);
            if (responsavel == null)
            {
                return CommandResponse<EditarBebeNascidoCommandResponse>.AdicionarErro(
                    statusCode: HttpStatusCode.NotFound,
                    mensagem: "Responsável não encontrado.");
            }

            // Atualizar a entidade existente usando reflection para acessar propriedades privadas
            var tipo = typeof(BebeNascido);
            tipo.GetProperty(nameof(BebeNascido.ResponsavelId))?.SetValue(bebeNascido, request.ResponsavelId);
            tipo.GetProperty(nameof(BebeNascido.Nome))?.SetValue(bebeNascido, request.Nome);
            tipo.GetProperty(nameof(BebeNascido.DataNascimento))?.SetValue(bebeNascido, request.DataNascimento);
            tipo.GetProperty(nameof(BebeNascido.Sexo))?.SetValue(bebeNascido, request.Sexo);
            tipo.GetProperty(nameof(BebeNascido.TipoSanguineo))?.SetValue(bebeNascido, request.TipoSanguineo);
            tipo.GetProperty(nameof(BebeNascido.IdadeMeses))?.SetValue(bebeNascido, request.IdadeMeses);
            tipo.GetProperty(nameof(BebeNascido.Peso))?.SetValue(bebeNascido, request.Peso);
            tipo.GetProperty(nameof(BebeNascido.Altura))?.SetValue(bebeNascido, request.Altura);

            await _bebeNascidoRepository.Atualizar(bebeNascido);

            var response = new EditarBebeNascidoCommandResponse(bebeNascido.Id);

            return CommandResponse<EditarBebeNascidoCommandResponse>.Sucesso(response, HttpStatusCode.OK);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao editar bebê nascido");
            return CommandResponse<EditarBebeNascidoCommandResponse>.ErroCritico(
                mensagem: $"Ocorreu um erro ao editar o bebê: {ex.Message}");
        }
    }
}
