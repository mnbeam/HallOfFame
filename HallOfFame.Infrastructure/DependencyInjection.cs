using HallOfFame.Core.ExternalServices;
using HallOfFame.Infrastructure.DataBase;
using HallOfFame.Infrastructure.Logger;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HallOfFame.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration,
        string rootPath)
    {
        services.AddDbContext<AppDbContext>(optionsAction =>
            optionsAction.UseSqlServer(configuration.GetConnectionString("Default")));

        services.AddScoped<IAppDbContext, AppDbContext>(provider =>
            provider.GetService<AppDbContext>()!);

        services.AddSingleton(new FileLogger(rootPath));
        services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));

        return services;
    }
}