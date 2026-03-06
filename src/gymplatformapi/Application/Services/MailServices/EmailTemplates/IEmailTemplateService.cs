using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.MailServices.EmailTemplates;

public interface IMailTemplateService
{
    MailTemplateResult CreateEmailOtpTemplate(string fullName, string otpCode, string purpose);

    MailTemplateResult CreatePasswordResetTemplate(string fullName, string resetLink);

    MailTemplateResult CreateAccountActivationTemplate(string fullName, string activationLink);

    MailTemplateResult CreateFirstPasswordSetupTemplate(string fullName, string setupLink);

    MailTemplateResult CreateOwnerInviteTemplate(string fullName, string tenantName, string subdomain, string activationLink);

    MailTemplateResult CreateStaffInviteTemplate(string fullName, string tenantName, string subdomain, string activationLink);

    MailTemplateResult CreateMemberInviteTemplate(string fullName, string tenantName, string mobileAppLink, string activationLink);
}
