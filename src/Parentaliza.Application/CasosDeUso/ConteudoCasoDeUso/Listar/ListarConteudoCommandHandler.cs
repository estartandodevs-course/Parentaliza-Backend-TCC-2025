using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Parentaliza.Application.Mediator;
using Parentaliza.Domain.InterfacesRepository;
using Parentaliza.Infrastructure.Context;
using System.Net;

namespace Parentaliza.Application.CasosDeUso.ConteudoCasoDeUso.Listar;

public class ListarConteudoCommandHandler : IRequestHandler<ListarConteudoCommand, CommandResponse<PagedResult<ListarConteudoCommandResponse>>>
{
    private readonly IConteudoRepository _conteudoRepository;
    private readonly ApplicationDbContext _context;
    private readonly ILogger<ListarConteudoCommandHandler> _logger;

    public ListarConteudoCommandHandler(
        IConteudoRepository conteudoRepository,
        ApplicationDbContext context,
        ILogger<ListarConteudoCommandHandler> logger)
    {
        _conteudoRepository = conteudoRepository;
        _context = context;
        _logger = logger;
    }

    public async Task<CommandResponse<PagedResult<ListarConteudoCommandResponse>>> Handle(ListarConteudoCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var query = _context.Set<Domain.Entidades.Conteudo>().AsNoTracking();

            // Aplicar filtros
            if (!string.IsNullOrWhiteSpace(request.Categoria))
            {
                query = query.Where(c => c.Categoria != null && c.Categoria.Contains(request.Categoria));
            }

            if (request.DataInicio.HasValue)
            {
                query = query.Where(c => c.DataPublicacao >= request.DataInicio.Value);
            }

            if (request.DataFim.HasValue)
            {
                query = query.Where(c => c.DataPublicacao <= request.DataFim.Value);
            }

            if (!string.IsNullOrWhiteSpace(request.Titulo))
            {
                query = query.Where(c => c.Titulo != null && c.Titulo.Contains(request.Titulo));
            }

            var totalCount = await query.CountAsync(cancellationToken);

            var sortBy = string.IsNullOrWhiteSpace(request.SortBy) ? "dataPublicacao" : request.SortBy.ToLower();
            var isAscending = request.SortOrder == "asc";

            query = sortBy switch
            {
                "titulo" => isAscending
                    ? query.OrderBy(c => c.Titulo).ThenByDescending(c => c.DataPublicacao)
                    : query.OrderByDescending(c => c.Titulo).ThenByDescending(c => c.DataPublicacao),
                "categoria" => isAscending
                    ? query.OrderBy(c => c.Categoria).ThenByDescending(c => c.DataPublicacao)
                    : query.OrderByDescending(c => c.Categoria).ThenByDescending(c => c.DataPublicacao),
                "dataPublicacao" or _ => isAscending
                    ? query.OrderBy(c => c.DataPublicacao)
                    : query.OrderByDescending(c => c.DataPublicacao)
            };

            var conteudos = await query
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync(cancellationToken);

            var items = conteudos.Select(conteudo => new ListarConteudoCommandResponse(
                conteudo.Id,
                conteudo.Titulo,
                conteudo.Categoria,
                conteudo.DataPublicacao,
                conteudo.Descricao
            )).ToList();

            var pagedResult = new PagedResult<ListarConteudoCommandResponse>(
                items,
                request.Page,
                request.PageSize,
                totalCount
            );

            return CommandResponse<PagedResult<ListarConteudoCommandResponse>>.Sucesso(pagedResult, HttpStatusCode.OK);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao listar conteúdos");
            return CommandResponse<PagedResult<ListarConteudoCommandResponse>>.ErroCritico(mensagem: $"Ocorreu um erro ao listar os conteúdos: {ex.Message}");
        }
    }
}