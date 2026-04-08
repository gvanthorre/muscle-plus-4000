using Microsoft.EntityFrameworkCore;
using MusclePlus4000.Infrastructure.Persistence;

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

var builder = WebApplication.CreateBuilder(args);
var allowedOrigins = builder.Configuration
    .GetSection("Cors:AllowedOrigins")
    .Get<string[]>() ?? [];

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins(allowedOrigins)
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

builder.Services.AddDbContext<WorkoutDbContext>(dbContextOptions =>
    dbContextOptions.UseNpgsql(builder.Configuration["ConnectionStrings:Default"],
        o => o.MigrationsHistoryTable("__EFMigrationsHistory", "app")));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<WorkoutDbContext>();
    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
    try
    {
        await dbContext.Database.OpenConnectionAsync();
        await dbContext.Database.CloseConnectionAsync();
        logger.LogInformation("Database connection successful");
    }
    catch (Exception)
    {
        logger.LogError("Database connection failed");
        throw;
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
