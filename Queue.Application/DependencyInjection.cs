using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Queue.Application.Common.Behaviors;
using Queue.Domain.Interfaces;
using System.Reflection;

namespace Queue.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddValidatorsFromAssemblies(new[] { Assembly.GetExecutingAssembly() });
        

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));


        return services;
    }
}
