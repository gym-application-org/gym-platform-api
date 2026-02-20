using Core.Application.Dtos;

namespace Application.Features.DietTemplateMeals.Queries.GetList;

public class GetListDietTemplateMealListItemDto : IDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Order { get; set; }
    public string? Notes { get; set; }
    public int DietTemplateDayId { get; set; }
}
