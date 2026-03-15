using Application.Features.Staffs.Constants;
using Application.Features.Staffs.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using Core.Security.Constants;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using static Application.Features.Staffs.Constants.StaffsOperationClaims;

namespace Application.Features.Staffs.Commands.Update;

public class UpdateStaffCommand : IRequest<UpdatedStaffResponse>, ISecuredRequest, ILoggableRequest, ITransactionalRequest, ITenantRequest
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public bool IsActive { get; set; }

    public string[] Roles => [GeneralOperationClaims.Owner];

    public class UpdateStaffCommandHandler : IRequestHandler<UpdateStaffCommand, UpdatedStaffResponse>
    {
        private readonly IMapper _mapper;
        private readonly IStaffRepository _staffRepository;
        private readonly StaffBusinessRules _staffBusinessRules;

        public UpdateStaffCommandHandler(IMapper mapper, IStaffRepository staffRepository, StaffBusinessRules staffBusinessRules)
        {
            _mapper = mapper;
            _staffRepository = staffRepository;
            _staffBusinessRules = staffBusinessRules;
        }

        public async Task<UpdatedStaffResponse> Handle(UpdateStaffCommand request, CancellationToken cancellationToken)
        {
            Staff? staff = await _staffRepository.GetAsync(predicate: s => s.Id == request.Id, cancellationToken: cancellationToken);
            await _staffBusinessRules.StaffShouldExistWhenSelected(staff);
            staff = _mapper.Map(request, staff);

            await _staffRepository.UpdateAsync(staff!);

            UpdatedStaffResponse response = _mapper.Map<UpdatedStaffResponse>(staff);
            return response;
        }
    }
}
