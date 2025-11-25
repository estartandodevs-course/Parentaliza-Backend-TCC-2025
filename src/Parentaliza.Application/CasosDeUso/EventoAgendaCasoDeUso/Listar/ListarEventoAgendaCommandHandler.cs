using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Parentaliza.Application.Mediator;
using Parentaliza.Domain.InterfacesRepository;
using Parentaliza.Infrastructure.Context;
using System.Net;

namespace Parentaliza.Application.CasosDeUso.EventoAgendaCasoDeUso.Listar;

public class ListarEventoAgendaCommandHandler : IRequestHandler<ListarEventoAgendaCommand, CommandResponse<PagedResult<ListarEventoAgendaCommandResponse>>>
{
    private readonly IEventoAgendaRepository _eventoAgendaRepository;
    private readonly ApplicationDbContext _context;
    private readonly ILogger<ListarEventoAgendaCommandHandler> _logger;

    public ListarEventoAgendaCommandHandler(
        IEventoAgendaRepository eventoAgendaRepository,
        ApplicationDbContext context,
        ILogger<ListarEventoAgendaCommandHandler> logger)
    {
        _eventoAgendaRepository = eventoAgendaRepository;
        _context = context;
        _logger = logger;
    }

    public async Task<CommandResponse<PagedResult<ListarEventoAgendaCommandResponse>>> Handle(ListarEventoAgendaCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var query = _context.Set<Domain.Entidades.EventoAgenda>().AsNoTracking();

            // Aplicar filtros
            if (request.ResponsavelId.HasValue && request.ResponsavelId.Value != Guid.Empty)
            {
                query = query.Where(e => e.ResponsavelId == request.ResponsavelId.Value);
            }

            if (request.DataInicio.HasValue)
            {
                query = query.Where(e => e.Data >= request.DataInicio.Value);
            }

            if (request.DataFim.HasValue)
            {
                query = query.Where(e => e.Data <= request.DataFim.Value);
            }

            if (!string.IsNullOrWhiteSpace(request.Especialidade))
            {
                query = query.Where(e => e.Especialidade != null && e.Especialidade.Contains(request.Especialidade));
            }

            if (!string.IsNullOrWhiteSpace(request.Localizacao))
            {
                query = query.Where(e => e.Localizacao != null && e.Localizacao.Contains(request.Localizacao));
            }

            // Contar total antes da paginação
            var totalCount = await query.CountAsync(cancellationToken);

            // Aplicar ordenação
            var sortBy = string.IsNullOrWhiteSpace(request.SortBy) ? "data" : request.SortBy.ToLower();
            var isAscending = request.SortOrder == "asc";

            query = sortBy switch
            {
                "evento" => isAscending
                    ? query.OrderBy(e => e.Evento).ThenByDescending(e => e.Data).ThenByDescending(e => e.Hora)
                    : query.OrderByDescending(e => e.Evento).ThenByDescending(e => e.Data).ThenByDescending(e => e.Hora),
                "especialidade" => isAscending
                    ? query.OrderBy(e => e.Especialidade).ThenByDescending(e => e.Data).ThenByDescending(e => e.Hora)
                    : query.OrderByDescending(e => e.Especialidade).ThenByDescending(e => e.Data).ThenByDescending(e => e.Hora),
                "data" or _ => isAscending
                    ? query.OrderBy(e => e.Data).ThenBy(e => e.Hora)
                    : query.OrderByDescending(e => e.Data).ThenByDescending(e => e.Hora)
            };

            // Aplicar paginação
            var eventos = await query
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync(cancellationToken);

            var items = eventos.Select(evento => new ListarEventoAgendaCommandResponse(
                evento.Id,
                evento.Evento,
                evento.Especialidade,
                evento.Localizacao,
                evento.Data,
                evento.Hora,
                evento.Anotacao
            )).ToList();

            var pagedResult = new PagedResult<ListarEventoAgendaCommandResponse>(
                items,
                request.Page,
                request.PageSize,
                totalCount
            );

            return CommandResponse<PagedResult<ListarEventoAgendaCommandResponse>>.Sucesso(pagedResult, HttpStatusCode.OK);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao listar eventos da agenda");
            return CommandResponse<PagedResult<ListarEventoAgendaCommandResponse>>.ErroCritico(mensagem: $"Ocorreu um erro ao listar os eventos da agenda: {ex.Message}");
        }
    }
}