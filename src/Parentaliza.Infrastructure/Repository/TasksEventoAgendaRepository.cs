using Microsoft.EntityFrameworkCore;
using Parentaliza.API.Infrastructure;
using Parentaliza.Domain.Entidades;
using Parentaliza.Domain.InterfacesRepository;

namespace Parentaliza.Infrastructure.Repository;

public class TasksEventoAgendaRepository : Repository<EventoAgenda>, IEventoAgendaRepository
{
    public TasksEventoAgendaRepository(ApplicationDbContext contexto) : base(contexto) { }

    // TODO: Método reservado para uso futuro - obter informações específicas do agendamento
    // public async Task<EventoAgenda> ObterInformacoesAgendamento()
    // {
    //     var response = await _contexto.EventosAgendas
    //         .AsNoTracking()
    //         .FirstOrDefaultAsync();
    //
    //     return response;
    // }

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
}