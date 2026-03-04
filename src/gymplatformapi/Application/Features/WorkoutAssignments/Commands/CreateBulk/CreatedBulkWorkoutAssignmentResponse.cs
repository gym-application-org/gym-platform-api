using Core.Application.Responses;
using Domain.Enums;

namespace Application.Features.WorkoutAssignments.Commands.CreateBulk;

public class CreatedBulkWorkoutAssignmentResponse : IResponse
{
    public int WorkoutTemplateId { get; set; }
    public int CreatedCount { get; set; }
    public ICollection<int> AssignmentIds { get; set; } = new List<int>();
}
