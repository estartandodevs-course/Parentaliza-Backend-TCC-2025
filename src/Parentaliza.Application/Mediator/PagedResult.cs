namespace Parentaliza.Application.Mediator;

/// <summary>
/// Classe genérica para resultados paginados
/// </summary>
/// <typeparam name="T">Tipo dos itens na lista</typeparam>
public class PagedResult<T>
{
    public List<T> Items { get; private set; }
    public int Page { get; private set; }
    public int PageSize { get; private set; }
    public int TotalCount { get; private set; }
    public int TotalPages { get; private set; }
    public bool HasNext { get; private set; }
    public bool HasPrevious { get; private set; }

    public PagedResult(List<T> items, int page, int pageSize, int totalCount)
    {
        Items = items;
        Page = page;
        PageSize = pageSize;
        TotalCount = totalCount;
        TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
        HasNext = page < TotalPages;
        HasPrevious = page > 1;
    }
}

