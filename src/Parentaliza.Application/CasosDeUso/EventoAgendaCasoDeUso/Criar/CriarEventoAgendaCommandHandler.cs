using MediatR;
using Parentaliza.Application.Mediator;
using Parentaliza.Domain.Entidades;
using Parentaliza.Domain.InterfacesRepository;
using System.Net;

namespace Parentaliza.Application.CasosDeUso.EventoAgendaCasoDeUso.Criar;

public class CriarEventoAgendaCommandHandler : IRequestHandler<CriarEventoAgendaCommand, CommandResponse<CriarEventoAgendaCommandResponse>>
{
    private readonly IEventoAgendaRepository _eventoAgendaRepository;

    public CriarEventoAgendaCommandHandler(IEventoAgendaRepository eventoAgendaRepository)
    {
        _eventoAgendaRepository = eventoAgendaRepository;

    }
    public async Task<CommandResponse<CriarEventoAgendaCommandResponse>> Handle(CriarEventoAgendaCommand request, CancellationToken cancellationToken)
    {
        if (!request.Validar())
        {
            return CommandResponse<CriarEventoAgendaCommandResponse>.ErroValidacao(request.ResultadoDasValidacoes);
        }

        try
        {
            var nomeJaUtilizado = await _eventoAgendaRepository.NomeJaUtilizado(request.Evento);

            if (nomeJaUtilizado)
            {
                return CommandResponse<CriarEventoAgendaCommandResponse>.AdicionarErro(statusCode: HttpStatusCode.Conflict, mensagem: "O nome do evento ou consulta já está em uso.");
            }

            var eventoAgenda = new EventoAgenda(request.Evento, request.Especialidade, request.Localizacao, request.Hora, request.Data, request.Anotacao);

            await _eventoAgendaRepository.Adicionar(eventoAgenda);

            var response = new CriarEventoAgendaCommandResponse(eventoAgenda.Id);


            return CommandResponse<CriarEventoAgendaCommandResponse>.Sucesso(response, statusCode: HttpStatusCode.Created);

        }
        catch (Exception ex)
        {
            return CommandResponse<CriarEventoAgendaCommandResponse>.ErroCritico(mensagem: $"Ocorreu um erro ao criar o evento/consulta: {ex.Message}");
        }

    }
}