using Microsoft.EntityFrameworkCore;

namespace Parentaliza.API.Infrastructure;

public class ParentalizaDbContext : DbContext
{
    public ParentalizaDbContext(DbContextOptions<ParentalizaDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        // Database models will be added here as needed
    }
}


