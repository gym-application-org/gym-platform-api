using Core.Application.Responses;

namespace Application.Features.DietTemplateMealItems.Commands.Delete;

public class DeletedDietTemplateMealItemResponse : IResponse
{
    public int Id { get; set; }
}
