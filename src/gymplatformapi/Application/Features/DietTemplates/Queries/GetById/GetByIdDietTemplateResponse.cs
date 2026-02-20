using Core.Application.Responses;

namespace Application.Features.DietTemplates.Queries.GetById;

public class GetByIdDietTemplateResponse : IResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public int? CaloriesTarget { get; set; }
    public int? ProteinGramsTarget { get; set; }
    public int? CarbGramsTarget { get; set; }
    public int? FatGramsTarget { get; set; }
    public bool IsActive { get; set; }
}
