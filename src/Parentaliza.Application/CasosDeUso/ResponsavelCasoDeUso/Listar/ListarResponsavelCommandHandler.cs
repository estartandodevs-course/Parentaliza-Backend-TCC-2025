using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Parentaliza.Application.Mediator;
using Parentaliza.Domain.InterfacesRepository;
using Parentaliza.Infrastructure.Context;
using System.Net;

namespace Parentaliza.Application.CasosDeUso.ResponsavelCasoDeUso.Listar;

public class ListarResponsavelCommandHandler : IRequestHandler<ListarResponsavelCommand, CommandResponse<PagedResult<ListarResponsavelCommandResponse>>>
{
    private readonly IResponsavelRepository _responsavelRepository;
    private readonly ApplicationDbContext _context;
    private readonly ILogger<ListarResponsavelCommandHandler> _logger;

    public ListarResponsavelCommandHandler(
        IResponsavelRepository responsavelRepository,
        ApplicationDbContext context,
        ILogger<ListarResponsavelCommandHandler> logger)
    {
        _responsavelRepository = responsavelRepository;
        _context = context;
        _logger = logger;
    }

    public async Task<CommandResponse<PagedResult<ListarResponsavelCommandResponse>>> Handle(ListarResponsavelCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var query = _context.Set<Domain.Entidades.Responsavel>().AsNoTracking();

            // Aplicar filtros
            if (!string.IsNullOrWhiteSpace(request.Nome))
            {
                query = query.Where(r => r.Nome != null && r.Nome.Contains(request.Nome));
            }

            if (!string.IsNullOrWhiteSpace(request.Email))
            {
                query = query.Where(r => r.Email != null && r.Email.Contains(request.Email));
            }

            if (request.TipoResponsavel.HasValue)
            {
                query = query.Where(r => r.TipoResponsavel == request.TipoResponsavel.Value);
            }

            // Contar total antes da paginação
            var totalCount = await query.CountAsync(cancellationToken);

            // Aplicar ordenação
            var sortBy = string.IsNullOrWhiteSpace(request.SortBy) ? "nome" : request.SortBy.ToLower();
            var isAscending = request.SortOrder == "asc";

            query = sortBy switch
            {
                "email" => isAscending
                    ? query.OrderBy(r => r.Email).ThenBy(r => r.Nome)
                    : query.OrderByDescending(r => r.Email).ThenBy(r => r.Nome),
                "nome" or _ => isAscending
                    ? query.OrderBy(r => r.Nome)
                    : query.OrderByDescending(r => r.Nome)
            };

            // Aplicar paginação
            var responsaveis = await query
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync(cancellationToken);

            var items = responsaveis.Select(responsavel => new ListarResponsavelCommandResponse(
                responsavel.Id,
                responsavel.Nome,
                responsavel.Email,
                responsavel.TipoResponsavel,
                responsavel.FaseNascimento
            )).ToList();

            var pagedResult = new PagedResult<ListarResponsavelCommandResponse>(
                items,
                request.Page,
                request.PageSize,
                totalCount
            );

            return CommandResponse<PagedResult<ListarResponsavelCommandResponse>>.Sucesso(pagedResult, HttpStatusCode.OK);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao listar responsáveis");
            return CommandResponse<PagedResult<ListarResponsavelCommandResponse>>.ErroCritico(
                mensagem: $"Ocorreu um erro ao listar os responsáveis: {ex.Message}");
        }
    }
}

