using Microsoft.EntityFrameworkCore;
using Parentaliza.Infrastructure.Context;
using Parentaliza.Domain.Entidades;
using Parentaliza.Domain.InterfacesRepository;

namespace Parentaliza.Infrastructure.Repository;

public class TasksBebeGestacaoRepository : Repository<BebeGestacao>, IBebeGestacaoRepository
{

    public TasksBebeGestacaoRepository(ApplicationDbContext contexto) : base(contexto) { }

    public async Task<bool> NomeJaUtilizado(string? nome)
    {
        if (string.IsNullOrWhiteSpace(nome))
        {
            return false;
        }

        var existe = await _dbSet
            .AsNoTracking()
            .AnyAsync(b => b.Nome != null && b.Nome.ToLower() == nome.ToLower());

        return existe;
    }

    public async Task<BebeGestacao> ObterBebeGestacao(Guid bebeGestacaoId)
    {
        return await ObterPorId(bebeGestacaoId);
    }

    public async Task<List<BebeGestacao>> ObterPorResponsavelId(Guid responsavelId)
    {
        return await _dbSet
            .AsNoTracking()
            .Where(b => b.ResponsavelId == responsavelId)
            .ToListAsync();
    }
}