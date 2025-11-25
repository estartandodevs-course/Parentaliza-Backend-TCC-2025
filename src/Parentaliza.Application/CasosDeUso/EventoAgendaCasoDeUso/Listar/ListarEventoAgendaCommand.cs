using MediatR;
using Parentaliza.Application.Mediator;

namespace Parentaliza.Application.CasosDeUso.EventoAgendaCasoDeUso.Listar;

public class ListarEventoAgendaCommand : IRequest<CommandResponse<PagedResult<ListarEventoAgendaCommandResponse>>>
{
    public int Page { get; private set; }
    public int PageSize { get; private set; }
    public Guid? ResponsavelId { get; private set; }
    public DateTime? DataInicio { get; private set; }
    public DateTime? DataFim { get; private set; }
    public string? Especialidade { get; private set; }
    public string? Localizacao { get; private set; }
    public string? SortBy { get; private set; }
    public string SortOrder { get; private set; }

    public ListarEventoAgendaCommand(
        int page = 1,
        int pageSize = 10,
        Guid? responsavelId = null,
        DateTime? dataInicio = null,
        DateTime? dataFim = null,
        string? especialidade = null,
        string? localizacao = null,
        string? sortBy = null,
        string sortOrder = "desc")
    {
        Page = page < 1 ? 1 : page;
        PageSize = pageSize < 1 ? 10 : (pageSize > 100 ? 100 : pageSize);
        ResponsavelId = responsavelId;
        DataInicio = dataInicio;
        DataFim = dataFim;
        Especialidade = especialidade;
        Localizacao = localizacao;
        SortBy = sortBy;
        SortOrder = string.IsNullOrWhiteSpace(sortOrder) || 
                   (sortOrder.ToLower() != "asc" && sortOrder.ToLower() != "desc") 
                   ? "desc" 
                   : sortOrder.ToLower();
    }
}
