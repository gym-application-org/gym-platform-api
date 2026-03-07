using Core.Application.Responses;

namespace Application.Features.EmailOtps.Commands.Delete;

public class DeletedEmailOtpResponse : IResponse
{
    public Guid Id { get; set; }
}
