using Microsoft.EntityFrameworkCore;

namespace Data.Persistence.DbContexts;

public class TransientAppDbContext : AppDbContext
{
    public TransientAppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
}
