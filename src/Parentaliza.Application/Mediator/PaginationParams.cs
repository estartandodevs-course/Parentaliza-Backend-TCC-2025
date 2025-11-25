namespace Parentaliza.Application.Mediator;

/// <summary>
/// Parâmetros de paginação padrão
/// </summary>
public class PaginationParams
{
    private const int MaxPageSize = 100;
    private const int DefaultPageSize = 10;
    private const int DefaultPage = 1;

    private int _page = DefaultPage;
    private int _pageSize = DefaultPageSize;

    /// <summary>
    /// Número da página (começa em 1)
    /// </summary>
    public int Page
    {
        get => _page;
        set => _page = value < 1 ? DefaultPage : value;
    }

    /// <summary>
    /// Quantidade de itens por página (máximo 100)
    /// </summary>
    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = value < 1 ? DefaultPageSize : (value > MaxPageSize ? MaxPageSize : value);
    }

    /// <summary>
    /// Calcula o número de itens a pular (skip)
    /// </summary>
    public int Skip => (Page - 1) * PageSize;

    /// <summary>
    /// Calcula o número de itens a tomar (take)
    /// </summary>
    public int Take => PageSize;
}

