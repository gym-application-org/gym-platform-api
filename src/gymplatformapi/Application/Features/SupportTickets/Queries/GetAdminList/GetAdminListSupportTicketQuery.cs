using Application.Features.SupportTickets.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Core.Security.Constants;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using static Application.Features.SupportTickets.Constants.SupportTicketsOperationClaims;

namespace Application.Features.SupportTickets.Queries.GetAdminList;

public class GetAdminListSupportTicketQuery
    : IRequest<GetListResponse<GetAdminListSupportTicketListItemDto>>,
        ISecuredRequest,
        ICachableRequest
{
    public PageRequest PageRequest { get; set; }
    public DateTime? From { get; set; }
    public DateTime? To { get; set; }
    public TicketStatus? Status { get; set; }
    public TicketPriority? Priority { get; set; }
    public Guid? TenantId { get; set; }

    public string[] Roles => [GeneralOperationClaims.Admin];

    public bool BypassCache { get; }
    public string? CacheKey => $"GetListSupportTickets({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string? CacheGroupKey => "GetSupportTickets";
    public TimeSpan? SlidingExpiration { get; }

    public class GetAdminListSupportTicketQueryHandler
        : IRequestHandler<GetAdminListSupportTicketQuery, GetListResponse<GetAdminListSupportTicketListItemDto>>
    {
        private readonly ISupportTicketRepository _supportTicketRepository;
        private readonly IMapper _mapper;

        public GetAdminListSupportTicketQueryHandler(ISupportTicketRepository supportTicketRepository, IMapper mapper)
        {
            _supportTicketRepository = supportTicketRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetAdminListSupportTicketListItemDto>> Handle(
            GetAdminListSupportTicketQuery request,
            CancellationToken cancellationToken
        )
        {
            IPaginate<SupportTicket> supportTickets = await _supportTicketRepository.GetListAsync(
                predicate: x =>
                    (!request.From.HasValue || x.CreatedDate >= request.From.Value)
                    && (!request.To.HasValue || (x.ClosedAt ?? DateTime.UtcNow) <= request.To.Value)
                    && (!request.Status.HasValue || x.Status == request.Status)
                    && (!request.Priority.HasValue || x.Priority == request.Priority)
                    && (!request.TenantId.HasValue || x.TenantId == request.TenantId),
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken,
                withDeleted: true
            );

            GetListResponse<GetAdminListSupportTicketListItemDto> response = _mapper.Map<
                GetListResponse<GetAdminListSupportTicketListItemDto>
            >(supportTickets);
            return response;
        }
    }
}
