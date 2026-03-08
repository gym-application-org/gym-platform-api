using Application.Features.UserActionTokens.Constants;
using Application.Features.UserActionTokens.Constants;
using Application.Features.UserActionTokens.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using Domain.Entities;
using MediatR;
using static Application.Features.UserActionTokens.Constants.UserActionTokensOperationClaims;

namespace Application.Features.UserActionTokens.Commands.Delete;

public class DeleteUserActionTokenCommand
    : IRequest<DeletedUserActionTokenResponse>,
        ISecuredRequest,
        ILoggableRequest,
        ITransactionalRequest
{
    public Guid Id { get; set; }

    public string[] Roles => [Admin, Write, UserActionTokensOperationClaims.Delete];

    public class DeleteUserActionTokenCommandHandler : IRequestHandler<DeleteUserActionTokenCommand, DeletedUserActionTokenResponse>
    {
        private readonly IMapper _mapper;
        private readonly IUserActionTokenRepository _userActionTokenRepository;
        private readonly UserActionTokenBusinessRules _userActionTokenBusinessRules;

        public DeleteUserActionTokenCommandHandler(
            IMapper mapper,
            IUserActionTokenRepository userActionTokenRepository,
            UserActionTokenBusinessRules userActionTokenBusinessRules
        )
        {
            _mapper = mapper;
            _userActionTokenRepository = userActionTokenRepository;
            _userActionTokenBusinessRules = userActionTokenBusinessRules;
        }

        public async Task<DeletedUserActionTokenResponse> Handle(DeleteUserActionTokenCommand request, CancellationToken cancellationToken)
        {
            UserActionToken? userActionToken = await _userActionTokenRepository.GetAsync(
                predicate: uat => uat.Id == request.Id,
                cancellationToken: cancellationToken
            );
            await _userActionTokenBusinessRules.UserActionTokenShouldExistWhenSelected(userActionToken);

            await _userActionTokenRepository.DeleteAsync(userActionToken!);

            DeletedUserActionTokenResponse response = _mapper.Map<DeletedUserActionTokenResponse>(userActionToken);
            return response;
        }
    }
}
