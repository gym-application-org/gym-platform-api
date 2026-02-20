using Application.Features.SupportTickets.Constants;
using Application.Features.SupportTickets.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Domain.Entities;
using MediatR;
using static Application.Features.SupportTickets.Constants.SupportTicketsOperationClaims;

namespace Application.Features.SupportTickets.Queries.GetById;

public class GetByIdSupportTicketQuery : IRequest<GetByIdSupportTicketResponse>, ISecuredRequest
{
    public int Id { get; set; }

    public string[] Roles => [Admin, Read];

    public class GetByIdSupportTicketQueryHandler : IRequestHandler<GetByIdSupportTicketQuery, GetByIdSupportTicketResponse>
    {
        private readonly IMapper _mapper;
        private readonly ISupportTicketRepository _supportTicketRepository;
        private readonly SupportTicketBusinessRules _supportTicketBusinessRules;

        public GetByIdSupportTicketQueryHandler(
            IMapper mapper,
            ISupportTicketRepository supportTicketRepository,
            SupportTicketBusinessRules supportTicketBusinessRules
        )
        {
            _mapper = mapper;
            _supportTicketRepository = supportTicketRepository;
            _supportTicketBusinessRules = supportTicketBusinessRules;
        }

        public async Task<GetByIdSupportTicketResponse> Handle(GetByIdSupportTicketQuery request, CancellationToken cancellationToken)
        {
            SupportTicket? supportTicket = await _supportTicketRepository.GetAsync(
                predicate: st => st.Id == request.Id,
                cancellationToken: cancellationToken
            );
            await _supportTicketBusinessRules.SupportTicketShouldExistWhenSelected(supportTicket);

            GetByIdSupportTicketResponse response = _mapper.Map<GetByIdSupportTicketResponse>(supportTicket);
            return response;
        }
    }
}
