using Microsoft.Extensions.DependencyInjection;
using MusclePlus4000.Application.Exercises.Queries.GetAllExercises;

namespace MusclePlus4000.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssemblyContaining<GetAllExercisesQuery>());

        return services;
    }
}

