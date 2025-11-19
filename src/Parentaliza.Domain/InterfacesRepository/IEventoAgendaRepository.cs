using Parentaliza.Domain.Entidades;
namespace Parentaliza.Domain.InterfacesRepository;

public interface IEventoAgendaRepository : IRepository<EventoAgenda>
{
    Task<EventoAgenda> ObterInformacoesAgendamento();
    //Task<List<EventoAgenda>> ObterEventosPorData(DateTime data);
    Task<bool> NomeJaUtilizado(string? eventoAgenda);
}
