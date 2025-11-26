using MediatR;
using Microsoft.Extensions.Logging;
using Parentaliza.Application.Mediator;
using Parentaliza.Domain.Entidades;
using Parentaliza.Domain.InterfacesRepository;
using System.Net;

namespace Parentaliza.Application.CasosDeUso.BebeGestacaoCasoDeUso.ConverterParaNascido;

public class ConverterBebeGestacaoParaNascidoCommandHandler : IRequestHandler<ConverterBebeGestacaoParaNascidoCommand, CommandResponse<ConverterBebeGestacaoParaNascidoCommandResponse>>
{
    private readonly IBebeGestacaoRepository _bebeGestacaoRepository;
    private readonly IBebeNascidoRepository _bebeNascidoRepository;
    private readonly ILogger<ConverterBebeGestacaoParaNascidoCommandHandler> _logger;

    public ConverterBebeGestacaoParaNascidoCommandHandler(
        IBebeGestacaoRepository bebeGestacaoRepository,
        IBebeNascidoRepository bebeNascidoRepository,
        ILogger<ConverterBebeGestacaoParaNascidoCommandHandler> logger)
    {
        _bebeGestacaoRepository = bebeGestacaoRepository;
        _bebeNascidoRepository = bebeNascidoRepository;
        _logger = logger;
    }

    public async Task<CommandResponse<ConverterBebeGestacaoParaNascidoCommandResponse>> Handle(
        ConverterBebeGestacaoParaNascidoCommand request,
        CancellationToken cancellationToken)
    {
        if (!request.Validar())
        {
            return CommandResponse<ConverterBebeGestacaoParaNascidoCommandResponse>.ErroValidacao(request.ResultadoDasValidacoes);
        }

        try
        {
            // Busca o bebê em gestação
            var bebeGestacao = await _bebeGestacaoRepository.ObterPorId(request.BebeGestacaoId);

            if (bebeGestacao == null)
            {
                return CommandResponse<ConverterBebeGestacaoParaNascidoCommandResponse>.AdicionarErro(
                    statusCode: HttpStatusCode.NotFound,
                    mensagem: "Bebê em gestação não encontrado.");
            }

            // Verifica se o nome já está em uso em BebeNascido
            var nomeJaUtilizado = await _bebeNascidoRepository.NomeJaUtilizado(bebeGestacao.Nome);

            if (nomeJaUtilizado)
            {
                return CommandResponse<ConverterBebeGestacaoParaNascidoCommandResponse>.AdicionarErro(
                    statusCode: HttpStatusCode.Conflict,
                    mensagem: "Já existe um bebê nascido com este nome. Por favor, use um nome diferente ou edite o bebê nascido existente.");
            }

            // Cria o bebê nascido usando os dados do bebê em gestação
            var bebeNascido = new BebeNascido(
                responsavelId: bebeGestacao.ResponsavelId,
                nome: bebeGestacao.Nome,
                dataNascimento: request.DataNascimento,
                sexo: request.Sexo,
                tipoSanguineo: request.TipoSanguineo,
                idadeMeses: request.IdadeMeses,
                peso: request.Peso,
                altura: request.Altura
            );

            await _bebeNascidoRepository.Adicionar(bebeNascido);

            // Se solicitado, exclui o registro de gestação
            if (request.ExcluirBebeGestacao)
            {
                await _bebeGestacaoRepository.Remover(request.BebeGestacaoId);
            }

            var response = new ConverterBebeGestacaoParaNascidoCommandResponse(
                bebeNascidoId: bebeNascido.Id,
                bebeGestacaoId: request.BebeGestacaoId,
                bebeGestacaoExcluido: request.ExcluirBebeGestacao
            );

            return CommandResponse<ConverterBebeGestacaoParaNascidoCommandResponse>.Sucesso(response, HttpStatusCode.Created);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao converter bebê em gestação para bebê nascido");
            return CommandResponse<ConverterBebeGestacaoParaNascidoCommandResponse>.ErroCritico(
                mensagem: $"Ocorreu um erro ao converter o bebê em gestação para bebê nascido: {ex.Message}");
        }
    }
}