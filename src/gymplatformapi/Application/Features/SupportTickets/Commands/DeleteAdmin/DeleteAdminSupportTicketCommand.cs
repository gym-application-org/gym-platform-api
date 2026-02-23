using Application.Features.SupportTickets.Constants;
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
using MediatR;
using static Application.Features.SupportTickets.Constants.SupportTicketsOperationClaims;

namespace Application.Features.SupportTickets.Commands.DeleteAdmin;

public class DeleteAdminSupportTicketCommand
    : IRequest<DeletedAdminSupportTicketResponse>,
        ISecuredRequest,
        ICacheRemoverRequest,
        ILoggableRequest,
        ITransactionalRequest
{
    public int Id { get; set; }

    public string[] Roles => [GeneralOperationClaims.Admin];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetSupportTickets"];

    public class DeleteAdminSupportTicketCommandHandler
        : IRequestHandler<DeleteAdminSupportTicketCommand, DeletedAdminSupportTicketResponse>
    {
        private readonly IMapper _mapper;
        private readonly ISupportTicketRepository _supportTicketRepository;
        private readonly SupportTicketBusinessRules _supportTicketBusinessRules;

        public DeleteAdminSupportTicketCommandHandler(
            IMapper mapper,
            ISupportTicketRepository supportTicketRepository,
            SupportTicketBusinessRules supportTicketBusinessRules
        )
        {
            _mapper = mapper;
            _supportTicketRepository = supportTicketRepository;
            _supportTicketBusinessRules = supportTicketBusinessRules;
        }

        public async Task<DeletedAdminSupportTicketResponse> Handle(
            DeleteAdminSupportTicketCommand request,
            CancellationToken cancellationToken
        )
        {
            SupportTicket? supportTicket = await _supportTicketRepository.GetAsync(
                predicate: st => st.Id == request.Id,
                withDeleted: true,
                cancellationToken: cancellationToken
            );
            await _supportTicketBusinessRules.SupportTicketShouldExistWhenSelected(supportTicket);

            await _supportTicketRepository.DeleteAsync(supportTicket!);

            DeletedAdminSupportTicketResponse response = _mapper.Map<DeletedAdminSupportTicketResponse>(supportTicket);
            return response;
        }
    }
}
