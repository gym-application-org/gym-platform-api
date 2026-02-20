using Core.Application.Responses;

namespace Application.Features.Gates.Commands.Delete;

public class DeletedGateResponse : IResponse
{
    public int Id { get; set; }
}
