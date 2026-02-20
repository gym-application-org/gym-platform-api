using Core.Application.Responses;

namespace Application.Features.DietTemplates.Commands.Delete;

public class DeletedDietTemplateResponse : IResponse
{
    public int Id { get; set; }
}
