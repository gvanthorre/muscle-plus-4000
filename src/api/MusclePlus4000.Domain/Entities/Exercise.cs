using MusclePlus4000.Domain.Common;

namespace MusclePlus4000.Domain.Entities;

public sealed class Exercise : AuditableEntity
{
    public string Name { get; private set; }
    public string Description { get; private set; }

    private Exercise(string name, string description)
    {
        Name = name;
        Description = description;
    }

    // Required by EF Core to materialize entities from the database
    private Exercise()
    {
        Name = string.Empty;
        Description = string.Empty;
    }

    public static Result<Exercise> Create(string name, string description, string createdBy)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Error.Validation("Exercise.Name", "Name cannot be empty.");

        if (string.IsNullOrWhiteSpace(description))
            return Error.Validation("Exercise.Description", "Description cannot be empty.");

        if (string.IsNullOrWhiteSpace(createdBy))
            return Error.Validation("Exercise.CreatedBy", "CreatedBy cannot be empty.");

        var exercise = new Exercise(name, description);
        exercise.SetCreated(createdBy);
        return exercise;
    }
}