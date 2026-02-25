using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Application.Responses;
using Domain.Enums;

namespace Application.Features.DietAssignments.Queries.GetMyDietAssignmentById;

public class GetMyDietAssignmentByIdResponse : IResponse
{
    public int AssignmentId { get; set; }
    public AssignmentStatus Status { get; set; }

    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }

    public int DietTemplateId { get; set; }
    public string DietTemplateName { get; set; } = default!;
    public string? DietTemplateDescription { get; set; }

    public int? CaloriesTarget { get; set; }
    public int? ProteinGramsTarget { get; set; }
    public int? CarbGramsTarget { get; set; }
    public int? FatGramsTarget { get; set; }

    public ICollection<MyDietTemplateDayDto> Days { get; set; } = new List<MyDietTemplateDayDto>();
}
