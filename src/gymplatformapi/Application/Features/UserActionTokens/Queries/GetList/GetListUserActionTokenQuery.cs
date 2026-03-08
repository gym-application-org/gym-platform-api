using Application.Features.UserActionTokens.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using static Application.Features.UserActionTokens.Constants.UserActionTokensOperationClaims;

namespace Application.Features.UserActionTokens.Queries.GetList;

public class GetListUserActionTokenQuery : IRequest<GetListResponse<GetListUserActionTokenListItemDto>>, ISecuredRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => [Admin, Read];

    public class GetListUserActionTokenQueryHandler
        : IRequestHandler<GetListUserActionTokenQuery, GetListResponse<GetListUserActionTokenListItemDto>>
    {
        private readonly IUserActionTokenRepository _userActionTokenRepository;
        private readonly IMapper _mapper;

        public GetListUserActionTokenQueryHandler(IUserActionTokenRepository userActionTokenRepository, IMapper mapper)
        {
            _userActionTokenRepository = userActionTokenRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListUserActionTokenListItemDto>> Handle(
            GetListUserActionTokenQuery request,
            CancellationToken cancellationToken
        )
        {
            IPaginate<UserActionToken> userActionTokens = await _userActionTokenRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListUserActionTokenListItemDto> response = _mapper.Map<GetListResponse<GetListUserActionTokenListItemDto>>(
                userActionTokens
            );
            return response;
        }
    }
}
