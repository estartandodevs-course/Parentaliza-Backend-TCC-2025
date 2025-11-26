namespace Parentaliza.Application.Mediator;

/// <summary>
/// Parâmetros de ordenação
/// </summary>
public class SortParams
{
    private const string DefaultSortOrder = "desc";

    private string _sortOrder = DefaultSortOrder;

    /// <summary>
    /// Campo para ordenar (ex: "horaTroca", "nome", "data")
    /// </summary>
    public string? SortBy { get; set; }

    /// <summary>
    /// Direção da ordenação: "asc" ou "desc" (padrão: "desc")
    /// </summary>
    public string SortOrder
    {
        get => _sortOrder;
        set => _sortOrder = string.IsNullOrWhiteSpace(value) ||
                           (value.ToLower() != "asc" && value.ToLower() != "desc")
                           ? DefaultSortOrder
                           : value.ToLower();
    }

    /// <summary>
    /// Verifica se a ordenação é ascendente
    /// </summary>
    public bool IsAscending => SortOrder == "asc";
}