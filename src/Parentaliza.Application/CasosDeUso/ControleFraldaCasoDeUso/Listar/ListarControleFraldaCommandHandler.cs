using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Parentaliza.Application.Mediator;
using Parentaliza.Domain.InterfacesRepository;
using Parentaliza.Infrastructure.Context;
using System.Net;

namespace Parentaliza.Application.CasosDeUso.ControleFraldaCasoDeUso.Listar;

public class ListarControleFraldaCommandHandler : IRequestHandler<ListarControleFraldaCommand, CommandResponse<PagedResult<ListarControleFraldaCommandResponse>>>
{
    private readonly IControleFraldaRepository _controleFraldaRepository;
    private readonly ApplicationDbContext _context;
    private readonly ILogger<ListarControleFraldaCommandHandler> _logger;

    public ListarControleFraldaCommandHandler(
        IControleFraldaRepository controleFraldaRepository,
        ApplicationDbContext context,
        ILogger<ListarControleFraldaCommandHandler> logger)
    {
        _controleFraldaRepository = controleFraldaRepository;
        _context = context;
        _logger = logger;
    }

    public async Task<CommandResponse<PagedResult<ListarControleFraldaCommandResponse>>> Handle(ListarControleFraldaCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var query = _context.Set<Domain.Entidades.ControleFralda>().AsNoTracking();

            if (request.BebeNascidoId.HasValue && request.BebeNascidoId.Value != Guid.Empty)
            {
                query = query.Where(c => c.BebeNascidoId == request.BebeNascidoId.Value);
            }

            if (request.DataInicio.HasValue)
            {
                query = query.Where(c => c.HoraTroca >= request.DataInicio.Value);
            }

            if (request.DataFim.HasValue)
            {
                query = query.Where(c => c.HoraTroca <= request.DataFim.Value);
            }

            if (!string.IsNullOrWhiteSpace(request.TipoFralda))
            {
                query = query.Where(c => c.TipoFralda != null && c.TipoFralda.Contains(request.TipoFralda));
            }

            var totalCount = await query.CountAsync(cancellationToken);

            var sortBy = string.IsNullOrWhiteSpace(request.SortBy) ? "horaTroca" : request.SortBy.ToLower();
            var isAscending = request.SortOrder == "asc";

            query = sortBy switch
            {
                "tipofralda" => isAscending
                    ? query.OrderBy(c => c.TipoFralda).ThenByDescending(c => c.HoraTroca)
                    : query.OrderByDescending(c => c.TipoFralda).ThenByDescending(c => c.HoraTroca),
                "horaTroca" or _ => isAscending
                    ? query.OrderBy(c => c.HoraTroca)
                    : query.OrderByDescending(c => c.HoraTroca)
            };

            var controles = await query
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync(cancellationToken);

            var items = controles.Select(controle => new ListarControleFraldaCommandResponse(
                controle.Id,
                controle.BebeNascidoId,
                controle.HoraTroca,
                controle.TipoFralda,
                controle.Observacoes
            )).ToList();

            var pagedResult = new PagedResult<ListarControleFraldaCommandResponse>(
                items,
                request.Page,
                request.PageSize,
                totalCount
            );

            return CommandResponse<PagedResult<ListarControleFraldaCommandResponse>>.Sucesso(pagedResult, HttpStatusCode.OK);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao listar controles de fralda");
            return CommandResponse<PagedResult<ListarControleFraldaCommandResponse>>.ErroCritico(
                mensagem: $"Ocorreu um erro ao listar os controles de fralda: {ex.Message}");
        }
    }
}