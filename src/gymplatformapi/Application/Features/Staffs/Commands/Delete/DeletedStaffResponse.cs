using Core.Application.Responses;

namespace Application.Features.Staffs.Commands.Delete;

public class DeletedStaffResponse : IResponse
{
    public Guid Id { get; set; }
}
