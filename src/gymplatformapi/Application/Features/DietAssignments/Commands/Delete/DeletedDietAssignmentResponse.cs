using Core.Application.Responses;

namespace Application.Features.DietAssignments.Commands.Delete;

public class DeletedDietAssignmentResponse : IResponse
{
    public int Id { get; set; }
}
