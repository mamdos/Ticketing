using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Services.Category;
using Services.Common.Models;
using Services.Ticket;
using Services.User;

namespace Services.Common.IoC;

public static class DependencyInjection
{
    public static IServiceCollection AddServicesLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ITicketManager, TicketManager>();
        services.AddScoped<ICategoryManager, CategoryManager>();
        services.AddScoped<IUserManager, UserManager>();
        services.AddScoped<ISignInManager, SignInManager>();
        services.AddTransient<IJwtTokenGenerator, JwtTokenGenerator>();

        return services;
    }
}
