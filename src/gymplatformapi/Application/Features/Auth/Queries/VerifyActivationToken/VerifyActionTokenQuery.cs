using Application.Features.DietAssignments.Constants;
using Application.Features.DietAssignments.Rules;
using Application.Services.Repositories;
using Application.Services.UserActionTokens;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Security.Constants;
using Core.Security.Hashing;
using Domain.Entities;
using MediatR;
using static Application.Features.DietAssignments.Constants.DietAssignmentsOperationClaims;

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
            if (token == null || token.RevokedAt.HasValue || token.UsedAt.HasValue || token.DeletedDate.HasValue)
            {
                return new VerifyActionTokenQueryResponse() { Status = "invalid" };
            }

            return new VerifyActionTokenQueryResponse() { Status = "valid" };
        }
    }
}
