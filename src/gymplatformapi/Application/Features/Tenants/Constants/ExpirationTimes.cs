using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Tenants.Constants;

public static class ExpirationTimes
{
    public static readonly TimeSpan ActivationLinkExpiration = TimeSpan.FromHours(48);
    public static readonly TimeSpan ResetPasswordLinkExpiration = TimeSpan.FromMinutes(30);
}
