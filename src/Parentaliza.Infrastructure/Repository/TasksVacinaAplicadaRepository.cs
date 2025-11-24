using Microsoft.EntityFrameworkCore;
using Parentaliza.Infrastructure.Context;
using Parentaliza.Domain.Entidades;
using Parentaliza.Domain.InterfacesRepository;

namespace Parentaliza.Infrastructure.Repository;

public class TasksVacinaAplicadaRepository : Repository<VacinaAplicada>, IVacinaAplicadaRepository
{
    public TasksVacinaAplicadaRepository(ApplicationDbContext contexto) : base(contexto) { }

    public async Task<List<VacinaAplicada>> ObterVacinasPorBebe(Guid bebeNascidoId)
    {
        return await _dbSet
            .AsNoTracking()
            .Where(v => v.BebeNascidoId == bebeNascidoId)
            .ToListAsync();
    }

    public async Task<VacinaAplicada?> ObterVacinaAplicadaPorBebeEVacina(Guid bebeNascidoId, Guid vacinaSusId)
    {
        return await _dbSet
            .FirstOrDefaultAsync(v => v.BebeNascidoId == bebeNascidoId && v.VacinaSusId == vacinaSusId);
    }
}

