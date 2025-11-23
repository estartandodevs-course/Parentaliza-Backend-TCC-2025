using Parentaliza.API.Infrastructure;
using Parentaliza.Domain.Entidades;
using Parentaliza.Domain.InterfacesRepository;

namespace Parentaliza.Infrastructure.Repository;

public class TasksBebeGestacaoRepository : Repository<BebeGestacao>, IBebeGestacaoRepository
{

    public TasksBebeGestacaoRepository(ApplicationDbContext context) : base(context) { }

    public Task<bool> NomeJaUtilizado(string? nome)
    {
        throw new NotImplementedException();
    }
    public Task<BebeGestacao> ObterBebeGestacao(Guid bebeGestacaoId)
    {
        throw new NotImplementedException();
    }
}