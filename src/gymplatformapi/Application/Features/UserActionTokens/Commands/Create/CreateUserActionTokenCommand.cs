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

namespace Application.Features.UserActionTokens.Commands.Create;

public class CreateUserActionTokenCommand
    : IRequest<CreatedUserActionTokenResponse>,
        ISecuredRequest,
        ILoggableRequest,
        ITransactionalRequest
{
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

    public string[] Roles => [Admin, Write, UserActionTokensOperationClaims.Create];

    public class CreateUserActionTokenCommandHandler : IRequestHandler<CreateUserActionTokenCommand, CreatedUserActionTokenResponse>
    {
        private readonly IMapper _mapper;
        private readonly IUserActionTokenRepository _userActionTokenRepository;
        private readonly UserActionTokenBusinessRules _userActionTokenBusinessRules;

        public CreateUserActionTokenCommandHandler(
            IMapper mapper,
            IUserActionTokenRepository userActionTokenRepository,
            UserActionTokenBusinessRules userActionTokenBusinessRules
        )
        {
            _mapper = mapper;
            _userActionTokenRepository = userActionTokenRepository;
            _userActionTokenBusinessRules = userActionTokenBusinessRules;
        }

        public async Task<CreatedUserActionTokenResponse> Handle(CreateUserActionTokenCommand request, CancellationToken cancellationToken)
        {
            UserActionToken userActionToken = _mapper.Map<UserActionToken>(request);

            await _userActionTokenRepository.AddAsync(userActionToken);

            CreatedUserActionTokenResponse response = _mapper.Map<CreatedUserActionTokenResponse>(userActionToken);
            return response;
        }
    }
}
