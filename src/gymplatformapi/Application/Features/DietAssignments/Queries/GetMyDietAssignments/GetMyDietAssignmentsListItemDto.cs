using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Application.Responses;
using Domain.Enums;

namespace Application.Features.DietAssignments.Queries.GetMyDietAssignemnts;

public class GetMyDietAssignmentsListItemDto : IResponse
{
    public int AssignmentId { get; set; }

    public int DietTemplateId { get; set; }
    public string DietTemplateName { get; set; } = default!;
    public string? DietTemplateDescription { get; set; }

    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public AssignmentStatus Status { get; set; }
}
