using Application.Features.Staffs.Constants;
using Application.Features.Staffs.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Domain.Entities;
using MediatR;
using static Application.Features.Staffs.Constants.StaffsOperationClaims;

namespace Application.Features.Staffs.Queries.GetById;

public class GetByIdStaffQuery : IRequest<GetByIdStaffResponse>, ISecuredRequest
{
    public Guid Id { get; set; }

    public string[] Roles => [Admin, Read];

    public class GetByIdStaffQueryHandler : IRequestHandler<GetByIdStaffQuery, GetByIdStaffResponse>
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

        public async Task<GetByIdStaffResponse> Handle(GetByIdStaffQuery request, CancellationToken cancellationToken)
        {
            Staff? staff = await _staffRepository.GetAsync(predicate: s => s.Id == request.Id, cancellationToken: cancellationToken);
            await _staffBusinessRules.StaffShouldExistWhenSelected(staff);

            GetByIdStaffResponse response = _mapper.Map<GetByIdStaffResponse>(staff);
            return response;
        }
    }
}
