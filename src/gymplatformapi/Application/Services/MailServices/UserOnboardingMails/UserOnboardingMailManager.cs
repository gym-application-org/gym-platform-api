using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Services.MailServices.EmailTemplates;
using Core.Mailing;
using MimeKit;

namespace Application.Services.MailServices.UserOnboardingMails;

public class UserOnboardingMailManager : IUserOnboardingMailService
{
    private readonly IMailService _mailService;
    private readonly IMailTemplateService _mailTemplateService;

    public UserOnboardingMailManager(IMailService mailService, IMailTemplateService mailTemplateService)
    {
        _mailService = mailService;
        _mailTemplateService = mailTemplateService;
    }

    public async Task SendOwnerInviteAsync(
        string email,
        string fullName,
        string tenantName,
        string subdomain,
        string activationLink,
        CancellationToken cancellationToken = default
    )
    {
        MailTemplateResult template = _mailTemplateService.CreateOwnerInviteTemplate(fullName, tenantName, subdomain, activationLink);

        Mail mail = CreateMail(email, fullName, template);

        await _mailService.SendEmailAsync(mail);
    }

    public async Task SendStaffInviteAsync(
        string email,
        string fullName,
        string tenantName,
        string subdomain,
        string activationLink,
        CancellationToken cancellationToken = default
    )
    {
        MailTemplateResult template = _mailTemplateService.CreateStaffInviteTemplate(fullName, tenantName, subdomain, activationLink);

        Mail mail = CreateMail(email, fullName, template);

        await _mailService.SendEmailAsync(mail);
    }

    public async Task SendMemberInviteAsync(
        string email,
        string fullName,
        string tenantName,
        string mobileAppLink,
        string activationLink,
        CancellationToken cancellationToken = default
    )
    {
        MailTemplateResult template = _mailTemplateService.CreateMemberInviteTemplate(fullName, tenantName, mobileAppLink, activationLink);

        Mail mail = CreateMail(email, fullName, template);

        await _mailService.SendEmailAsync(mail);
    }

    private static Mail CreateMail(string email, string fullName, MailTemplateResult template)
    {
        return new Mail(template.Subject, template.TextBody, template.HtmlBody, [new MailboxAddress(fullName, email)]);
    }
}
