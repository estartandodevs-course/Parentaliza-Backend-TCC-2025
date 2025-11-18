using Microsoft.EntityFrameworkCore;
using Parentaliza.API.Infrastructure;
using Parentaliza.Domain.Entidades;
using Parentaliza.Domain.Repository;

namespace Parentaliza.Infrastructure.Repository;

public class EventoAgendaRepository : Repository<EventoAgenda>, IEventoAgendaRepository
{
    public EventoAgendaRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<List<EventoAgenda>> ObterInformacoesAgendamento()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task<List<EventoAgenda>> ObterEventosPorData(DateTime data)
    {
        return await _dbSet
            .Where(e => e.Data.Date == data.Date)
            .ToListAsync();
    }
}

