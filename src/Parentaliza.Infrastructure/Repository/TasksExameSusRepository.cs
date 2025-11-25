using Parentaliza.Infrastructure.Context;
using Parentaliza.Domain.Entidades;
using Parentaliza.Domain.InterfacesRepository;

namespace Parentaliza.Infrastructure.Repository;

public class TasksExameSusRepository : Repository<ExameSus>, IExameSusRepository
{
    public TasksExameSusRepository(ApplicationDbContext contexto) : base(contexto) { }
}
