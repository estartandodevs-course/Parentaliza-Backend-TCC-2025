using Parentaliza.Domain.Entidades;

namespace Parentaliza.Domain.InterfacesRepository;

public interface IResponsavelRepository : IRepository<Responsavel>
{
    Task<bool> EmailJaUtilizado(string? email);
}
