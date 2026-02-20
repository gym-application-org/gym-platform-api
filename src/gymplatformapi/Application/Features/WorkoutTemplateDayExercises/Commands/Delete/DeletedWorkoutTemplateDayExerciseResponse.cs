using Core.Application.Responses;

namespace Application.Features.WorkoutTemplateDayExercises.Commands.Delete;

public class DeletedWorkoutTemplateDayExerciseResponse : IResponse
{
    public int Id { get; set; }
}
