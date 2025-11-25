using Microsoft.EntityFrameworkCore;
using Parentaliza.Infrastructure.Context;
using Parentaliza.Domain.Entidades;
using Parentaliza.Domain.InterfacesRepository;

namespace Parentaliza.Infrastructure.Repository;

public class TasksControleMamadeiraRepository : Repository<ControleMamadeira>, IControleMamadeiraRepository
{
    public TasksControleMamadeiraRepository(ApplicationDbContext contexto) : base(contexto) { }

    public async Task<List<ControleMamadeira>> ObterControlesPorBebe(Guid bebeNascidoId)
    {
        return await _dbSet
            .AsNoTracking()
            .Where(c => c.BebeNascidoId == bebeNascidoId)
            .OrderByDescending(c => c.Data)
            .ThenByDescending(c => c.Hora)
            .ToListAsync();
    }
}
