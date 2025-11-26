using MediatR;
using Parentaliza.Application.Mediator;

namespace Parentaliza.Application.CasosDeUso.ControleFraldaCasoDeUso.Listar;

public class ListarControleFraldaCommand : IRequest<CommandResponse<PagedResult<ListarControleFraldaCommandResponse>>>
{
    public int Page { get; private set; }
    public int PageSize { get; private set; }
    public Guid? BebeNascidoId { get; private set; }
    public DateTime? DataInicio { get; private set; }
    public DateTime? DataFim { get; private set; }
    public string? TipoFralda { get; private set; }
    public string? SortBy { get; private set; }
    public string SortOrder { get; private set; }

    public ListarControleFraldaCommand(
        int page = 1,
        int pageSize = 10,
        Guid? bebeNascidoId = null,
        DateTime? dataInicio = null,
        DateTime? dataFim = null,
        string? tipoFralda = null,
        string? sortBy = null,
        string sortOrder = "desc")
    {
        Page = page < 1 ? 1 : page;
        PageSize = pageSize < 1 ? 10 : (pageSize > 100 ? 100 : pageSize);
        BebeNascidoId = bebeNascidoId;
        DataInicio = dataInicio;
        DataFim = dataFim;
        TipoFralda = tipoFralda;
        SortBy = sortBy;
        SortOrder = string.IsNullOrWhiteSpace(sortOrder) ||
                   (sortOrder.ToLower() != "asc" && sortOrder.ToLower() != "desc")
                   ? "desc"
                   : sortOrder.ToLower();
    }
}