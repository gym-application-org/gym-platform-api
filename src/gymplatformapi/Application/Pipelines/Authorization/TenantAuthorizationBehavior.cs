using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Application.Features.OperationClaims.Constants;
using Application.Services.TenantMembershipService;
using Core.Application.Abstractions.Security;
using Core.Application.Pipelines.Authorization;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.Security.Entities;
using Core.Security.Extensions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;

namespace Application.Pipelines.Authorization;

public sealed class TenantAuthorizationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>, ITenantRequest
{
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly ICurrentTenant _currentTenant;
    private readonly ITenantMembershipService _tenantMembershipService;

    public TenantAuthorizationBehavior(
        IHttpContextAccessor httpContextAccessor,
        ICurrentTenant currentTenant,
        ITenantMembershipService tenantMembershipService
    )
    {
        _contextAccessor = httpContextAccessor;
        _currentTenant = currentTenant;
        _tenantMembershipService = tenantMembershipService;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        HttpContext context = _contextAccessor.HttpContext;
        if (context == null)
        {
            throw new AuthorizationException("You are not authenticated.");
        }

        Guid tenantId = GetTenantIdOrThrow();
        int userId = GetUserIdOrThrow(context.User);

        List<string>? roles = context.User.ClaimRoles();
        bool isAdmin = roles != null && roles.Any(r => r == GeneralOperationClaims.Admin);

        if (!isAdmin)
        {
            bool allowed = await _tenantMembershipService.IsUserAllowedInTenantAsync(userId, tenantId, cancellationToken);

            if (!allowed)
            {
                throw new AuthorizationException("You are not authorized.");
            }
        }

        return await next();
    }

    private Guid GetTenantIdOrThrow()
    {
        if (!_currentTenant.TenantId.HasValue)
        {
            throw new AuthorizationException("Tenant context is required.");
        }
        return _currentTenant.TenantId.Value;
    }

    private static int GetUserIdOrThrow(ClaimsPrincipal user)
    {
        string? sub = user.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub)?.Value;
        if (!sub.IsNullOrEmpty() && int.TryParse(sub, out int subUserId))
        {
            return subUserId;
        }

        string? nameId = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        if (!nameId.IsNullOrEmpty() && int.TryParse(nameId, out int nameIdentifierUserId))
        {
            return nameIdentifierUserId;
        }

        throw new AuthorizationException("You are not autenticated.");
    }
}
