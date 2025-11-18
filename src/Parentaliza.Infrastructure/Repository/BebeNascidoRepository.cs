using Microsoft.EntityFrameworkCore;
using Parentaliza.API.Infrastructure;
using Parentaliza.Domain.Entidades;
using Parentaliza.Domain.Repository;

namespace Parentaliza.Infrastructure.Repository;

public class BebeNascidoRepository : Repository<BebeNascido>, IBebeNascidoRepository
{
    public BebeNascidoRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<bool> NomeJaUtilizado(string? nome)
    {
        if (string.IsNullOrWhiteSpace(nome))
            return false;

        return await _dbSet.AnyAsync(b => b.Nome != null && b.Nome.ToLower() == nome.ToLower());
    }
}

