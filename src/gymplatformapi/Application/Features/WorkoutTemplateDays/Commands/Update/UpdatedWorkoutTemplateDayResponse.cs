using Core.Application.Responses;

namespace Application.Features.WorkoutTemplateDays.Commands.Update;

public class UpdatedWorkoutTemplateDayResponse : IResponse
{
    public int Id { get; set; }
    public int DayNumber { get; set; }
    public string Title { get; set; }
    public string? Notes { get; set; }
    public int WorkoutTemplateId { get; set; }
}
