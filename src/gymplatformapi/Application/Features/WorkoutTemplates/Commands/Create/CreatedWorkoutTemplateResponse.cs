using Core.Application.Responses;
using Domain.Enums;

namespace Application.Features.WorkoutTemplates.Commands.Create;

public class CreatedWorkoutTemplateResponse : IResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public DifficultyLevel Level { get; set; }
    public bool IsActive { get; set; }
}
