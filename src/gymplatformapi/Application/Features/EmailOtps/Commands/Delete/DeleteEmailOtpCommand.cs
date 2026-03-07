using Application.Features.EmailOtps.Constants;
using Application.Features.EmailOtps.Constants;
using Application.Features.EmailOtps.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using Domain.Entities;
using MediatR;
using static Application.Features.EmailOtps.Constants.EmailOtpsOperationClaims;

namespace Application.Features.EmailOtps.Commands.Delete;

public class DeleteEmailOtpCommand : IRequest<DeletedEmailOtpResponse>, ISecuredRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }

    public string[] Roles => [Admin, Write, EmailOtpsOperationClaims.Delete];

    public class DeleteEmailOtpCommandHandler : IRequestHandler<DeleteEmailOtpCommand, DeletedEmailOtpResponse>
    {
        private readonly IMapper _mapper;
        private readonly IEmailOtpRepository _emailOtpRepository;
        private readonly EmailOtpBusinessRules _emailOtpBusinessRules;

        public DeleteEmailOtpCommandHandler(
            IMapper mapper,
            IEmailOtpRepository emailOtpRepository,
            EmailOtpBusinessRules emailOtpBusinessRules
        )
        {
            _mapper = mapper;
            _emailOtpRepository = emailOtpRepository;
            _emailOtpBusinessRules = emailOtpBusinessRules;
        }

        public async Task<DeletedEmailOtpResponse> Handle(DeleteEmailOtpCommand request, CancellationToken cancellationToken)
        {
            EmailOtp? emailOtp = await _emailOtpRepository.GetAsync(
                predicate: eo => eo.Id == request.Id,
                cancellationToken: cancellationToken
            );
            await _emailOtpBusinessRules.EmailOtpShouldExistWhenSelected(emailOtp);

            await _emailOtpRepository.DeleteAsync(emailOtp!);

            DeletedEmailOtpResponse response = _mapper.Map<DeletedEmailOtpResponse>(emailOtp);
            return response;
        }
    }
}
