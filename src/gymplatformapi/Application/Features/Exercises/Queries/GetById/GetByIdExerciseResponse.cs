using Core.Application.Responses;
using Domain.Enums;

namespace Application.Features.Exercises.Queries.GetById;

public class GetByIdExerciseResponse : IResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public string? MuscleGroup { get; set; }
    public string? Equipment { get; set; }
    public DifficultyLevel DifficultyLevel { get; set; }
    public string? VideoUrl { get; set; }
    public string? ThumbnailUrl { get; set; }
    public bool IsActive { get; set; }
}
