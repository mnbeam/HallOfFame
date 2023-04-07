using System.Reflection;
using FluentValidation;
using HallOfFame.Core.Services.PersonService;
using Microsoft.Extensions.DependencyInjection;

namespace HallOfFame.Core;

public static class DependencyInjection
{
    public static IServiceCollection AddLogic(this IServiceCollection services)
    {
        services.AddScoped<IPersonService, PersonService>();
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        return services;
    }
}