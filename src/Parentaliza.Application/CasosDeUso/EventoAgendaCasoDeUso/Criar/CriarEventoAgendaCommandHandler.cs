using MediatR;
using Parentaliza.Application.Mediator;
using Parentaliza.Domain.Entidades;
using Parentaliza.Domain.Repository;
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
            // Criar nossa entidade
            var eventoAgenda = new EventoAgenda(
                evento: request.Evento,
                especialidade: request.Especialidade,
                localizacao: request.Localizacao,
                horario: request.Horario,
                data: request.Data,
                anotacao: request.Anotacao
            );

            // Salvar no banco de dados
            await _eventoAgendaRepository.Adicionar(eventoAgenda);

            var response = new CriarEventoAgendaCommandResponse(eventoAgenda.Id);

            // Retornar uma resposta
            return CommandResponse<CriarEventoAgendaCommandResponse>.Sucesso(response, statusCode: HttpStatusCode.Created);
        }
        catch (Exception ex)
        {
            return CommandResponse<CriarEventoAgendaCommandResponse>.ErroCritico(mensagem: $"Ocorreu um erro ao criar o evento: {ex.Message}");
        }
    }
}
