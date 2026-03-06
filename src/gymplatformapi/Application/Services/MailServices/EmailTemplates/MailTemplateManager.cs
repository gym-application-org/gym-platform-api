using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.MailServices.EmailTemplates;

public class MailTemplateManager : IMailTemplateService
{
    private const string BrandName = "Gymmer";
    private const string PrimaryColor = "#4F46E5";
    private const string BackgroundColor = "#F4F7FB";
    private const string CardColor = "#FFFFFF";
    private const string TextColor = "#111827";
    private const string MutedTextColor = "#6B7280";
    private const string BorderColor = "#E5E7EB";
    private const string SuccessSoftColor = "#EEF2FF";

    public MailTemplateResult CreateEmailOtpTemplate(string fullName, string otpCode, string purpose)
    {
        string safeFullName = Encode(fullName);
        string safeOtpCode = Encode(otpCode);
        string safePurpose = Encode(purpose);

        string subject = $"{BrandName} verification code";

        string textBody =
            $@"Hello {fullName},

Your verification code is: {otpCode}

Purpose: {purpose}
This code will expire shortly.

If you did not request this code, you can safely ignore this email.";

        string htmlBody = BuildLayout(
            preheader: "Your verification code is ready.",
            title: "Verify your email",
            introHtml: $@"Hello <strong>{safeFullName}</strong>,",
            contentHtml: $@"
<p style=""margin:0 0 16px 0;"">
Use the verification code below to continue.
</p>

<div style=""margin:24px 0;padding:18px 20px;background:{SuccessSoftColor};border:1px solid {BorderColor};border-radius:12px;text-align:center;"">
    <div style=""font-size:12px;letter-spacing:1.2px;text-transform:uppercase;color:{MutedTextColor};margin-bottom:8px;"">
        Verification Code
    </div>
    <div style=""font-size:32px;font-weight:700;letter-spacing:6px;color:{TextColor};"">
        {safeOtpCode}
    </div>
</div>

<p style=""margin:0 0 12px 0;color:{MutedTextColor};"">
Purpose: <strong style=""color:{TextColor};"">{safePurpose}</strong>
</p>
<p style=""margin:0;color:{MutedTextColor};"">
This code will expire shortly. If you did not request it, you can safely ignore this email.
</p>",
            buttonText: null,
            buttonUrl: null
        );

        return new MailTemplateResult(subject, textBody, htmlBody);
    }

    public MailTemplateResult CreatePasswordResetTemplate(string fullName, string resetLink)
    {
        string safeFullName = Encode(fullName);
        string safeResetLink = Encode(resetLink);

        string subject = $"Reset your {BrandName} password";

        string textBody =
            $@"Hello {fullName},

We received a request to reset your password.

Use the link below to reset it:
{resetLink}

If you did not request this, you can safely ignore this email.";

        string htmlBody = BuildLayout(
            preheader: "Reset your password securely.",
            title: "Reset your password",
            introHtml: $@"Hello <strong>{safeFullName}</strong>,",
            contentHtml: @"
<p style=""margin:0 0 16px 0;"">
We received a request to reset your password. Click the button below to choose a new password.
</p>
<p style=""margin:0;color:#6B7280;"">
If you did not request this, you can safely ignore this email.
</p>",
            buttonText: "Reset Password",
            buttonUrl: safeResetLink,
            fallbackUrl: safeResetLink
        );

        return new MailTemplateResult(subject, textBody, htmlBody);
    }

    public MailTemplateResult CreateAccountActivationTemplate(string fullName, string activationLink)
    {
        string safeFullName = Encode(fullName);
        string safeActivationLink = Encode(activationLink);

        string subject = $"Activate your {BrandName} account";

        string textBody =
            $@"Hello {fullName},

Your account is ready.

Use the link below to activate your account:
{activationLink}

If you did not expect this email, you can safely ignore it.";

        string htmlBody = BuildLayout(
            preheader: "Activate your account to get started.",
            title: "Activate your account",
            introHtml: $@"Hello <strong>{safeFullName}</strong>,",
            contentHtml: @"
<p style=""margin:0 0 16px 0;"">
Your account has been created successfully. Click the button below to activate it and continue.
</p>
<p style=""margin:0;color:#6B7280;"">
If you did not expect this email, you can safely ignore it.
</p>",
            buttonText: "Activate Account",
            buttonUrl: safeActivationLink,
            fallbackUrl: safeActivationLink
        );

        return new MailTemplateResult(subject, textBody, htmlBody);
    }

    public MailTemplateResult CreateFirstPasswordSetupTemplate(string fullName, string setupLink)
    {
        string safeFullName = Encode(fullName);
        string safeSetupLink = Encode(setupLink);

        string subject = $"Set your {BrandName} password";

        string textBody =
            $@"Hello {fullName},

Please use the link below to set your password and complete your first sign-in:
{setupLink}

If you did not expect this email, you can safely ignore it.";

        string htmlBody = BuildLayout(
            preheader: "Set your password to complete account setup.",
            title: "Set your password",
            introHtml: $@"Hello <strong>{safeFullName}</strong>,",
            contentHtml: @"
<p style=""margin:0 0 16px 0;"">
Click the button below to set your password and complete your first sign-in.
</p>
<p style=""margin:0;color:#6B7280;"">
If you did not expect this email, you can safely ignore it.
</p>",
            buttonText: "Set Password",
            buttonUrl: safeSetupLink,
            fallbackUrl: safeSetupLink
        );

        return new MailTemplateResult(subject, textBody, htmlBody);
    }

    public MailTemplateResult CreateOwnerInviteTemplate(string fullName, string tenantName, string subdomain, string activationLink)
    {
        string safeFullName = Encode(fullName);
        string safeTenantName = Encode(tenantName);
        string safeSubdomain = Encode(subdomain);
        string safeActivationLink = Encode(activationLink);

        string subject = $"You are invited as owner of {tenantName}";

        string textBody =
            $@"Hello {fullName},

You have been invited as the owner of {tenantName}.

Workspace: {subdomain}
Activation link:
{activationLink}

Please activate your account to get started.";

        string htmlBody = BuildLayout(
            preheader: $"Owner invitation for {safeTenantName}.",
            title: $"Welcome to {safeTenantName}",
            introHtml: $@"Hello <strong>{safeFullName}</strong>,",
            contentHtml: $@"
<p style=""margin:0 0 16px 0;"">
You have been invited as the <strong>owner</strong> of <strong>{safeTenantName}</strong>.
</p>

<div style=""margin:20px 0;padding:16px 18px;border:1px solid {BorderColor};border-radius:12px;background:#FAFAFA;"">
    <div style=""font-size:12px;text-transform:uppercase;letter-spacing:1px;color:{MutedTextColor};margin-bottom:6px;"">
        Workspace
    </div>
    <div style=""font-size:16px;font-weight:600;color:{TextColor};"">
        {safeSubdomain}
    </div>
</div>

<p style=""margin:0;color:{MutedTextColor};"">
Activate your account to access your workspace and complete your first sign-in.
</p>",
            buttonText: "Activate Account",
            buttonUrl: safeActivationLink,
            fallbackUrl: safeActivationLink
        );

        return new MailTemplateResult(subject, textBody, htmlBody);
    }

    public MailTemplateResult CreateStaffInviteTemplate(string fullName, string tenantName, string subdomain, string activationLink)
    {
        string safeFullName = Encode(fullName);
        string safeTenantName = Encode(tenantName);
        string safeSubdomain = Encode(subdomain);
        string safeActivationLink = Encode(activationLink);

        string subject = $"You are invited to join {tenantName} staff";

        string textBody =
            $@"Hello {fullName},

You have been invited to join {tenantName} as a staff member.

Workspace: {subdomain}
Activation link:
{activationLink}

Please activate your account to get started.";

        string htmlBody = BuildLayout(
            preheader: $"Staff invitation for {safeTenantName}.",
            title: $"Join {safeTenantName}",
            introHtml: $@"Hello <strong>{safeFullName}</strong>,",
            contentHtml: $@"
<p style=""margin:0 0 16px 0;"">
You have been invited to join <strong>{safeTenantName}</strong> as a <strong>staff member</strong>.
</p>

<div style=""margin:20px 0;padding:16px 18px;border:1px solid {BorderColor};border-radius:12px;background:#FAFAFA;"">
    <div style=""font-size:12px;text-transform:uppercase;letter-spacing:1px;color:{MutedTextColor};margin-bottom:6px;"">
        Workspace
    </div>
    <div style=""font-size:16px;font-weight:600;color:{TextColor};"">
        {safeSubdomain}
    </div>
</div>

<p style=""margin:0;color:{MutedTextColor};"">
Activate your account to sign in and start using the platform.
</p>",
            buttonText: "Activate Account",
            buttonUrl: safeActivationLink,
            fallbackUrl: safeActivationLink
        );

        return new MailTemplateResult(subject, textBody, htmlBody);
    }

    public MailTemplateResult CreateMemberInviteTemplate(string fullName, string tenantName, string mobileAppLink, string activationLink)
    {
        string safeFullName = Encode(fullName);
        string safeTenantName = Encode(tenantName);
        string safeMobileAppLink = Encode(mobileAppLink);
        string safeActivationLink = Encode(activationLink);

        string subject = $"Welcome to {tenantName}";

        string textBody =
            $@"Hello {fullName},

Welcome to {tenantName}.

Activate your account:
{activationLink}

Download the mobile app:
{mobileAppLink}

After activation, you can sign in and start using the app.";

        string htmlBody = BuildLayout(
            preheader: $"Your member account for {safeTenantName} is ready.",
            title: $"Welcome to {safeTenantName}",
            introHtml: $@"Hello <strong>{safeFullName}</strong>,",
            contentHtml: $@"
<p style=""margin:0 0 16px 0;"">
Your member account for <strong>{safeTenantName}</strong> has been created.
</p>
<p style=""margin:0 0 16px 0;color:{MutedTextColor};"">
Activate your account first, then download and sign in to the mobile application.
</p>
<p style=""margin:0;color:{MutedTextColor};"">
Mobile app link:
<a href=""{safeMobileAppLink}"" style=""color:{PrimaryColor};text-decoration:none;"">{safeMobileAppLink}</a>
</p>",
            buttonText: "Activate Account",
            buttonUrl: safeActivationLink,
            fallbackUrl: safeActivationLink
        );

        return new MailTemplateResult(subject, textBody, htmlBody);
    }

    private static string Encode(string? value)
    {
        return WebUtility.HtmlEncode(value ?? string.Empty);
    }

    private string BuildLayout(
        string preheader,
        string title,
        string introHtml,
        string contentHtml,
        string? buttonText,
        string? buttonUrl,
        string? fallbackUrl = null
    )
    {
        string actionSection = string.Empty;

        if (!string.IsNullOrWhiteSpace(buttonText) && !string.IsNullOrWhiteSpace(buttonUrl))
        {
            actionSection =
                $@"
<tr>
    <td style=""padding:0 40px 32px 40px;text-align:center;"">
        <a href=""{buttonUrl}""
           style=""display:inline-block;background:{PrimaryColor};color:#FFFFFF;text-decoration:none;padding:14px 26px;border-radius:10px;font-size:14px;font-weight:600;"">
            {Encode(buttonText)}
        </a>
    </td>
</tr>";
        }

        string fallbackSection = string.Empty;

        if (!string.IsNullOrWhiteSpace(fallbackUrl))
        {
            fallbackSection =
                $@"
<tr>
    <td style=""padding:0 40px 24px 40px;font-size:12px;line-height:1.6;color:{MutedTextColor};word-break:break-word;"">
        If the button does not work, copy and paste this link into your browser:<br>
        <a href=""{fallbackUrl}"" style=""color:{PrimaryColor};text-decoration:none;"">{fallbackUrl}</a>
    </td>
</tr>";
        }

        return $@"<!DOCTYPE html>
<html lang=""en"">
<head>
    <meta charset=""UTF-8"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
    <title>{Encode(title)}</title>
</head>
<body style=""margin:0;padding:0;background:{BackgroundColor};font-family:Arial,Helvetica,sans-serif;color:{TextColor};"">
    <div style=""display:none;max-height:0;overflow:hidden;opacity:0;"">
        {Encode(preheader)}
    </div>

    <table role=""presentation"" width=""100%"" cellpadding=""0"" cellspacing=""0"" style=""background:{BackgroundColor};margin:0;padding:24px 0;"">
        <tr>
            <td align=""center"">
                <table role=""presentation"" width=""100%"" cellpadding=""0"" cellspacing=""0"" style=""max-width:640px;background:{CardColor};border:1px solid {BorderColor};border-radius:16px;overflow:hidden;"">
                    <tr>
                        <td style=""padding:24px 40px 12px 40px;font-size:14px;font-weight:700;color:{PrimaryColor};letter-spacing:0.3px;"">
                            {BrandName}
                        </td>
                    </tr>

                    <tr>
                        <td style=""padding:8px 40px 12px 40px;font-size:28px;line-height:1.3;font-weight:700;color:{TextColor};"">
                            {Encode(title)}
                        </td>
                    </tr>

                    <tr>
                        <td style=""padding:0 40px 20px 40px;font-size:15px;line-height:1.7;color:{TextColor};"">
                            {introHtml}
                        </td>
                    </tr>

                    <tr>
                        <td style=""padding:0 40px 32px 40px;font-size:15px;line-height:1.7;color:{TextColor};"">
                            {contentHtml}
                        </td>
                    </tr>

                    {actionSection}
                    {fallbackSection}

                    <tr>
                        <td style=""padding:24px 40px;border-top:1px solid {BorderColor};font-size:12px;line-height:1.6;color:{MutedTextColor};"">
                            This email was sent by {BrandName}. If you did not expect this message, you can safely ignore it.
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</body>
</html>";
    }
}
