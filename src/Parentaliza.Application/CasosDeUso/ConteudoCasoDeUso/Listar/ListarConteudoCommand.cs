using MediatR;
using Parentaliza.Application.Mediator;

namespace Parentaliza.Application.CasosDeUso.ConteudoCasoDeUso.Listar;

public class ListarConteudoCommand : IRequest<CommandResponse<PagedResult<ListarConteudoCommandResponse>>>
{
    public int Page { get; private set; }
    public int PageSize { get; private set; }
    public string? Categoria { get; private set; }
    public DateTime? DataInicio { get; private set; }
    public DateTime? DataFim { get; private set; }
    public string? Titulo { get; private set; }
    public string? SortBy { get; private set; }
    public string SortOrder { get; private set; }

    public ListarConteudoCommand(
        int page = 1,
        int pageSize = 10,
        string? categoria = null,
        DateTime? dataInicio = null,
        DateTime? dataFim = null,
        string? titulo = null,
        string? sortBy = null,
        string sortOrder = "desc")
    {
        Page = page < 1 ? 1 : page;
        PageSize = pageSize < 1 ? 10 : (pageSize > 100 ? 100 : pageSize);
        Categoria = categoria;
        DataInicio = dataInicio;
        DataFim = dataFim;
        Titulo = titulo;
        SortBy = sortBy;
        SortOrder = string.IsNullOrWhiteSpace(sortOrder) ||
                   (sortOrder.ToLower() != "asc" && sortOrder.ToLower() != "desc")
                   ? "desc"
                   : sortOrder.ToLower();
    }
}