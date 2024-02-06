using Data.Entities.User.Aggregate;
using Data.Persistence.DbContexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Data.Common.IoC;

public static class DependencyInjection
{
    public static IServiceCollection AddDataLayer(this IServiceCollection services)
    {
        services.AddDbContext<AppDbContext>(
            DbContextOptionsGenerator,
            ServiceLifetime.Scoped
        );
        services.AddDbContext<TransientAppDbContext>(
            DbContextOptionsGenerator,
            ServiceLifetime.Transient
        );

        services.AddIdentityCore<User>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<AppDbContext>();

        return services;
    }

    private static void DbContextOptionsGenerator(IServiceProvider serviceProvider, DbContextOptionsBuilder options)
    {
        var configuration = serviceProvider.GetRequiredService<IConfiguration>();

        options.UseSqlServer(configuration.GetConnectionString("Main"));
    }
}
