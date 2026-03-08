using Application.Features.UserActionTokens.Constants;
using Application.Features.UserActionTokens.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using Domain.Entities;
using Domain.Enums;
using Domain.Enums;
using MediatR;
using static Application.Features.UserActionTokens.Constants.UserActionTokensOperationClaims;

namespace Application.Features.UserActionTokens.Commands.Update;

public class UpdateUserActionTokenCommand
    : IRequest<UpdatedUserActionTokenResponse>,
        ISecuredRequest,
        ILoggableRequest,
        ITransactionalRequest
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid? TenantId { get; set; }
    public string Email { get; set; }
    public UserActionPurpose Purpose { get; set; }
    public UserActionTargetType? TargetType { get; set; }
    public Guid? TargetEntityId { get; set; }
    public string TokenHash { get; set; }
    public DateTime ExpiresAt { get; set; }
    public DateTime? UsedAt { get; set; }
    public DateTime? RevokedAt { get; set; }
    public Guid? ReplacedByInvitationId { get; set; }
    public Guid? CreatedByUserId { get; set; }
    public string? MetadataJson { get; set; }

    public string[] Roles => [Admin, Write, UserActionTokensOperationClaims.Update];

    public class UpdateUserActionTokenCommandHandler : IRequestHandler<UpdateUserActionTokenCommand, UpdatedUserActionTokenResponse>
    {
        private readonly IMapper _mapper;
        private readonly IUserActionTokenRepository _userActionTokenRepository;
        private readonly UserActionTokenBusinessRules _userActionTokenBusinessRules;

        public UpdateUserActionTokenCommandHandler(
            IMapper mapper,
            IUserActionTokenRepository userActionTokenRepository,
            UserActionTokenBusinessRules userActionTokenBusinessRules
        )
        {
            _mapper = mapper;
            _userActionTokenRepository = userActionTokenRepository;
            _userActionTokenBusinessRules = userActionTokenBusinessRules;
        }

        public async Task<UpdatedUserActionTokenResponse> Handle(UpdateUserActionTokenCommand request, CancellationToken cancellationToken)
        {
            UserActionToken? userActionToken = await _userActionTokenRepository.GetAsync(
                predicate: uat => uat.Id == request.Id,
                cancellationToken: cancellationToken
            );
            await _userActionTokenBusinessRules.UserActionTokenShouldExistWhenSelected(userActionToken);
            userActionToken = _mapper.Map(request, userActionToken);

            await _userActionTokenRepository.UpdateAsync(userActionToken!);

            UpdatedUserActionTokenResponse response = _mapper.Map<UpdatedUserActionTokenResponse>(userActionToken);
            return response;
        }
    }
}
