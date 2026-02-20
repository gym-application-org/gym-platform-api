using Core.Application.Responses;

namespace Application.Features.DietTemplateMeals.Commands.Update;

public class UpdatedDietTemplateMealResponse : IResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Order { get; set; }
    public string? Notes { get; set; }
    public int DietTemplateDayId { get; set; }
}
