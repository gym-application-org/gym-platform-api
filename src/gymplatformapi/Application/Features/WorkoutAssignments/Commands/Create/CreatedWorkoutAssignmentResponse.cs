using Core.Application.Responses;
using Domain.Enums;

namespace Application.Features.WorkoutAssignments.Commands.Create;

public class CreatedWorkoutAssignmentResponse : IResponse
{
    public int Id { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public AssignmentStatus Status { get; set; }
    public Guid MemberId { get; set; }
    public int WorkoutTemplateId { get; set; }
}
