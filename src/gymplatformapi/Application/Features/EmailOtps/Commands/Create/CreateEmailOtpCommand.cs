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

namespace Application.Features.EmailOtps.Commands.Create;

public class CreateEmailOtpCommand : IRequest<CreatedEmailOtpResponse>, ISecuredRequest, ILoggableRequest, ITransactionalRequest
{
    public string Email { get; set; }
    public string CodeHash { get; set; }
    public OtpPurpose Purpose { get; set; }
    public DateTime ExpiresAt { get; set; }
    public DateTime? UsedDate { get; set; }
    public bool IsUsed { get; set; }
    public int TryCount { get; set; }
    public Guid? TenantId { get; set; }
    public Guid? UserId { get; set; }

    public string[] Roles => [Admin, Write, EmailOtpsOperationClaims.Create];

    public class CreateEmailOtpCommandHandler : IRequestHandler<CreateEmailOtpCommand, CreatedEmailOtpResponse>
    {
        private readonly IMapper _mapper;
        private readonly IEmailOtpRepository _emailOtpRepository;
        private readonly EmailOtpBusinessRules _emailOtpBusinessRules;

        public CreateEmailOtpCommandHandler(
            IMapper mapper,
            IEmailOtpRepository emailOtpRepository,
            EmailOtpBusinessRules emailOtpBusinessRules
        )
        {
            _mapper = mapper;
            _emailOtpRepository = emailOtpRepository;
            _emailOtpBusinessRules = emailOtpBusinessRules;
        }

        public async Task<CreatedEmailOtpResponse> Handle(CreateEmailOtpCommand request, CancellationToken cancellationToken)
        {
            EmailOtp emailOtp = _mapper.Map<EmailOtp>(request);

            await _emailOtpRepository.AddAsync(emailOtp);

            CreatedEmailOtpResponse response = _mapper.Map<CreatedEmailOtpResponse>(emailOtp);
            return response;
        }
    }
}
