using Application.Features.EmailOtps.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using static Application.Features.EmailOtps.Constants.EmailOtpsOperationClaims;

namespace Application.Features.EmailOtps.Queries.GetList;

public class GetListEmailOtpQuery : IRequest<GetListResponse<GetListEmailOtpListItemDto>>, ISecuredRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => [Admin, Read];

    public class GetListEmailOtpQueryHandler : IRequestHandler<GetListEmailOtpQuery, GetListResponse<GetListEmailOtpListItemDto>>
    {
        private readonly IEmailOtpRepository _emailOtpRepository;
        private readonly IMapper _mapper;

        public GetListEmailOtpQueryHandler(IEmailOtpRepository emailOtpRepository, IMapper mapper)
        {
            _emailOtpRepository = emailOtpRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListEmailOtpListItemDto>> Handle(
            GetListEmailOtpQuery request,
            CancellationToken cancellationToken
        )
        {
            IPaginate<EmailOtp> emailOtps = await _emailOtpRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListEmailOtpListItemDto> response = _mapper.Map<GetListResponse<GetListEmailOtpListItemDto>>(emailOtps);
            return response;
        }
    }
}
