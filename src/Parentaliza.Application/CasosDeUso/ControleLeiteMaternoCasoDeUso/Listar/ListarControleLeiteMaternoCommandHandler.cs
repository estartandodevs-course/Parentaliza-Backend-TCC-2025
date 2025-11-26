using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Parentaliza.Application.Mediator;
using Parentaliza.Domain.InterfacesRepository;
using Parentaliza.Infrastructure.Context;
using System.Net;

namespace Parentaliza.Application.CasosDeUso.ControleLeiteMaternoCasoDeUso.Listar;

public class ListarControleLeiteMaternoCommandHandler : IRequestHandler<ListarControleLeiteMaternoCommand, CommandResponse<PagedResult<ListarControleLeiteMaternoCommandResponse>>>
{
    private readonly IControleLeiteMaternoRepository _controleLeiteMaternoRepository;
    private readonly ApplicationDbContext _context;
    private readonly ILogger<ListarControleLeiteMaternoCommandHandler> _logger;

    public ListarControleLeiteMaternoCommandHandler(
        IControleLeiteMaternoRepository controleLeiteMaternoRepository,
        ApplicationDbContext context,
        ILogger<ListarControleLeiteMaternoCommandHandler> logger)
    {
        _controleLeiteMaternoRepository = controleLeiteMaternoRepository;
        _context = context;
        _logger = logger;
    }

    public async Task<CommandResponse<PagedResult<ListarControleLeiteMaternoCommandResponse>>> Handle(ListarControleLeiteMaternoCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var query = _context.Set<Domain.Entidades.ControleLeiteMaterno>().AsNoTracking();

            // Aplicar filtros
            if (request.BebeNascidoId.HasValue && request.BebeNascidoId.Value != Guid.Empty)
            {
                query = query.Where(c => c.BebeNascidoId == request.BebeNascidoId.Value);
            }

            if (request.DataInicio.HasValue)
            {
                query = query.Where(c => c.Cronometro >= request.DataInicio.Value);
            }

            if (request.DataFim.HasValue)
            {
                query = query.Where(c => c.Cronometro <= request.DataFim.Value);
            }

            var totalCount = await query.CountAsync(cancellationToken);

            var sortBy = string.IsNullOrWhiteSpace(request.SortBy) ? "cronometro" : request.SortBy.ToLower();
            var isAscending = request.SortOrder == "asc";

            query = sortBy switch
            {
                "cronometro" or _ => isAscending
                    ? query.OrderBy(c => c.Cronometro)
                    : query.OrderByDescending(c => c.Cronometro)
            };

            var controles = await query
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync(cancellationToken);

            var items = controles.Select(controle => new ListarControleLeiteMaternoCommandResponse(
                controle.Id,
                controle.BebeNascidoId,
                controle.Cronometro,
                controle.LadoDireito,
                controle.LadoEsquerdo
            )).ToList();

            var pagedResult = new PagedResult<ListarControleLeiteMaternoCommandResponse>(
                items,
                request.Page,
                request.PageSize,
                totalCount
            );

            return CommandResponse<PagedResult<ListarControleLeiteMaternoCommandResponse>>.Sucesso(pagedResult, HttpStatusCode.OK);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao listar controles de leite materno");
            return CommandResponse<PagedResult<ListarControleLeiteMaternoCommandResponse>>.ErroCritico(
                mensagem: $"Ocorreu um erro ao listar os controles de leite materno: {ex.Message}");
        }
    }
}