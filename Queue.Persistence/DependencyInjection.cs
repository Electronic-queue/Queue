using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Queue.Domain.Interfaces;

namespace Queue.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection
        services,IConfiguration configuration)
    {
        var connectionString = configuration["DbConnection:"];
        services.AddDbContext<QueuesDbContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });
        services.AddScoped<IUserRepository, SqlUserRepository>();
        services.AddScoped<IWindowRepository, SqlWindowRepository>();
        services.AddScoped<INotificationTypeRepository, SqlNotificationTypeRepository>();
        services.AddScoped<INotificationRepository, SqlNotificationRepository>();
        return services;
    }
}
