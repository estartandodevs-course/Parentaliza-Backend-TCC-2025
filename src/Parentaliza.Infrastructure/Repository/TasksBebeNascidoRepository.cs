using Microsoft.EntityFrameworkCore;
using Parentaliza.API.Infrastructure;
using Parentaliza.Domain.Entidades;
using Parentaliza.Domain.InterfacesRepository;

namespace Parentaliza.Infrastructure.Repository;

public class TasksBebeNascidoRepository : Repository<BebeNascido>, IBebeNascidoRepository
{
    public TasksBebeNascidoRepository(ApplicationDbContext contexto) : base(contexto) { }

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
}
