using Serilog;
using TygrLabs.Application.IServices;
using TygrLabs.Application.Services;
using TygrLabs.Domain.Repositories;
using TygrLabs.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Loger configuration
var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();

try
{
    // Add services to the container.

    // Configure serilog logger globally.
    builder.Logging.ClearProviders();
    builder.Logging.AddSerilog(logger);

    logger.Information("Starting web application");

    // Register user repository and service
    builder.Services.AddSingleton<IUserRepository, UserRepository>();
    builder.Services.AddSingleton<IUserService, UserService>();

    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();

}
catch (Exception ex)
{
    logger.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}

