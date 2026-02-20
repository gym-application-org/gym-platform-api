using Core.Application.Responses;

namespace Application.Features.WorkoutTemplates.Commands.Delete;

public class DeletedWorkoutTemplateResponse : IResponse
{
    public int Id { get; set; }
}
