using MediatR;
using Microsoft.Extensions.Logging;
using Parentaliza.Application.Mediator;
using Parentaliza.Domain.Entidades;
using Parentaliza.Domain.InterfacesRepository;
using System.Net;
using System.Reflection;

namespace Parentaliza.Application.CasosDeUso.ControleLeiteMaternoCasoDeUso.Editar;

public class EditarControleLeiteMaternoCommandHandler : IRequestHandler<EditarControleLeiteMaternoCommand, CommandResponse<EditarControleLeiteMaternoCommandResponse>>
{
    private readonly IControleLeiteMaternoRepository _controleLeiteMaternoRepository;
    private readonly IBebeNascidoRepository _bebeNascidoRepository;
    private readonly ILogger<EditarControleLeiteMaternoCommandHandler> _logger;

    public EditarControleLeiteMaternoCommandHandler(
        IControleLeiteMaternoRepository controleLeiteMaternoRepository,
        IBebeNascidoRepository bebeNascidoRepository,
        ILogger<EditarControleLeiteMaternoCommandHandler> logger)
    {
        _controleLeiteMaternoRepository = controleLeiteMaternoRepository;
        _bebeNascidoRepository = bebeNascidoRepository;
        _logger = logger;
    }

    public async Task<CommandResponse<EditarControleLeiteMaternoCommandResponse>> Handle(EditarControleLeiteMaternoCommand request, CancellationToken cancellationToken)
    {
        if (!request.Validar())
        {
            return CommandResponse<EditarControleLeiteMaternoCommandResponse>.ErroValidacao(request.ResultadoDasValidacoes);
        }

        try
        {
            var controleLeiteMaterno = await _controleLeiteMaternoRepository.ObterPorId(request.Id);

            if (controleLeiteMaterno == null)
            {
                return CommandResponse<EditarControleLeiteMaternoCommandResponse>.AdicionarErro(
                    statusCode: HttpStatusCode.NotFound,
                    mensagem: "Controle de leite materno não encontrado.");
            }

            var bebe = await _bebeNascidoRepository.ObterPorId(request.BebeNascidoId);
            if (bebe == null)
            {
                return CommandResponse<EditarControleLeiteMaternoCommandResponse>.AdicionarErro(
                    statusCode: HttpStatusCode.NotFound,
                    mensagem: "Bebê não encontrado.");
            }

            // Atualizar a entidade existente usando reflection para acessar propriedades privadas
            var tipo = typeof(ControleLeiteMaterno);
            tipo.GetProperty(nameof(ControleLeiteMaterno.BebeNascidoId))?.SetValue(controleLeiteMaterno, request.BebeNascidoId);
            tipo.GetProperty(nameof(ControleLeiteMaterno.Cronometro))?.SetValue(controleLeiteMaterno, request.Cronometro);
            tipo.GetProperty(nameof(ControleLeiteMaterno.LadoDireito))?.SetValue(controleLeiteMaterno, request.LadoDireito);
            tipo.GetProperty(nameof(ControleLeiteMaterno.LadoEsquerdo))?.SetValue(controleLeiteMaterno, request.LadoEsquerdo);

            await _controleLeiteMaternoRepository.Atualizar(controleLeiteMaterno);

            var response = new EditarControleLeiteMaternoCommandResponse(controleLeiteMaterno.Id);

            return CommandResponse<EditarControleLeiteMaternoCommandResponse>.Sucesso(response, HttpStatusCode.OK);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao editar controle de leite materno");
            return CommandResponse<EditarControleLeiteMaternoCommandResponse>.ErroCritico(
                mensagem: $"Ocorreu um erro ao editar o controle de leite materno: {ex.Message}");
        }
    }
}

