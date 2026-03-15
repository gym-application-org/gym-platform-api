using Application.Features.Tenants.Constants;
using Application.Features.Tenants.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using Core.Security.Constants;
using Domain.Entities;
using MediatR;
using static Application.Features.Tenants.Constants.TenantsOperationClaims;

namespace Application.Features.Tenants.Commands.Update;

public class UpdateTenantCommand : IRequest<UpdatedTenantResponse>, ISecuredRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public bool IsActive { get; set; }

    public string[] Roles => [GeneralOperationClaims.Admin];

    public class UpdateTenantCommandHandler : IRequestHandler<UpdateTenantCommand, UpdatedTenantResponse>
    {
        private readonly IMapper _mapper;
        private readonly ITenantRepository _tenantRepository;
        private readonly TenantBusinessRules _tenantBusinessRules;

        public UpdateTenantCommandHandler(IMapper mapper, ITenantRepository tenantRepository, TenantBusinessRules tenantBusinessRules)
        {
            _mapper = mapper;
            _tenantRepository = tenantRepository;
            _tenantBusinessRules = tenantBusinessRules;
        }

        public async Task<UpdatedTenantResponse> Handle(UpdateTenantCommand request, CancellationToken cancellationToken)
        {
            Tenant? tenant = await _tenantRepository.GetAsync(predicate: t => t.Id == request.Id, cancellationToken: cancellationToken);
            await _tenantBusinessRules.TenantShouldExistWhenSelected(tenant);
            tenant = _mapper.Map(request, tenant);

            await _tenantRepository.UpdateAsync(tenant!);

            UpdatedTenantResponse response = _mapper.Map<UpdatedTenantResponse>(tenant);
            return response;
        }
    }
}
