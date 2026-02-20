using Core.Application.Responses;

namespace Application.Features.Exercises.Commands.Delete;

public class DeletedExerciseResponse : IResponse
{
    public int Id { get; set; }
}
