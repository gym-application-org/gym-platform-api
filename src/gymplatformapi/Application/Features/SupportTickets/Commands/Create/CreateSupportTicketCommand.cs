using Application.Features.SupportTickets.Constants;
using Application.Features.SupportTickets.Rules;
using Application.Services.Repositories;
using Application.Services.Staffs;
using AutoMapper;
using Core.Application.Abstractions.Security;
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

namespace Application.Features.SupportTickets.Commands.Create;

public class CreateSupportTicketCommand
    : IRequest<CreatedSupportTicketResponse>,
        ISecuredRequest,
        ICacheRemoverRequest,
        ILoggableRequest,
        ITransactionalRequest,
        ITenantRequest
{
    public string Title { get; set; }
    public string Description { get; set; }
    public TicketStatus Status { get; set; }
    public TicketPriority Priority { get; set; }
    public DateTime? ClosedAt { get; set; }

    public string[] Roles => [GeneralOperationClaims.Owner];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetSupportTickets"];

    public class CreateSupportTicketCommandHandler : IRequestHandler<CreateSupportTicketCommand, CreatedSupportTicketResponse>
    {
        private readonly IMapper _mapper;
        private readonly ISupportTicketRepository _supportTicketRepository;
        private readonly SupportTicketBusinessRules _supportTicketBusinessRules;
        private readonly ICurrentTenant _currentTenant;
        private readonly IStaffService _staffService;
        private readonly ICurrentUser _currentUser;

        public CreateSupportTicketCommandHandler(
            IMapper mapper,
            ISupportTicketRepository supportTicketRepository,
            SupportTicketBusinessRules supportTicketBusinessRules,
            ICurrentTenant currentTenant,
            ICurrentUser currentUser,
            IStaffService staffService
        )
        {
            _mapper = mapper;
            _supportTicketRepository = supportTicketRepository;
            _supportTicketBusinessRules = supportTicketBusinessRules;
            _currentUser = currentUser;
            _staffService = staffService;
            _currentTenant = currentTenant;
        }

        public async Task<CreatedSupportTicketResponse> Handle(CreateSupportTicketCommand request, CancellationToken cancellationToken)
        {
            Staff? staff = await _staffService.GetAsync(predicate: x => x.UserId == _currentUser.UserId);
            await _supportTicketBusinessRules.StaffShouldExistWhenSelected(staff);

            SupportTicket supportTicket = _mapper.Map<SupportTicket>(request);
            supportTicket.CreatedByStaffId = staff!.Id;
            supportTicket.TenantId = _currentTenant.TenantId!.Value;
            supportTicket.Status = TicketStatus.Open;

            await _supportTicketRepository.AddAsync(supportTicket);

            CreatedSupportTicketResponse response = _mapper.Map<CreatedSupportTicketResponse>(supportTicket);
            return response;
        }
    }
}
