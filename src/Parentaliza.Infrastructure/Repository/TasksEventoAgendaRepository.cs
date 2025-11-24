using Microsoft.EntityFrameworkCore;
using Parentaliza.Infrastructure.Context;
using Parentaliza.Domain.Entidades;
using Parentaliza.Domain.InterfacesRepository;

namespace Parentaliza.Infrastructure.Repository;

public class TasksEventoAgendaRepository : Repository<EventoAgenda>, IEventoAgendaRepository
{
    public TasksEventoAgendaRepository(ApplicationDbContext contexto) : base(contexto) { }

    public async Task<bool> NomeJaUtilizado(string? eventoAgenda)
    {
        if (string.IsNullOrWhiteSpace(eventoAgenda))
        {
            return false;
        }

        var existe = await _dbSet
            .AsNoTracking()
            .AnyAsync(e => e.Evento != null && e.Evento.ToLower() == eventoAgenda.ToLower());

        return existe;
    }

    public async Task<List<EventoAgenda>> ObterPorResponsavelId(Guid responsavelId)
    {
        return await _dbSet
            .AsNoTracking()
            .Where(e => e.ResponsavelId == responsavelId)
            .ToListAsync();
    }
}