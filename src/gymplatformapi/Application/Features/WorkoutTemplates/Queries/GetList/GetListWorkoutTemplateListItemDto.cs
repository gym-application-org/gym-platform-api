using Core.Application.Dtos;
using Domain.Enums;

namespace Application.Features.WorkoutTemplates.Queries.GetList;

public class GetListWorkoutTemplateListItemDto : IDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public DifficultyLevel Level { get; set; }
    public bool IsActive { get; set; }
}
