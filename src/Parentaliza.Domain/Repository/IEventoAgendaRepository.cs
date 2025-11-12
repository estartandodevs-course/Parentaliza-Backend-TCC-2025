using Parentaliza.Domain.Entidades;
namespace Parentaliza.Domain.Repository;

public interface IEventoAgendaRepository : IRepository<EventoAgenda>
{
    Task<List<EventoAgenda>> ObterInformacoesAgendamento();
    Task<List<EventoAgenda>> ObterEventosPorData(DateTime data);
}
