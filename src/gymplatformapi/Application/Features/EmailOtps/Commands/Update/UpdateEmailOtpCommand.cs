using Application.Features.EmailOtps.Constants;
using Application.Features.EmailOtps.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using static Application.Features.EmailOtps.Constants.EmailOtpsOperationClaims;

namespace Application.Features.EmailOtps.Commands.Update;

public class UpdateEmailOtpCommand : IRequest<UpdatedEmailOtpResponse>, ISecuredRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string CodeHash { get; set; }
    public OtpPurpose Purpose { get; set; }
    public DateTime ExpiresAt { get; set; }
    public DateTime? UsedDate { get; set; }
    public bool IsUsed { get; set; }
    public int TryCount { get; set; }
    public Guid? TenantId { get; set; }
    public Guid? UserId { get; set; }

    public string[] Roles => [Admin, Write, EmailOtpsOperationClaims.Update];

    public class UpdateEmailOtpCommandHandler : IRequestHandler<UpdateEmailOtpCommand, UpdatedEmailOtpResponse>
    {
        private readonly IMapper _mapper;
        private readonly IEmailOtpRepository _emailOtpRepository;
        private readonly EmailOtpBusinessRules _emailOtpBusinessRules;

        public UpdateEmailOtpCommandHandler(
            IMapper mapper,
            IEmailOtpRepository emailOtpRepository,
            EmailOtpBusinessRules emailOtpBusinessRules
        )
        {
            _mapper = mapper;
            _emailOtpRepository = emailOtpRepository;
            _emailOtpBusinessRules = emailOtpBusinessRules;
        }

        public async Task<UpdatedEmailOtpResponse> Handle(UpdateEmailOtpCommand request, CancellationToken cancellationToken)
        {
            EmailOtp? emailOtp = await _emailOtpRepository.GetAsync(
                predicate: eo => eo.Id == request.Id,
                cancellationToken: cancellationToken
            );
            await _emailOtpBusinessRules.EmailOtpShouldExistWhenSelected(emailOtp);
            emailOtp = _mapper.Map(request, emailOtp);

            await _emailOtpRepository.UpdateAsync(emailOtp!);

            UpdatedEmailOtpResponse response = _mapper.Map<UpdatedEmailOtpResponse>(emailOtp);
            return response;
        }
    }
}
