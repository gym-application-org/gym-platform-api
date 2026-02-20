using Core.Application.Dtos;

namespace Application.Features.DietTemplateDays.Queries.GetList;

public class GetListDietTemplateDayListItemDto : IDto
{
    public int Id { get; set; }
    public int DayNumber { get; set; }
    public string Title { get; set; }
    public string? Notes { get; set; }
    public int DietTemplateId { get; set; }
}
