using MediatR;
using Microsoft.Extensions.Logging;
using Parentaliza.Application.Mediator;
using Parentaliza.Domain.Entidades;
using Parentaliza.Domain.InterfacesRepository;
using System.Net;

namespace Parentaliza.Application.CasosDeUso.ResponsavelCasoDeUso.Criar;

public class CriarResponsavelCommandHandler : IRequestHandler<CriarResponsavelCommand, CommandResponse<CriarResponsavelCommandResponse>>
{
    private readonly IResponsavelRepository _responsavelRepository;
    private readonly ILogger<CriarResponsavelCommandHandler> _logger;

    public CriarResponsavelCommandHandler(
        IResponsavelRepository responsavelRepository,
        ILogger<CriarResponsavelCommandHandler> logger)
    {
        _responsavelRepository = responsavelRepository;
        _logger = logger;
    }

    public async Task<CommandResponse<CriarResponsavelCommandResponse>> Handle(CriarResponsavelCommand request, CancellationToken cancellationToken)
    {
        if (!request.Validar())
        {
            return CommandResponse<CriarResponsavelCommandResponse>.ErroValidacao(request.ResultadoDasValidacoes);
        }

        try
        {
            var emailJaUtilizado = await _responsavelRepository.EmailJaUtilizado(request.Email);

            if (emailJaUtilizado)
            {
                return CommandResponse<CriarResponsavelCommandResponse>.AdicionarErro(
                    statusCode: HttpStatusCode.Conflict,
                    mensagem: "O email já está em uso.");
            }

            var responsavel = new Responsavel(
                request.Nome,
                request.Email,
                (int)request.TipoResponsavel,
                request.Senha,
                request.FaseNascimento
            );

            await _responsavelRepository.Adicionar(responsavel);

            var response = new CriarResponsavelCommandResponse(responsavel.Id);

            return CommandResponse<CriarResponsavelCommandResponse>.Sucesso(response, HttpStatusCode.Created);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao criar responsável");
            return CommandResponse<CriarResponsavelCommandResponse>.ErroCritico(
                mensagem: $"Ocorreu um erro ao criar o responsável: {ex.Message}");
        }
    }
}

