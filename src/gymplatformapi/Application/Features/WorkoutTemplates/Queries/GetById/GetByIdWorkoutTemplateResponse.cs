using Application.Features.WorkoutTemplates.Queries.GetById.Dtos;
using Core.Application.Responses;
using Domain.Enums;

namespace Application.Features.WorkoutTemplates.Queries.GetById;

public class GetByIdWorkoutTemplateResponse : IResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public DifficultyLevel Level { get; set; }
    public bool IsActive { get; set; }

    public ICollection<GetByIdWorkoutTemplateDayDto> Days { get; set; } = new List<GetByIdWorkoutTemplateDayDto>();
}
