using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Queue.Domain.Interfaces;
using Queue.Persistence.Repository;

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
        services.AddScoped<IRecordStatusRepository, SqlRecordStatusRepository>();
        services.AddScoped<IWindowRepository, SqlWindowRepository>();
        services.AddScoped<IRecordRepository, SqlRecordRepository>();
        services.AddScoped<INotificationRepository, SqlNotificationRepository>();
        services.AddScoped<INotificationTypeRepository, SqlNotificationTypeRepository>();
        services.AddScoped<IServiceRepository, SqlServiceRepository>();


        return services;
    }
}
