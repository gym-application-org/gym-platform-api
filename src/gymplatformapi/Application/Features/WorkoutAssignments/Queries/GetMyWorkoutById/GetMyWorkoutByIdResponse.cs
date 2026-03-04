using Core.Application.Responses;
using Domain.Enums;

namespace Application.Features.WorkoutAssignments.Queries.GetMyWorkoutById;

public class GetMyWorkoutByIdResponse : IResponse
{
    public int AssignmentId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public AssignmentStatus Status { get; set; }

    public int WorkoutTemplateId { get; set; }

    public string WorkoutTemplateName { get; set; }
    public string? WorkoutTemplateDescription { get; set; }
    public DifficultyLevel WorkoutTemplateLevel { get; set; }
    public bool WorkoutTemplateIsActive { get; set; }

    public ICollection<MyWorkoutTemplateDayDto> Days { get; set; } = new List<MyWorkoutTemplateDayDto>();
}
