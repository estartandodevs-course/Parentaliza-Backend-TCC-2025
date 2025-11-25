using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Parentaliza.Application.Mediator;
using Parentaliza.Domain.InterfacesRepository;
using Parentaliza.Infrastructure.Context;
using System.Net;

namespace Parentaliza.Application.CasosDeUso.ControleMamadeiraCasoDeUso.Listar;

public class ListarControleMamadeiraCommandHandler : IRequestHandler<ListarControleMamadeiraCommand, CommandResponse<PagedResult<ListarControleMamadeiraCommandResponse>>>
{
    private readonly IControleMamadeiraRepository _controleMamadeiraRepository;
    private readonly ApplicationDbContext _context;
    private readonly ILogger<ListarControleMamadeiraCommandHandler> _logger;

    public ListarControleMamadeiraCommandHandler(
        IControleMamadeiraRepository controleMamadeiraRepository,
        ApplicationDbContext context,
        ILogger<ListarControleMamadeiraCommandHandler> logger)
    {
        _controleMamadeiraRepository = controleMamadeiraRepository;
        _context = context;
        _logger = logger;
    }

    public async Task<CommandResponse<PagedResult<ListarControleMamadeiraCommandResponse>>> Handle(ListarControleMamadeiraCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var query = _context.Set<Domain.Entidades.ControleMamadeira>().AsNoTracking();

            // Aplicar filtros
            if (request.BebeNascidoId.HasValue && request.BebeNascidoId.Value != Guid.Empty)
            {
                query = query.Where(c => c.BebeNascidoId == request.BebeNascidoId.Value);
            }

            if (request.DataInicio.HasValue)
            {
                query = query.Where(c => c.Data >= request.DataInicio.Value);
            }

            if (request.DataFim.HasValue)
            {
                query = query.Where(c => c.Data <= request.DataFim.Value);
            }

            if (request.QuantidadeLeiteMin.HasValue)
            {
                query = query.Where(c => c.QuantidadeLeite.HasValue && c.QuantidadeLeite.Value >= request.QuantidadeLeiteMin.Value);
            }

            if (request.QuantidadeLeiteMax.HasValue)
            {
                query = query.Where(c => c.QuantidadeLeite.HasValue && c.QuantidadeLeite.Value <= request.QuantidadeLeiteMax.Value);
            }

            // Contar total antes da paginação
            var totalCount = await query.CountAsync(cancellationToken);

            // Aplicar ordenação
            var sortBy = string.IsNullOrWhiteSpace(request.SortBy) ? "data" : request.SortBy.ToLower();
            var isAscending = request.SortOrder == "asc";

            query = sortBy switch
            {
                "quantidadeleite" => isAscending
                    ? query.OrderBy(c => c.QuantidadeLeite).ThenByDescending(c => c.Data).ThenByDescending(c => c.Hora)
                    : query.OrderByDescending(c => c.QuantidadeLeite).ThenByDescending(c => c.Data).ThenByDescending(c => c.Hora),
                "data" or _ => isAscending
                    ? query.OrderBy(c => c.Data).ThenBy(c => c.Hora)
                    : query.OrderByDescending(c => c.Data).ThenByDescending(c => c.Hora)
            };

            // Aplicar paginação
            var controles = await query
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync(cancellationToken);

            var items = controles.Select(controle => new ListarControleMamadeiraCommandResponse(
                controle.Id,
                controle.BebeNascidoId,
                controle.Data,
                controle.Hora,
                controle.QuantidadeLeite,
                controle.Anotacao
            )).ToList();

            var pagedResult = new PagedResult<ListarControleMamadeiraCommandResponse>(
                items,
                request.Page,
                request.PageSize,
                totalCount
            );

            return CommandResponse<PagedResult<ListarControleMamadeiraCommandResponse>>.Sucesso(pagedResult, HttpStatusCode.OK);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao listar controles de mamadeira");
            return CommandResponse<PagedResult<ListarControleMamadeiraCommandResponse>>.ErroCritico(
                mensagem: $"Ocorreu um erro ao listar os controles de mamadeira: {ex.Message}");
        }
    }
}

