using Parentaliza.Domain.Entidades;
namespace Parentaliza.Domain.InterfacesRepository;

public interface IEventoAgendaRepository : IRepository<EventoAgenda>
{
    Task<bool> NomeJaUtilizado(string? eventoAgenda);
    Task<List<EventoAgenda>> ObterPorResponsavelId(Guid responsavelId);
}
