using MediatR;
using Parentaliza.Application.Mediator;

namespace Parentaliza.Application.CasosDeUso.ControleMamadeiraCasoDeUso.Listar;

public class ListarControleMamadeiraCommand : IRequest<CommandResponse<PagedResult<ListarControleMamadeiraCommandResponse>>>
{
    public int Page { get; private set; }
    public int PageSize { get; private set; }
    public Guid? BebeNascidoId { get; private set; }
    public DateTime? DataInicio { get; private set; }
    public DateTime? DataFim { get; private set; }
    public decimal? QuantidadeLeiteMin { get; private set; }
    public decimal? QuantidadeLeiteMax { get; private set; }
    public string? SortBy { get; private set; }
    public string SortOrder { get; private set; }

    public ListarControleMamadeiraCommand(
        int page = 1,
        int pageSize = 10,
        Guid? bebeNascidoId = null,
        DateTime? dataInicio = null,
        DateTime? dataFim = null,
        decimal? quantidadeLeiteMin = null,
        decimal? quantidadeLeiteMax = null,
        string? sortBy = null,
        string sortOrder = "desc")
    {
        Page = page < 1 ? 1 : page;
        PageSize = pageSize < 1 ? 10 : (pageSize > 100 ? 100 : pageSize);
        BebeNascidoId = bebeNascidoId;
        DataInicio = dataInicio;
        DataFim = dataFim;
        QuantidadeLeiteMin = quantidadeLeiteMin;
        QuantidadeLeiteMax = quantidadeLeiteMax;
        SortBy = sortBy;
        SortOrder = string.IsNullOrWhiteSpace(sortOrder) || 
                   (sortOrder.ToLower() != "asc" && sortOrder.ToLower() != "desc") 
                   ? "desc" 
                   : sortOrder.ToLower();
    }
}

