using Microsoft.EntityFrameworkCore;
using Parentaliza.Infrastructure.Context;
using Parentaliza.Domain.Entidades;
using Parentaliza.Domain.InterfacesRepository;

namespace Parentaliza.Infrastructure.Repository;

public class TasksResponsavelRepository : Repository<Responsavel>, IResponsavelRepository
{
    public TasksResponsavelRepository(ApplicationDbContext contexto) : base(contexto) { }

    public async Task<bool> EmailJaUtilizado(string? email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            return false;
        }

        var existe = await _dbSet
            .AsNoTracking()
            .AnyAsync(r => r.Email != null && r.Email.ToLower() == email.ToLower());

        return existe;
    }
}
