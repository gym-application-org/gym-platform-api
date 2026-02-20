using Core.Application.Responses;

namespace Application.Features.WorkoutAssignments.Commands.Delete;

public class DeletedWorkoutAssignmentResponse : IResponse
{
    public int Id { get; set; }
}
