using MediatR;
using Parentaliza.Application.Mediator;
using Parentaliza.Domain.Enums;

namespace Parentaliza.Application.CasosDeUso.ResponsavelCasoDeUso.Listar;

public class ListarResponsavelCommand : IRequest<CommandResponse<PagedResult<ListarResponsavelCommandResponse>>>
{
    public int Page { get; private set; }
    public int PageSize { get; private set; }
    public string? Nome { get; private set; }
    public string? Email { get; private set; }
    public TiposEnum? TipoResponsavel { get; private set; }
    public string? SortBy { get; private set; }
    public string SortOrder { get; private set; }

    public ListarResponsavelCommand(
        int page = 1,
        int pageSize = 10,
        string? nome = null,
        string? email = null,
        TiposEnum? tipoResponsavel = null,
        string? sortBy = null,
        string sortOrder = "asc")
    {
        Page = page < 1 ? 1 : page;
        PageSize = pageSize < 1 ? 10 : (pageSize > 100 ? 100 : pageSize);
        Nome = nome;
        Email = email;
        TipoResponsavel = tipoResponsavel;
        SortBy = sortBy;
        SortOrder = string.IsNullOrWhiteSpace(sortOrder) || 
                   (sortOrder.ToLower() != "asc" && sortOrder.ToLower() != "desc") 
                   ? "asc" 
                   : sortOrder.ToLower();
    }
}

