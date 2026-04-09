namespace MusclePlus4000.Domain.Common;

public record Error(string Code, string Description)
{
    public static readonly Error None = new(string.Empty, string.Empty);

    public static Error Validation(string code, string description) =>
        new($"Validation.{code}", description);

    public static Error NotFound(string code, string description) =>
        new($"NotFound.{code}", description);

    public static Error Conflict(string code, string description) =>
        new($"Conflict.{code}", description);
}

