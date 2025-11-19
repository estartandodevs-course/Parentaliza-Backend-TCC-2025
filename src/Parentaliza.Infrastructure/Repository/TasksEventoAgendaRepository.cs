using Microsoft.EntityFrameworkCore;
using Parentaliza.API.Infrastructure;
using Parentaliza.Domain.Entidades;
using Parentaliza.Domain.InterfacesRepository;

namespace Parentaliza.Infrastructure.Repository;

public class TasksEventoAgendaRepository : Repository<EventoAgenda>, IEventoAgendaRepository
{
    public TasksEventoAgendaRepository(ApplicationDbContext contexto) : base(contexto) { }

    public async Task<EventoAgenda> ObterInformacoesAgendamento()
    {
        var response = await _contexto.EventosAgendas
            .AsNoTracking()
            .Include(eventoAgenda => eventoAgenda.Evento)
            .FirstOrDefaultAsync();

        return response;
    }
    public Task<bool> NomeJaUtilizado(string? eventoAgenda)
    {
        throw new NotImplementedException();
    }
}