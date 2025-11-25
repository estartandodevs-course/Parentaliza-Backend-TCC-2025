using Microsoft.EntityFrameworkCore;
using Parentaliza.Infrastructure.Context;
using Parentaliza.Domain.Entidades;
using Parentaliza.Domain.InterfacesRepository;

namespace Parentaliza.Infrastructure.Repository;

public class TasksControleLeiteMaternoRepository : Repository<ControleLeiteMaterno>, IControleLeiteMaternoRepository
{
    public TasksControleLeiteMaternoRepository(ApplicationDbContext contexto) : base(contexto) { }

    public async Task<List<ControleLeiteMaterno>> ObterControlesPorBebe(Guid bebeNascidoId)
    {
        return await _dbSet
            .AsNoTracking()
            .Where(c => c.BebeNascidoId == bebeNascidoId)
            .OrderByDescending(c => c.Cronometro)
            .ToListAsync();
    }
}
