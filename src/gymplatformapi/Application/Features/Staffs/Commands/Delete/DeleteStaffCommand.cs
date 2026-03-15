using Application.Features.Staffs.Constants;
using Application.Features.Staffs.Constants;
using Application.Features.Staffs.Rules;
using Application.Services.Repositories;
using Application.Services.UsersService;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using Core.Security.Constants;
using Core.Security.Entities;
using Domain.Entities;
using MediatR;
using static Application.Features.Staffs.Constants.StaffsOperationClaims;

namespace Application.Features.Staffs.Commands.Delete;

public class DeleteStaffCommand : IRequest<DeletedStaffResponse>, ISecuredRequest, ILoggableRequest, ITransactionalRequest, ITenantRequest
{
    public Guid Id { get; set; }

    public string[] Roles => [GeneralOperationClaims.Owner];

    public class DeleteStaffCommandHandler : IRequestHandler<DeleteStaffCommand, DeletedStaffResponse>
    {
        private readonly IMapper _mapper;
        private readonly IStaffRepository _staffRepository;
        private readonly StaffBusinessRules _staffBusinessRules;
        private readonly IUserService _userService;

        public DeleteStaffCommandHandler(
            IMapper mapper,
            IUserService userService,
            IStaffRepository staffRepository,
            StaffBusinessRules staffBusinessRules
        )
        {
            _mapper = mapper;
            _userService = userService;
            _staffRepository = staffRepository;
            _staffBusinessRules = staffBusinessRules;
        }

        public async Task<DeletedStaffResponse> Handle(DeleteStaffCommand request, CancellationToken cancellationToken)
        {
            Staff? staff = await _staffRepository.GetAsync(predicate: s => s.Id == request.Id, cancellationToken: cancellationToken);
            await _staffBusinessRules.StaffShouldExistWhenSelected(staff);

            User? user = await _userService.GetAsync(predicate: x => x.Id == staff!.UserId, cancellationToken: cancellationToken);

            await _staffRepository.DeleteAsync(staff!);
            await _userService.DeleteAsync(user!);

            DeletedStaffResponse response = _mapper.Map<DeletedStaffResponse>(staff);
            return response;
        }
    }
}
