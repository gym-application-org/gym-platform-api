using Application.Features.DietAssignments.Constants;
using Application.Features.DietAssignments.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Domain.Entities;
using MediatR;
using static Application.Features.DietAssignments.Constants.DietAssignmentsOperationClaims;

namespace Application.Features.DietAssignments.Queries.GetById;

public class GetByIdDietAssignmentQuery : IRequest<GetByIdDietAssignmentResponse>, ISecuredRequest
{
    public int Id { get; set; }

    public string[] Roles => [Admin, Read];

    public class GetByIdDietAssignmentQueryHandler : IRequestHandler<GetByIdDietAssignmentQuery, GetByIdDietAssignmentResponse>
    {
        private readonly IMapper _mapper;
        private readonly IDietAssignmentRepository _dietAssignmentRepository;
        private readonly DietAssignmentBusinessRules _dietAssignmentBusinessRules;

        public GetByIdDietAssignmentQueryHandler(
            IMapper mapper,
            IDietAssignmentRepository dietAssignmentRepository,
            DietAssignmentBusinessRules dietAssignmentBusinessRules
        )
        {
            _mapper = mapper;
            _dietAssignmentRepository = dietAssignmentRepository;
            _dietAssignmentBusinessRules = dietAssignmentBusinessRules;
        }

        public async Task<GetByIdDietAssignmentResponse> Handle(GetByIdDietAssignmentQuery request, CancellationToken cancellationToken)
        {
            DietAssignment? dietAssignment = await _dietAssignmentRepository.GetAsync(
                predicate: da => da.Id == request.Id,
                cancellationToken: cancellationToken
            );
            await _dietAssignmentBusinessRules.DietAssignmentShouldExistWhenSelected(dietAssignment);

            GetByIdDietAssignmentResponse response = _mapper.Map<GetByIdDietAssignmentResponse>(dietAssignment);
            return response;
        }
    }
}
