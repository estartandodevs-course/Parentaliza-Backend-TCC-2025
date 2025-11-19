using MediatR;
using Parentaliza.Application.Mediator;
using Parentaliza.Domain.Entidades;
using Parentaliza.Domain.InterfacesRepository;
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

            var bebeNascido = new BebeNascido(
                responsavelIdn: request.ResponsavelIdN,
                nome: request.Nome,
                dataNascimento: request.DataNascimento,
                sexo: request.Sexo,
                tipoSanguineo: request.TipoSanguineo,
                peso: request.Peso,
                altura: request.Altura
            );

            await _criarBebeNascidoRepository.Adicionar(bebeNascido);

            var response = new CriarBebeNascidoCommandResponse(bebeNascido.Id);

            return CommandResponse<CriarBebeNascidoCommandResponse>.Sucesso(response, statusCode: HttpStatusCode.Created);

        }
        catch (Exception ex)
        {
            return CommandResponse<CriarBebeNascidoCommandResponse>.ErroCritico(mensagem: $"Ocorreu um erro ao cadastrar um bebê: {ex.Message}");
        }
    }
}
