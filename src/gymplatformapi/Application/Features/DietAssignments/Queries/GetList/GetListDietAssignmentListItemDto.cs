using Core.Application.Dtos;
using Domain.Entities;
using Domain.Enums;

namespace Application.Features.DietAssignments.Queries.GetList;

public class GetListDietAssignmentListItemDto : IDto
{
    public int Id { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public AssignmentStatus Status { get; set; }
    public Guid MemberId { get; set; }
    public int DietTemplateId { get; set; }
}
