using MediatR;
using Parentaliza.Application.Mediator;
using Parentaliza.Domain.Entidades;
using Parentaliza.Domain.InterfacesRepository;
using System.Net;

namespace Parentaliza.Application.CasosDeUso.EventoAgendaCasoDeUso.Editar;

public class EditarEventoAgendaCommandHandler : IRequestHandler<EditarEventoAgendaCommand, CommandResponse<EditarEventoAgendaCommandResponse>>
{
    private readonly IEventoAgendaRepository _eventoAgendaRepository;

    public EditarEventoAgendaCommandHandler(IEventoAgendaRepository eventoAgendaRepository)
    {
        _eventoAgendaRepository = eventoAgendaRepository;
    }

    public async Task<CommandResponse<EditarEventoAgendaCommandResponse>> Handle(EditarEventoAgendaCommand request, CancellationToken cancellationToken)
    {
        if (!request.Validar())
        {
            return CommandResponse<EditarEventoAgendaCommandResponse>.ErroValidacao(request.ResultadoDasValidacoes);
        }

        try
        {
            var eventoAgenda = await _eventoAgendaRepository.ObterPorId(request.Id);

            if (eventoAgenda == null)
            {
                return CommandResponse<EditarEventoAgendaCommandResponse>.AdicionarErro(
                    statusCode: HttpStatusCode.NotFound,
                    mensagem: "Evento da agenda não encontrado.");
            }

            if (eventoAgenda.Evento?.ToLower() != request.Evento?.ToLower())
            {
                var nomeJaUtilizado = await _eventoAgendaRepository.NomeJaUtilizado(request.Evento);
                if (nomeJaUtilizado)
                {
                    return CommandResponse<EditarEventoAgendaCommandResponse>.AdicionarErro(
                        statusCode: HttpStatusCode.Conflict,
                        mensagem: "O nome do evento ou consulta já está em uso.");
                }
            }

            var eventoAgendaAtualizado = new EventoAgenda(
                request.Evento,
                request.Especialidade,
                request.Localizacao,
                request.Hora,
                request.Data,
                request.Anotacao
            );

            eventoAgendaAtualizado.Id = request.Id;

            await _eventoAgendaRepository.Atualizar(eventoAgendaAtualizado);

            var response = new EditarEventoAgendaCommandResponse(eventoAgendaAtualizado.Id);

            return CommandResponse<EditarEventoAgendaCommandResponse>.Sucesso(response, HttpStatusCode.OK);
        }
        catch (Exception ex)
        {
            return CommandResponse<EditarEventoAgendaCommandResponse>.ErroCritico(
                mensagem: $"Ocorreu um erro ao editar o evento da agenda: {ex.Message}");
        }
    }
}