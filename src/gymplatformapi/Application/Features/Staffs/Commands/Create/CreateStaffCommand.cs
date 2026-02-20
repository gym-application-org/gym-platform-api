using Application.Features.Staffs.Constants;
using Application.Features.Staffs.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using static Application.Features.Staffs.Constants.StaffsOperationClaims;

namespace Application.Features.Staffs.Commands.Create;

public class CreateStaffCommand
    : IRequest<CreatedStaffResponse>,
        ISecuredRequest,
        ICacheRemoverRequest,
        ILoggableRequest,
        ITransactionalRequest
{
    public string Name { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public StaffRole Role { get; set; }
    public bool IsActive { get; set; }
    public int UserId { get; set; }

    public string[] Roles => [Admin, Write, StaffsOperationClaims.Create];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetStaffs"];

    public class CreateStaffCommandHandler : IRequestHandler<CreateStaffCommand, CreatedStaffResponse>
    {
        private readonly IMapper _mapper;
        private readonly IStaffRepository _staffRepository;
        private readonly StaffBusinessRules _staffBusinessRules;

        public CreateStaffCommandHandler(IMapper mapper, IStaffRepository staffRepository, StaffBusinessRules staffBusinessRules)
        {
            _mapper = mapper;
            _staffRepository = staffRepository;
            _staffBusinessRules = staffBusinessRules;
        }

        public async Task<CreatedStaffResponse> Handle(CreateStaffCommand request, CancellationToken cancellationToken)
        {
            Staff staff = _mapper.Map<Staff>(request);

            await _staffRepository.AddAsync(staff);

            CreatedStaffResponse response = _mapper.Map<CreatedStaffResponse>(staff);
            return response;
        }
    }
}
