using Core.Application.Responses;

namespace Application.Features.WorkoutTemplateDays.Commands.Delete;

public class DeletedWorkoutTemplateDayResponse : IResponse
{
    public int Id { get; set; }
}
