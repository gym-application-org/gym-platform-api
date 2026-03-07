using Application.Features.EmailOtps.Constants;
using Application.Features.EmailOtps.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Domain.Entities;
using MediatR;
using static Application.Features.EmailOtps.Constants.EmailOtpsOperationClaims;

namespace Application.Features.EmailOtps.Queries.GetById;

public class GetByIdEmailOtpQuery : IRequest<GetByIdEmailOtpResponse>, ISecuredRequest
{
    public Guid Id { get; set; }

    public string[] Roles => [Admin, Read];

    public class GetByIdEmailOtpQueryHandler : IRequestHandler<GetByIdEmailOtpQuery, GetByIdEmailOtpResponse>
    {
        private readonly IMapper _mapper;
        private readonly IEmailOtpRepository _emailOtpRepository;
        private readonly EmailOtpBusinessRules _emailOtpBusinessRules;

        public GetByIdEmailOtpQueryHandler(
            IMapper mapper,
            IEmailOtpRepository emailOtpRepository,
            EmailOtpBusinessRules emailOtpBusinessRules
        )
        {
            _mapper = mapper;
            _emailOtpRepository = emailOtpRepository;
            _emailOtpBusinessRules = emailOtpBusinessRules;
        }

        public async Task<GetByIdEmailOtpResponse> Handle(GetByIdEmailOtpQuery request, CancellationToken cancellationToken)
        {
            EmailOtp? emailOtp = await _emailOtpRepository.GetAsync(
                predicate: eo => eo.Id == request.Id,
                cancellationToken: cancellationToken
            );
            await _emailOtpBusinessRules.EmailOtpShouldExistWhenSelected(emailOtp);

            GetByIdEmailOtpResponse response = _mapper.Map<GetByIdEmailOtpResponse>(emailOtp);
            return response;
        }
    }
}
