using Core.Application.Responses;

namespace Application.Features.DietTemplateMeals.Queries.GetById;

public class GetByIdDietTemplateMealResponse : IResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Order { get; set; }
    public string? Notes { get; set; }
    public int DietTemplateDayId { get; set; }
}
