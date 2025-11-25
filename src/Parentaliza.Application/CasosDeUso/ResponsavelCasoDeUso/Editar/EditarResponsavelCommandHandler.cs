using MediatR;
using Microsoft.Extensions.Logging;
using Parentaliza.Application.Mediator;
using Parentaliza.Domain.Entidades;
using Parentaliza.Domain.InterfacesRepository;
using System.Net;
using System.Reflection;

namespace Parentaliza.Application.CasosDeUso.ResponsavelCasoDeUso.Editar;

public class EditarResponsavelCommandHandler : IRequestHandler<EditarResponsavelCommand, CommandResponse<EditarResponsavelCommandResponse>>
{
    private readonly IResponsavelRepository _responsavelRepository;
    private readonly ILogger<EditarResponsavelCommandHandler> _logger;

    public EditarResponsavelCommandHandler(
        IResponsavelRepository responsavelRepository,
        ILogger<EditarResponsavelCommandHandler> logger)
    {
        _responsavelRepository = responsavelRepository;
        _logger = logger;
    }

    public async Task<CommandResponse<EditarResponsavelCommandResponse>> Handle(EditarResponsavelCommand request, CancellationToken cancellationToken)
    {
        if (!request.Validar())
        {
            return CommandResponse<EditarResponsavelCommandResponse>.ErroValidacao(request.ResultadoDasValidacoes);
        }

        try
        {
            var responsavel = await _responsavelRepository.ObterPorId(request.Id);

            if (responsavel == null)
            {
                return CommandResponse<EditarResponsavelCommandResponse>.AdicionarErro(
                    statusCode: HttpStatusCode.NotFound,
                    mensagem: "Responsável não encontrado.");
            }

            if (responsavel.Email?.ToLower() != request.Email?.ToLower())
            {
                var emailJaUtilizado = await _responsavelRepository.EmailJaUtilizado(request.Email);
                if (emailJaUtilizado)
                {
                    return CommandResponse<EditarResponsavelCommandResponse>.AdicionarErro(
                        statusCode: HttpStatusCode.Conflict,
                        mensagem: "O email já está em uso.");
                }
            }

            // Atualizar a entidade existente usando reflection para acessar propriedades privadas
            var tipo = typeof(Responsavel);
            tipo.GetProperty(nameof(Responsavel.Nome))?.SetValue(responsavel, request.Nome);
            tipo.GetProperty(nameof(Responsavel.Email))?.SetValue(responsavel, request.Email);
            tipo.GetProperty(nameof(Responsavel.TipoResponsavel))?.SetValue(responsavel, request.TipoResponsavel);
            
            // Atualizar senha apenas se fornecida
            if (!string.IsNullOrWhiteSpace(request.Senha))
            {
                tipo.GetProperty(nameof(Responsavel.Senha))?.SetValue(responsavel, request.Senha);
            }
            
            tipo.GetProperty(nameof(Responsavel.FaseNascimento))?.SetValue(responsavel, request.FaseNascimento);

            await _responsavelRepository.Atualizar(responsavel);

            var response = new EditarResponsavelCommandResponse(responsavel.Id);

            return CommandResponse<EditarResponsavelCommandResponse>.Sucesso(response, HttpStatusCode.OK);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao editar responsável");
            return CommandResponse<EditarResponsavelCommandResponse>.ErroCritico(
                mensagem: $"Ocorreu um erro ao editar o responsável: {ex.Message}");
        }
    }
}

