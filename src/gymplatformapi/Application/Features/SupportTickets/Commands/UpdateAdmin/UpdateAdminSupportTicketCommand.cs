using Application.Features.SupportTickets.Constants;
using Application.Features.SupportTickets.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using Core.Security.Constants;
using Domain.Entities;
using Domain.Enums;
using Domain.Enums;
using MediatR;
using static Application.Features.SupportTickets.Constants.SupportTicketsOperationClaims;

namespace Application.Features.SupportTickets.Commands.UpdateAdmin;

public class UpdateAdminSupportTicketCommand
    : IRequest<UpdatedAdminSupportTicketResponse>,
        ISecuredRequest,
        ICacheRemoverRequest,
        ILoggableRequest,
        ITransactionalRequest
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public TicketStatus Status { get; set; }
    public TicketPriority Priority { get; set; }
    public DateTime? ClosedAt { get; set; }

    public string[] Roles => [GeneralOperationClaims.Admin];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetSupportTickets"];

    public class UpdateAdminSupportTicketCommandHandler
        : IRequestHandler<UpdateAdminSupportTicketCommand, UpdatedAdminSupportTicketResponse>
    {
        private readonly IMapper _mapper;
        private readonly ISupportTicketRepository _supportTicketRepository;
        private readonly SupportTicketBusinessRules _supportTicketBusinessRules;

        public UpdateAdminSupportTicketCommandHandler(
            IMapper mapper,
            ISupportTicketRepository supportTicketRepository,
            SupportTicketBusinessRules supportTicketBusinessRules
        )
        {
            _mapper = mapper;
            _supportTicketRepository = supportTicketRepository;
            _supportTicketBusinessRules = supportTicketBusinessRules;
        }

        public async Task<UpdatedAdminSupportTicketResponse> Handle(
            UpdateAdminSupportTicketCommand request,
            CancellationToken cancellationToken
        )
        {
            SupportTicket? supportTicket = await _supportTicketRepository.GetAsync(
                predicate: st => st.Id == request.Id,
                cancellationToken: cancellationToken,
                withDeleted: true
            );
            await _supportTicketBusinessRules.SupportTicketShouldExistWhenSelected(supportTicket);
            supportTicket = _mapper.Map(request, supportTicket);

            await _supportTicketRepository.UpdateAsync(supportTicket!);

            UpdatedAdminSupportTicketResponse response = _mapper.Map<UpdatedAdminSupportTicketResponse>(supportTicket);
            return response;
        }
    }
}
