using Microsoft.EntityFrameworkCore;
using Parentaliza.Domain.Entidades;
using Parentaliza.Domain.Repository;

namespace Parentaliza.API.Infrastructure;

public class OrderRepository : IOrderRepository
{
    private readonly ParentalizaDbContext _dbContext;

    public OrderRepository(ParentalizaDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<OrderEntity>> GetAll(CancellationToken cancellationToken)
    {
        return await _dbContext.Orders.AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task<OrderEntity?> GetById(string id, CancellationToken cancellationToken)
    {
        return await _dbContext.Orders.AsNoTracking().FirstOrDefaultAsync(o => o.Id == id, cancellationToken);
    }

    public async Task Add(OrderEntity order, CancellationToken cancellationToken)
    {
        await _dbContext.Orders.AddAsync(order, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task Update(OrderEntity order, CancellationToken cancellationToken)
    {
        _dbContext.Orders.Update(order);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task Delete(string id, CancellationToken cancellationToken)
    {
        var entity = await _dbContext.Orders.FirstOrDefaultAsync(o => o.Id == id, cancellationToken);
        if (entity != null)
        {
            _dbContext.Orders.Remove(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }

    public async Task<int> GetTotalCount(CancellationToken cancellationToken)
    {
        return await _dbContext.Orders.CountAsync(cancellationToken);
    }

    public async Task<decimal> GetTotalRevenue(CancellationToken cancellationToken)
    {
        return await _dbContext.Orders.SumAsync(o => o.TotalAmount, cancellationToken);
    }
}


