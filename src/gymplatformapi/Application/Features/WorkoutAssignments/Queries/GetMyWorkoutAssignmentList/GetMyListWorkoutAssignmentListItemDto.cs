using Core.Application.Dtos;
using Domain.Enums;

namespace Application.Features.WorkoutAssignments.Queries.GetMyWorkoutAssignmentList;

public class GetMyListWorkoutAssignmentListItemDto : IDto
{
    public int Id { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public AssignmentStatus Status { get; set; }
    public int WorkoutTemplateId { get; set; }
}
