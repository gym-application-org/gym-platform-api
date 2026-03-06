using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.MailServices.UserOnboardingMails;

public interface IUserOnboardingMailService
{
    Task SendOwnerInviteAsync(
        string email,
        string fullName,
        string tenantName,
        string subdomain,
        string activationLink,
        CancellationToken cancellationToken = default
    );

    Task SendStaffInviteAsync(
        string email,
        string fullName,
        string tenantName,
        string subdomain,
        string activationLink,
        CancellationToken cancellationToken = default
    );

    Task SendMemberInviteAsync(
        string email,
        string fullName,
        string tenantName,
        string mobileAppLink,
        string activationLink,
        CancellationToken cancellationToken = default
    );
}
