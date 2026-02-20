using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Core.Application.Abstractions.Security;
using Microsoft.IdentityModel.Tokens;

namespace WebAPI.Services.Security;

public class CurrentUser : ICurrentUser
{
    private readonly IHttpContextAccessor _contextAccessor;

    public CurrentUser(IHttpContextAccessor contextAccessor)
    {
        _contextAccessor = contextAccessor;
    }

    public bool IsAuthenticated
    {
        get
        {
            ClaimsPrincipal? user = _contextAccessor.HttpContext?.User;
            return user?.Identity?.IsAuthenticated == true;
        }
    }

    public int UserId
    {
        get
        {
            ClaimsPrincipal? user = _contextAccessor.HttpContext?.User;
            if (user == null)
            {
                return 0;
            }

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

            return 0;
        }
    }
}
