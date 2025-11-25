using Microsoft.EntityFrameworkCore;
using Parentaliza.Infrastructure.Context;
using Parentaliza.Domain.Entidades;
using Parentaliza.Domain.InterfacesRepository;

namespace Parentaliza.Infrastructure.Repository;

public class TasksConteudoRepository : Repository<Conteudo>, IConteudoRepository
{
    public TasksConteudoRepository(ApplicationDbContext contexto) : base(contexto){}

    public async Task<bool> NomeJaUtilizado(string? titulo)
    {
        if (string.IsNullOrWhiteSpace(titulo))
        {
            return false;
        }

        var existe = await _dbSet
            .AsNoTracking()
            .AnyAsync(t => t.Titulo != null && t.Titulo.Equals(titulo, StringComparison.OrdinalIgnoreCase));

        return existe;
    }
}