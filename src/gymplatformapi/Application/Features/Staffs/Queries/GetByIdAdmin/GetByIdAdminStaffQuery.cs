using Application.Features.Staffs.Constants;
using Application.Features.Staffs.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Security.Constants;
using Domain.Entities;
using MediatR;
using static Application.Features.Staffs.Constants.StaffsOperationClaims;

namespace Application.Features.Staffs.Queries.GetByIdAdmin;

public class GetByIdAdminStaffQuery : IRequest<GetByIdAdminStaffResponse>, ISecuredRequest
{
    public Guid Id { get; set; }

    public string[] Roles => [GeneralOperationClaims.Admin];

    public class GetByIdStaffQueryHandler : IRequestHandler<GetByIdAdminStaffQuery, GetByIdAdminStaffResponse>
    {
        private readonly IMapper _mapper;
        private readonly IStaffRepository _staffRepository;
        private readonly StaffBusinessRules _staffBusinessRules;

        public GetByIdStaffQueryHandler(IMapper mapper, IStaffRepository staffRepository, StaffBusinessRules staffBusinessRules)
        {
            _mapper = mapper;
            _staffRepository = staffRepository;
            _staffBusinessRules = staffBusinessRules;
        }

        public async Task<GetByIdAdminStaffResponse> Handle(GetByIdAdminStaffQuery request, CancellationToken cancellationToken)
        {
            Staff? staff = await _staffRepository.GetAsync(predicate: s => s.Id == request.Id, cancellationToken: cancellationToken);
            await _staffBusinessRules.StaffShouldExistWhenSelected(staff);

            GetByIdAdminStaffResponse response = _mapper.Map<GetByIdAdminStaffResponse>(staff);
            return response;
        }
    }
}
