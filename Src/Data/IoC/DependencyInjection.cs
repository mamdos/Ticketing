using Data.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Data.IoC;

public static class DependencyInjection
{
    public static IServiceCollection AddDataServices(this IServiceCollection services)
    {
        services.AddDbContext<AppDbContext>(
            DbContextOptionsGenerator,
            ServiceLifetime.Scoped
        );
        services.AddDbContext<TransientAppDbContext>(
            DbContextOptionsGenerator,
            ServiceLifetime.Transient
        );


        return services;
    }

    private static void DbContextOptionsGenerator(IServiceProvider serviceProvider, DbContextOptionsBuilder options)
    {
        var configuration = serviceProvider.GetRequiredService<IConfiguration>();

        options.UseSqlServer(configuration.GetConnectionString("Main"));
    }
}
