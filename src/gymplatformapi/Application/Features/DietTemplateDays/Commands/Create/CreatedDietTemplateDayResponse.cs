using Core.Application.Responses;

namespace Application.Features.DietTemplateDays.Commands.Create;

public class CreatedDietTemplateDayResponse : IResponse
{
    public int Id { get; set; }
    public int DayNumber { get; set; }
    public string Title { get; set; }
    public string? Notes { get; set; }
    public int DietTemplateId { get; set; }
}
