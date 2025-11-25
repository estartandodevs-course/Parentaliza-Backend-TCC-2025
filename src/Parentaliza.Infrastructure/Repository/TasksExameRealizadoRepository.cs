using Microsoft.EntityFrameworkCore;
using Parentaliza.Infrastructure.Context;
using Parentaliza.Domain.Entidades;
using Parentaliza.Domain.InterfacesRepository;

namespace Parentaliza.Infrastructure.Repository;

public class TasksExameRealizadoRepository : Repository<ExameRealizado>, IExameRealizadoRepository
{
    public TasksExameRealizadoRepository(ApplicationDbContext contexto) : base(contexto) { }

    public async Task<List<ExameRealizado>> ObterExamesPorBebe(Guid bebeNascidoId)
    {
        return await _dbSet
            .AsNoTracking()
            .Where(e => e.BebeNascidoId == bebeNascidoId)
            .ToListAsync();
    }

    public async Task<ExameRealizado?> ObterExameRealizadoPorBebeEExame(Guid bebeNascidoId, Guid exameSusId)
    {
        return await _dbSet
            .FirstOrDefaultAsync(e => e.BebeNascidoId == bebeNascidoId && e.ExameSusId == exameSusId);
    }
}

