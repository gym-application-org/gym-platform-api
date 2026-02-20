using Application.Features.SupportTickets.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using static Application.Features.SupportTickets.Constants.SupportTicketsOperationClaims;

namespace Application.Features.SupportTickets.Queries.GetList;

public class GetListSupportTicketQuery : IRequest<GetListResponse<GetListSupportTicketListItemDto>>, ISecuredRequest, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => [Admin, Read];

    public bool BypassCache { get; }
    public string? CacheKey => $"GetListSupportTickets({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string? CacheGroupKey => "GetSupportTickets";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListSupportTicketQueryHandler
        : IRequestHandler<GetListSupportTicketQuery, GetListResponse<GetListSupportTicketListItemDto>>
    {
        private readonly ISupportTicketRepository _supportTicketRepository;
        private readonly IMapper _mapper;

        public GetListSupportTicketQueryHandler(ISupportTicketRepository supportTicketRepository, IMapper mapper)
        {
            _supportTicketRepository = supportTicketRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListSupportTicketListItemDto>> Handle(
            GetListSupportTicketQuery request,
            CancellationToken cancellationToken
        )
        {
            IPaginate<SupportTicket> supportTickets = await _supportTicketRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListSupportTicketListItemDto> response = _mapper.Map<GetListResponse<GetListSupportTicketListItemDto>>(
                supportTickets
            );
            return response;
        }
    }
}
