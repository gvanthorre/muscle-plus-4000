using Microsoft.EntityFrameworkCore;
using MusclePlus4000.Application.Abstractions.Persistence;
using MusclePlus4000.Application.Exercises.Queries.GetAllExercises;
using MusclePlus4000.Infrastructure.Persistence;
using MusclePlus4000.Infrastructure.Persistence.Repositories;

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

var builder = WebApplication.CreateBuilder(args);
var allowedOrigins = builder.Configuration
    .GetSection("Cors:AllowedOrigins")
    .Get<string[]>() ?? [];

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssemblyContaining<GetAllExercisesQuery>());

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

builder.Services.AddSingleton(TimeProvider.System);
builder.Services.AddScoped<AuditFieldsInterceptor>();
builder.Services.AddScoped<IExerciseReadRepository, ExerciseReadRepository>();

builder.Services.AddDbContext<WorkoutDbContext>((serviceProvider, dbContextOptions) =>
    dbContextOptions
        .UseNpgsql(builder.Configuration["ConnectionStrings:Default"],
            o => o
                .MigrationsAssembly("MusclePlus4000.Infrastructure")
                .MigrationsHistoryTable("__EFMigrationsHistory", "app"))
        .AddInterceptors(serviceProvider.GetRequiredService<AuditFieldsInterceptor>()));

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
