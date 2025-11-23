using MediatR;
using Parentaliza.Application.Mediator;
using Parentaliza.Domain.Entidades;
using Parentaliza.Domain.InterfacesRepository;
using System.Net;

namespace Parentaliza.Application.CasosDeUso.BebeNascidoCasoDeUso.Editar;

public class EditarBebeNascidoCommandHandler : IRequestHandler<EditarBebeNascidoCommand, CommandResponse<EditarBebeNascidoCommandResponse>>
{

    private readonly IBebeNascidoRepository _bebeNascidoRepository;

    public EditarBebeNascidoCommandHandler(IBebeNascidoRepository bebeNascidoRepository)
    {
        _bebeNascidoRepository = bebeNascidoRepository;
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

            var bebeNascidoAtualizado = new BebeNascido(
                request.Nome,
                request.DataNascimento,
                request.Sexo,
                request.TipoSanguineo,
                request.IdadeMeses,
                request.Peso,
                request.Altura
            );

            bebeNascidoAtualizado.Id = request.Id;

            await _bebeNascidoRepository.Atualizar(bebeNascidoAtualizado);

            var response = new EditarBebeNascidoCommandResponse(bebeNascidoAtualizado.Id);

            return CommandResponse<EditarBebeNascidoCommandResponse>.Sucesso(response, HttpStatusCode.OK);
        }
        catch (Exception ex)
        {
            // Loger necessario para o 
            return CommandResponse<EditarBebeNascidoCommandResponse>.ErroCritico("Ocorreu um erro ao editar o bebê: " + ex.Message);
        }
    }
}
