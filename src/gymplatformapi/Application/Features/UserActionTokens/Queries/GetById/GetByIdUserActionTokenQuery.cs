using Application.Features.UserActionTokens.Constants;
using Application.Features.UserActionTokens.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Domain.Entities;
using MediatR;
using static Application.Features.UserActionTokens.Constants.UserActionTokensOperationClaims;

namespace Application.Features.UserActionTokens.Queries.GetById;

public class GetByIdUserActionTokenQuery : IRequest<GetByIdUserActionTokenResponse>, ISecuredRequest
{
    public Guid Id { get; set; }

    public string[] Roles => [Admin, Read];

    public class GetByIdUserActionTokenQueryHandler : IRequestHandler<GetByIdUserActionTokenQuery, GetByIdUserActionTokenResponse>
    {
        private readonly IMapper _mapper;
        private readonly IUserActionTokenRepository _userActionTokenRepository;
        private readonly UserActionTokenBusinessRules _userActionTokenBusinessRules;

        public GetByIdUserActionTokenQueryHandler(
            IMapper mapper,
            IUserActionTokenRepository userActionTokenRepository,
            UserActionTokenBusinessRules userActionTokenBusinessRules
        )
        {
            _mapper = mapper;
            _userActionTokenRepository = userActionTokenRepository;
            _userActionTokenBusinessRules = userActionTokenBusinessRules;
        }

        public async Task<GetByIdUserActionTokenResponse> Handle(GetByIdUserActionTokenQuery request, CancellationToken cancellationToken)
        {
            UserActionToken? userActionToken = await _userActionTokenRepository.GetAsync(
                predicate: uat => uat.Id == request.Id,
                cancellationToken: cancellationToken
            );
            await _userActionTokenBusinessRules.UserActionTokenShouldExistWhenSelected(userActionToken);

            GetByIdUserActionTokenResponse response = _mapper.Map<GetByIdUserActionTokenResponse>(userActionToken);
            return response;
        }
    }
}
