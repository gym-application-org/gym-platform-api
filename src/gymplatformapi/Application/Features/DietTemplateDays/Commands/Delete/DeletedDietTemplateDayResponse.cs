using Core.Application.Responses;

namespace Application.Features.DietTemplateDays.Commands.Delete;

public class DeletedDietTemplateDayResponse : IResponse
{
    public int Id { get; set; }
}
