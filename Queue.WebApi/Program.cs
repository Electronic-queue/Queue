using Microsoft.EntityFrameworkCore;
using Queue.Application;
using Queue.Application.Common.Mappings;
using Queue.Application.Interfaces;
using Queue.Persistence;
using Queue.Application.Common.Mappings;
using Queue.Persistence;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddAutoMapper(config =>
{
    config.AddProfile(new AssemblyMappingProfile(typeof(IQueuesDbContext).Assembly));
    config.AddProfile(new AssemblyMappingProfile(AppDomain.CurrentDomain.GetAssemblies()));
});


builder.Services.AddApplication();
builder.Services.AddPersistence(builder.Configuration);


builder.Services.AddDbContext<QueuesDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyHeader()
              .AllowAnyMethod()
              .AllowAnyOrigin();
    });
});


builder.Services.AddControllers();

var app = builder.Build();


using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    try
    {
        var context = serviceProvider.GetRequiredService<QueuesDbContext>();
        DbInitializer.Initialize(context);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Ошибка инициализации базы данных: {ex.Message}");
    }
}


if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseRouting();
app.MapControllers();

// Запуск приложения
app.Run();
