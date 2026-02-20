using Core.Application.Dtos;

namespace Application.Features.DietTemplateMealItems.Queries.GetList;

public class GetListDietTemplateMealItemListItemDto : IDto
{
    public int Id { get; set; }
    public int Order { get; set; }
    public string FoodName { get; set; }
    public decimal? Quantity { get; set; }
    public string? Unit { get; set; }
    public int? Calories { get; set; }
    public int? ProteinG { get; set; }
    public int? CarbG { get; set; }
    public int? FatG { get; set; }
    public string? Note { get; set; }
    public int DietTemplateMealId { get; set; }
}
