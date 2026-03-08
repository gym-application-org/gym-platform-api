using Core.Application.Responses;

namespace Application.Features.UserActionTokens.Commands.Delete;

public class DeletedUserActionTokenResponse : IResponse
{
    public Guid Id { get; set; }
}
