using Application.Features.DietAssignments.Constants;
using Application.Features.DietAssignments.Queries.GetMyDietAssignemnts;
using Application.Features.DietAssignments.Rules;
using Application.Services.Members;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Abstractions.Security;
using Core.Application.Pipelines.Authorization;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Core.Security.Constants;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static Application.Features.DietAssignments.Constants.DietAssignmentsOperationClaims;

namespace Application.Features.DietAssignments.Queries.GetMyDietAssignmentById;

public class GetMyDietAssignmentByIdQuery : IRequest<GetMyDietAssignmentByIdResponse>, ISecuredRequest, ITenantRequest
{
    public int Id { get; set; }

    public string[] Roles => [GeneralOperationClaims.Member];

    public class GetMyDietAssignmentByIdQueryHandler : IRequestHandler<GetMyDietAssignmentByIdQuery, GetMyDietAssignmentByIdResponse>
    {
        private readonly IDietAssignmentRepository _dietAssignmentRepository;
        private readonly IMapper _mapper;
        private readonly ICurrentUser _currentUser;
        private readonly IMemberService _memberService;
        private readonly DietAssignmentBusinessRules _dietAssignmentBusinessRules;

        public GetMyDietAssignmentByIdQueryHandler(
            IDietAssignmentRepository dietAssignmentRepository,
            IMapper mapper,
            ICurrentUser currentUser,
            IMemberService memberService,
            DietAssignmentBusinessRules dietAssignmentBusinessRules
        )
        {
            _dietAssignmentRepository = dietAssignmentRepository;
            _mapper = mapper;
            _currentUser = currentUser;
            _memberService = memberService;
            _dietAssignmentBusinessRules = dietAssignmentBusinessRules;
        }

        public async Task<GetMyDietAssignmentByIdResponse> Handle(GetMyDietAssignmentByIdQuery request, CancellationToken cancellationToken)
        {
            Member? member = await _memberService.GetAsync(x => x.UserId == _currentUser.UserId, cancellationToken: cancellationToken);
            await _dietAssignmentBusinessRules.MemberShouldExistWhenSelected(member);

            DietAssignment? activeDietAssignment = await _dietAssignmentRepository.GetAsync(
                predicate: x => x.MemberId == member!.Id && x.Id == request.Id,
                include: q => q.Include(x => x.DietTemplate).ThenInclude(t => t.Days).ThenInclude(d => d.Meals).ThenInclude(m => m.Items),
                enableTracking: false,
                cancellationToken: cancellationToken
            );
            await _dietAssignmentBusinessRules.DietAssignmentShouldExistWhenSelected(activeDietAssignment);

            GetMyDietAssignmentByIdResponse response = _mapper.Map<GetMyDietAssignmentByIdResponse>(activeDietAssignment);
            return response;
        }
    }
}
