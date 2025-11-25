using Parentaliza.Infrastructure.Context;
using Parentaliza.Domain.Entidades;
using Parentaliza.Domain.InterfacesRepository;

namespace Parentaliza.Infrastructure.Repository;

public class TasksVacinaSusRepository : Repository<VacinaSus>, IVacinaSusRepository
{
    public TasksVacinaSusRepository(ApplicationDbContext contexto) : base(contexto) { }
}
