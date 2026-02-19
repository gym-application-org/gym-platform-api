using Core.Application.Abstractions.Security;
using Core.Application.Constants;

namespace WebAPI.Services.Security;

public class CurrentTenant : ICurrentTenant
{
    private readonly IHttpContextAccessor _contextAccessor;

    public CurrentTenant(IHttpContextAccessor contextAccessor)
    {
        _contextAccessor = contextAccessor;
    }

    public Guid? TenantId
    {
        get
        {
            HttpContext? context = _contextAccessor.HttpContext;
            if (context == null)
            {
                return null;
            }

            if (
                context.Request.Headers.TryGetValue(TenantHeaders.TenantId, out var headerValues)
                && Guid.TryParse(headerValues.FirstOrDefault(), out Guid tenantId)
            )
            {
                return tenantId;
            }

            return null;
        }
    }

    public bool HasTenant => TenantId.HasValue;
}
