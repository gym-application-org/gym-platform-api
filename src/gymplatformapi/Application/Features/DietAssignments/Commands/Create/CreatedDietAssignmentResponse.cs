using Core.Application.Responses;
using Domain.Entities;
using Domain.Enums;

namespace Application.Features.DietAssignments.Commands.Create;

public class CreatedDietAssignmentResponse : IResponse
{
    public int Id { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public AssignmentStatus Status { get; set; }
    public Guid MemberId { get; set; }
    public Member Member { get; set; }
    public int DietTemplateId { get; set; }
}
