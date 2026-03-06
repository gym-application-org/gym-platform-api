using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Services.MailServices.EmailTemplates;
using Core.Mailing;
using Domain.Enums;
using MimeKit;

namespace Application.Services.MailServices.AuthMails;

public class AuthMailManager : IAuthMailService
{
    private readonly IMailService _mailService;
    private readonly IMailTemplateService _mailTemplateService;

    public AuthMailManager(IMailService mailService, IMailTemplateService mailTemplateService)
    {
        _mailService = mailService;
        _mailTemplateService = mailTemplateService;
    }

    public async Task SendEmailOtpAsync(
        string email,
        string fullName,
        string otpCode,
        OtpPurpose purpose,
        CancellationToken cancellationToken = default
    )
    {
        MailTemplateResult template = _mailTemplateService.CreateEmailOtpTemplate(fullName, otpCode, purpose.ToString());

        Mail mail = CreateMail(email, fullName, template);

        await _mailService.SendEmailAsync(mail);
    }

    public async Task SendPasswordResetAsync(string email, string fullName, string resetLink, CancellationToken cancellationToken = default)
    {
        MailTemplateResult template = _mailTemplateService.CreatePasswordResetTemplate(fullName, resetLink);

        Mail mail = CreateMail(email, fullName, template);

        await _mailService.SendEmailAsync(mail);
    }

    public async Task SendAccountActivationAsync(
        string email,
        string fullName,
        string activationLink,
        CancellationToken cancellationToken = default
    )
    {
        MailTemplateResult template = _mailTemplateService.CreateAccountActivationTemplate(fullName, activationLink);

        Mail mail = CreateMail(email, fullName, template);

        await _mailService.SendEmailAsync(mail);
    }

    public async Task SendFirstPasswordSetupAsync(
        string email,
        string fullName,
        string setupLink,
        CancellationToken cancellationToken = default
    )
    {
        MailTemplateResult template = _mailTemplateService.CreateFirstPasswordSetupTemplate(fullName, setupLink);

        Mail mail = CreateMail(email, fullName, template);

        await _mailService.SendEmailAsync(mail);
    }

    private static Mail CreateMail(string email, string fullName, MailTemplateResult template)
    {
        return new Mail(template.Subject, template.TextBody, template.HtmlBody, [new MailboxAddress(fullName, email)]);
    }
}
