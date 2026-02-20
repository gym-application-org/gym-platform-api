using Core.Application.Responses;

namespace Application.Features.WorkoutTemplateDays.Queries.GetById;

public class GetByIdWorkoutTemplateDayResponse : IResponse
{
    public int Id { get; set; }
    public int DayNumber { get; set; }
    public string Title { get; set; }
    public string? Notes { get; set; }
    public int WorkoutTemplateId { get; set; }
}
