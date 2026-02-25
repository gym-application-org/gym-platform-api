using Core.Application.Responses;
using Domain.Entities;
using Domain.Enums;

namespace Application.Features.DietAssignments.Commands.CreateBulk;

public class CreatedBulkDietAssignmentResponse : IResponse
{
    public int DietTemplateId { get; set; }
    public int CreatedCount { get; set; }
    public ICollection<int> AssignmentIds { get; set; } = new List<int>();
}
