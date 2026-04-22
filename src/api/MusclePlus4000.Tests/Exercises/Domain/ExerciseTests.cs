using FluentAssertions;
using MusclePlus4000.Domain.Entities;
using Xunit;

namespace MusclePlus4000.Tests.Exercises.Domain;

public class ExerciseTests
{
    [Fact]
    public void CreateExercise_WithValidData_ShouldSucceed()
    {
        var result = Exercise.Create(
            "Push-Up",
            "A bodyweight exercise that targets the chest, shoulders, and triceps.",
            "TestUser"
        );

        result.IsSuccess.Should().BeTrue();
    }
    
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void CreateExercise_WithInvalidName_ShouldReturnValidationError(string? name)
    {
        var result = Exercise.Create(
            name!,
            "A bodyweight exercise that targets the chest, shoulders, and triceps.",
            "TestUser"
        );

        result.IsFailure.Should().BeTrue();
        result.Error?.Code.Should().Be("Validation.Exercise.Name");
    }
    
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void CreateExercise_WithInvalidDescription_ShouldReturnValidationError(string? description)
    {
        var result = Exercise.Create(
            "Push-Up",
            description!,
            "TestUser"
        );

        result.IsFailure.Should().BeTrue();
        result.Error?.Code.Should().Be("Validation.Exercise.Description");
    }
    
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void CreateExercise_WithInvalidCreatedBy_ShouldReturnValidationError(string? createdBy)
    {
        var result = Exercise.Create(
            "Push-Up",
            "A bodyweight exercise that targets the chest, shoulders, and triceps.",
            createdBy!
        );

        result.IsFailure.Should().BeTrue();
        result.Error?.Code.Should().Be("Validation.Exercise.CreatedBy");
    }

    [Fact]
    public void CreateExercise_WithValidData_ShouldSetAuditFields()
    {
        var result = Exercise.Create(
            "Push-Up",
            "A bodyweight exercise that targets the chest, shoulders, and triceps.",
            "TestUser"
        );

        result.IsSuccess.Should().BeTrue();

        var exercise = result.Value;

        exercise.CreatedBy.Should().Be("TestUser");
        exercise.CreatedAt.Should().NotBe(default);
    }
    
    [Fact]
    public void CreateExercise_WithInvalidData_ShouldThrowWhenAccessingValue()
    {
        var result = Exercise.Create(
            "",
            "A bodyweight exercise that targets the chest, shoulders, and triceps.",
            "TestUser"
        );

        result.IsFailure.Should().BeTrue();
        
        var act = () => { var _ = result.Value; };

        act.Should().Throw<InvalidOperationException>();
    }
}