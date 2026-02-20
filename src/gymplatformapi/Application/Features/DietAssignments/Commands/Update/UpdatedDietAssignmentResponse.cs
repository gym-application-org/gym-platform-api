using Core.Application.Responses;
using Domain.Entities;
using Domain.Enums;

namespace Application.Features.DietAssignments.Commands.Update;

public class UpdatedDietAssignmentResponse : IResponse
{
    public int Id { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public AssignmentStatus Status { get; set; }
    public Guid MemberId { get; set; }
    public Member Member { get; set; }
    public int DietTemplateId { get; set; }
}
