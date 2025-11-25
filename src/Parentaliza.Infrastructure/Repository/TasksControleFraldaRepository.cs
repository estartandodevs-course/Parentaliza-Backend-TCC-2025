using Microsoft.EntityFrameworkCore;
using Parentaliza.Infrastructure.Context;
using Parentaliza.Domain.Entidades;
using Parentaliza.Domain.InterfacesRepository;

namespace Parentaliza.Infrastructure.Repository;

public class TasksControleFraldaRepository : Repository<ControleFralda>, IControleFraldaRepository
{
    public TasksControleFraldaRepository(ApplicationDbContext contexto) : base(contexto) { }

    public async Task<List<ControleFralda>> ObterControlesPorBebe(Guid bebeNascidoId)
    {
        return await _dbSet
            .AsNoTracking()
            .Where(c => c.BebeNascidoId == bebeNascidoId)
            .OrderByDescending(c => c.HoraTroca)
            .ToListAsync();
    }
}
