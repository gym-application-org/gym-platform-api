using Core.Application.Dtos;

namespace Application.Features.WorkoutTemplateDays.Queries.GetList;

public class GetListWorkoutTemplateDayListItemDto : IDto
{
    public int Id { get; set; }
    public int DayNumber { get; set; }
    public string Title { get; set; }
    public string? Notes { get; set; }
    public int WorkoutTemplateId { get; set; }
}
