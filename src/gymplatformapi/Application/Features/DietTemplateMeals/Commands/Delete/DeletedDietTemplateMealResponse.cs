using Core.Application.Responses;

namespace Application.Features.DietTemplateMeals.Commands.Delete;

public class DeletedDietTemplateMealResponse : IResponse
{
    public int Id { get; set; }
}
