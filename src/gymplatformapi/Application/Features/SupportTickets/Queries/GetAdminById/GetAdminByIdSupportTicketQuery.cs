using Application.Features.SupportTickets.Constants;
using Application.Features.SupportTickets.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Security.Constants;
using Domain.Entities;
using MediatR;
using static Application.Features.SupportTickets.Constants.SupportTicketsOperationClaims;

namespace Application.Features.SupportTickets.Queries.GetAdminById;

public class GetAdminByIdSupportTicketQuery : IRequest<GetAdminByIdSupportTicketResponse>, ISecuredRequest
{
    public int Id { get; set; }

    public string[] Roles => [GeneralOperationClaims.Admin];

    public class GetAdminByIdSupportTicketQueryHandler : IRequestHandler<GetAdminByIdSupportTicketQuery, GetAdminByIdSupportTicketResponse>
    {
        private readonly IMapper _mapper;
        private readonly ISupportTicketRepository _supportTicketRepository;
        private readonly SupportTicketBusinessRules _supportTicketBusinessRules;

        public GetAdminByIdSupportTicketQueryHandler(
            IMapper mapper,
            ISupportTicketRepository supportTicketRepository,
            SupportTicketBusinessRules supportTicketBusinessRules
        )
        {
            _mapper = mapper;
            _supportTicketRepository = supportTicketRepository;
            _supportTicketBusinessRules = supportTicketBusinessRules;
        }

        public async Task<GetAdminByIdSupportTicketResponse> Handle(
            GetAdminByIdSupportTicketQuery request,
            CancellationToken cancellationToken
        )
        {
            SupportTicket? supportTicket = await _supportTicketRepository.GetAsync(
                predicate: st => st.Id == request.Id,
                cancellationToken: cancellationToken,
                ignoreQueryFilters: true
            );
            await _supportTicketBusinessRules.SupportTicketShouldExistWhenSelected(supportTicket);

            GetAdminByIdSupportTicketResponse response = _mapper.Map<GetAdminByIdSupportTicketResponse>(supportTicket);
            return response;
        }
    }
}
