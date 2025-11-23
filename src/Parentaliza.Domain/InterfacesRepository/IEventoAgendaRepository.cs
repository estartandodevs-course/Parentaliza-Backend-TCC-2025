using Parentaliza.Domain.Entidades;
namespace Parentaliza.Domain.InterfacesRepository;

public interface IEventoAgendaRepository : IRepository<EventoAgenda>
{
    // TODO: Método reservado para uso futuro - obter informações específicas do agendamento
    // Task<EventoAgenda> ObterInformacoesAgendamento();
    // Task<List<EventoAgenda>> ObterEventosPorData(DateTime data);
    Task<bool> NomeJaUtilizado(string? eventoAgenda);
}
