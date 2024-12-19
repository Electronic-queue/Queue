using Microsoft.EntityFrameworkCore;
using Queue.Application;
using Queue.Application.Common.Mappings;
using Queue.Application.Interfaces;
using Queue.Persistence;
using Queue.WebApi.Common;
using Queue.WebApi.Middleware;
using Queue.WebApi.Services;
using Serilog;
using Serilog.Events;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
    .WriteTo.File("UsersWebAppLog-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog();
builder.Services.AddPersistence(builder.Configuration);

builder.Services.AddAutoMapper(config =>
{
   
    config.AddProfile(new AssemblyMappingProfile(AppDomain.CurrentDomain.GetAssemblies()));
    config.AddProfile(typeof(UserProfile));
});

builder.Services.AddApplication();
/*builder.Services.AddPersistence(builder.Configuration);

builder.Services.AddDbContext<QueuesDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
*/
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyHeader()
              .AllowAnyMethod()
              .AllowAnyOrigin();
    });
});

builder.Services.AddSingleton<ICurrentUserService, CurrentUserService>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen(config =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    config.IncludeXmlComments(xmlPath);
});
        

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    try
    {
/*        var context = serviceProvider.GetRequiredService<QueuesDbContext>();
        DbInitializer.Initialize(context);*/
    }
    catch (Exception ex)
    {
        Log.Fatal(ex, "An error occurred while initializing the database");
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}


app.UseSwagger();
app.UseSwaggerUI(config =>
{
    config.RoutePrefix = string.Empty;
    config.SwaggerEndpoint("swagger/v1/swagger.json", "Users API");
});
app.UseCustomExceptionHandler();
app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseRouting();
app.MapControllers();

app.Run();
