using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Queue.Application;
using Queue.Application.Common.Mappings;
using Queue.Application.Interfaces;
using Queue.Persistence;
using Queue.WebApi;
using Queue.WebApi.Common.ExceedingsTimeProfile;
using Queue.WebApi.Common.NotificationProfile;
using Queue.WebApi.Common.NotificationTypeProfile;
using Queue.WebApi.Common.QueueTypeProfile;
using Queue.WebApi.Common.ReasonsForCancellationProfile;
using Queue.WebApi.Common.RecordProfile;
using Queue.WebApi.Common.RecordStatusProfile;
using Queue.WebApi.Common.ReviewProfile;
using Queue.WebApi.Common.ServiceProfile;
using Queue.WebApi.Common.UserServiceProfile;
using Queue.WebApi.Common.UserWindowProfile;
using Queue.WebApi.Common.WindowProfile;
using Queue.WebApi.Common.WindowStatusProfile;
using Queue.WebApi.Middleware;
using Queue.WebApi.Services;
using Serilog;
using Serilog.Events;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug() 
            .MinimumLevel.Override("Microsoft", LogEventLevel.Warning) 
            .Enrich.FromLogContext()
            .WriteTo.Console() 
            .WriteTo.Seq("http://localhost:5341") 
            .CreateLogger();

builder.Host.UseSerilog();
builder.Services.AddPersistence(builder.Configuration);

builder.Services.AddAutoMapper(config =>
{
   
    config.AddProfile(new AssemblyMappingProfile(AppDomain.CurrentDomain.GetAssemblies()));
    config.AddProfile(typeof(WindowProfile));
    config.AddProfile(typeof(RecordStatusProfile));
    config.AddProfile(typeof(RecordProfile));
    config.AddProfile(typeof(NotificationTypeProfile));
    config.AddProfile(typeof(NotificationProfile));
    config.AddProfile(typeof(ServiceProfile));
    config.AddProfile(typeof(WindowStatusProfile));
    config.AddProfile(typeof(QueueTypeProfile));
    config.AddProfile(typeof(ReviewProfile));
    config.AddProfile(typeof(UserServiceProfile));
    config.AddProfile(typeof(UserWindowProfile));
    config.AddProfile(typeof(ReasonsForCancellationProfile));
    config.AddProfile(typeof(ExceedingsTimeProfile));
});

builder.Services.AddApplication();


//builder.Services.AddDbContext<QueuesDbContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

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
builder.Services.AddVersionedApiExplorer(options =>
                options.GroupNameFormat = "'v'VVV");
builder.Services.AddScoped<IConfigureOptions<SwaggerGenOptions>,
        ConfigureSwaggerOptions>();
builder.Services.AddApiVersioning();

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
app.UseMiddleware<CustomExceptionHandlerMiddleware>();

app.UseMiddleware<CorrelationIdMiddleware>();
app.UseSwagger();
app.UseSwaggerUI(config =>
{
    var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
    

    foreach (var description in provider.ApiVersionDescriptions)
    {
        config.SwaggerEndpoint(
                $"/swagger/{description.GroupName}/swagger.json",
                 description.GroupName.ToUpperInvariant());
        config.RoutePrefix = string.Empty;

    }
});
app.UseCustomExceptionHandler();
app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseRouting();
app.UseApiVersioning();
app.MapControllers();

Log.Information("Приложение запущено");
app.Run();