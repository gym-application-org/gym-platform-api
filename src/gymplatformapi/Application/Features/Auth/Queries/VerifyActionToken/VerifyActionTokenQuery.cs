using Application.Services.UserActionTokens;
using Core.Security.Hashing;
using Domain.Entities;
using MediatR;

namespace Application.Features.Auth.Queries.VerifyActionToken;

public class VerifyActionTokenQuery : IRequest<VerifyActionTokenQueryResponse>
{
    public string Token { get; set; }

    public class VerifyActionTokenQueryHandler : IRequestHandler<VerifyActionTokenQuery, VerifyActionTokenQueryResponse>
    {
        private readonly IUserActionTokenService _userActionTokenService;

        public VerifyActionTokenQueryHandler(IUserActionTokenService userActionTokenService)
        {
            _userActionTokenService = userActionTokenService;
        }

        public async Task<VerifyActionTokenQueryResponse> Handle(VerifyActionTokenQuery request, CancellationToken cancellationToken)
        {
            HashingHelper.CreateActionTokenHash(request.Token, out byte[] tokenHash);

            UserActionToken? token = await _userActionTokenService.GetAsync(
                x => x.TokenHash == tokenHash,
                cancellationToken: cancellationToken
            );
            if (
                token == null
                || token.RevokedAt.HasValue
                || token.UsedAt.HasValue
                || token.DeletedDate.HasValue
                || token.ExpiresAt <= DateTime.UtcNow
            )
            {
                return new VerifyActionTokenQueryResponse() { Status = "invalid" };
            }

            return new VerifyActionTokenQueryResponse() { Status = "valid" };
        }
    }
}
