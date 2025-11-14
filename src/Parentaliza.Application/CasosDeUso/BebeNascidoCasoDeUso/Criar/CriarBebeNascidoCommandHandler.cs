using MediatR;
using Parentaliza.Application.Mediator;
using Parentaliza.Domain.Entidades;
using Parentaliza.Domain.Repository;
using System.Net;

namespace Parentaliza.Application.CasosDeUso.BebeNascidoCasoDeUso.Criar;

public class CriarBebeNascidoCommandHandler : IRequestHandler<CriarBebeNascidoCommand, CommandResponse<CriarBebeNascidoCommandResponse>>
{
    private readonly IBebeNascidoRepository _criarBebeNascidoRepository;

    public CriarBebeNascidoCommandHandler(IBebeNascidoRepository criarBebeNascidoRepository)
    {
        _criarBebeNascidoRepository = criarBebeNascidoRepository;
    }

    public async Task<CommandResponse<CriarBebeNascidoCommandResponse>> Handle(CriarBebeNascidoCommand request, CancellationToken cancellationToken)
    {
        if (!request.Validar())
        {
            return CommandResponse<CriarBebeNascidoCommandResponse>.ErroValidacao(request.ResultadoDasValidacoes);
        }
        try
        {
            var nomeJaUtilizado = await _criarBebeNascidoRepository.NomeJaUtilizado(request.Nome);

            if (nomeJaUtilizado)
            {
                return CommandResponse<CriarBebeNascidoCommandResponse>.AdicionarErro(statusCode: HttpStatusCode.Conflict, mensagem: "O nome do bebê já está em uso.");
            }

            // Criar nossa entidade 
            var bebeNascido = new BebeNascido(
                nome: request.Nome,
                dataNascimento: request.DataNascimento,
                sexo: request.Sexo,
                tipoSanguineo: request.TipoSanguineo,
                peso: request.Peso,
                altura: request.Altura
            );

            //Salvar no banco de dados
            await _criarBebeNascidoRepository.Adicionar(bebeNascido);

            var response = new CriarBebeNascidoCommandResponse(bebeNascido.Id);

            // Retornar uma resposta  
            return CommandResponse<CriarBebeNascidoCommandResponse>.Sucesso(response, statusCode: HttpStatusCode.Created);

        }
        catch (Exception ex)
        {
            return CommandResponse<CriarBebeNascidoCommandResponse>.ErroCritico(mensagem: $"Ocorreu um erro ao criar o fornecedor: {ex.Message}");
        }
    }
}
