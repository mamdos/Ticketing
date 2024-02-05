using Microsoft.Extensions.DependencyInjection;
using Services.Ticket;

namespace Services.Common.IoC;

public static class DependencyInjection
{
    public static IServiceCollection AddServicesLayer(this IServiceCollection services)
    {
        services.AddScoped<ITicketManager, TicketManager>();

        return services;
    }
}
