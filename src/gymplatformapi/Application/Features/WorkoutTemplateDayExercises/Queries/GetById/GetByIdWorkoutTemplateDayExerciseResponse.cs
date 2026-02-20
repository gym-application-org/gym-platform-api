using Core.Application.Responses;

namespace Application.Features.WorkoutTemplateDayExercises.Queries.GetById;

public class GetByIdWorkoutTemplateDayExerciseResponse : IResponse
{
    public int Id { get; set; }
    public int Order { get; set; }
    public int Sets { get; set; }
    public string Reps { get; set; }
    public decimal? WeightKg { get; set; }
    public int? RestSeconds { get; set; }
    public string? Tempo { get; set; }
    public string? Note { get; set; }
    public int WorkoutTemplateDayId { get; set; }
    public int? ExerciseId { get; set; }
}
