using Application.Features.SupportTickets.Constants;
using Application.Features.SupportTickets.Constants;
using Application.Features.SupportTickets.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using Domain.Entities;
using MediatR;
using static Application.Features.SupportTickets.Constants.SupportTicketsOperationClaims;

namespace Application.Features.SupportTickets.Commands.Delete;

public class DeleteSupportTicketCommand
    : IRequest<DeletedSupportTicketResponse>,
        ISecuredRequest,
        ICacheRemoverRequest,
        ILoggableRequest,
        ITransactionalRequest
{
    public int Id { get; set; }

    public string[] Roles => [Admin, Write, SupportTicketsOperationClaims.Delete];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetSupportTickets"];

    public class DeleteSupportTicketCommandHandler : IRequestHandler<DeleteSupportTicketCommand, DeletedSupportTicketResponse>
    {
        private readonly IMapper _mapper;
        private readonly ISupportTicketRepository _supportTicketRepository;
        private readonly SupportTicketBusinessRules _supportTicketBusinessRules;

        public DeleteSupportTicketCommandHandler(
            IMapper mapper,
            ISupportTicketRepository supportTicketRepository,
            SupportTicketBusinessRules supportTicketBusinessRules
        )
        {
            _mapper = mapper;
            _supportTicketRepository = supportTicketRepository;
            _supportTicketBusinessRules = supportTicketBusinessRules;
        }

        public async Task<DeletedSupportTicketResponse> Handle(DeleteSupportTicketCommand request, CancellationToken cancellationToken)
        {
            SupportTicket? supportTicket = await _supportTicketRepository.GetAsync(
                predicate: st => st.Id == request.Id,
                cancellationToken: cancellationToken
            );
            await _supportTicketBusinessRules.SupportTicketShouldExistWhenSelected(supportTicket);

            await _supportTicketRepository.DeleteAsync(supportTicket!);

            DeletedSupportTicketResponse response = _mapper.Map<DeletedSupportTicketResponse>(supportTicket);
            return response;
        }
    }
}
