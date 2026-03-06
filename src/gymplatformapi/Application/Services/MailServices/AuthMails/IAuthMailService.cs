using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Enums;

namespace Application.Services.MailServices.AuthMails;

public interface IAuthMailService
{
    Task SendEmailOtpAsync(
        string email,
        string fullName,
        string otpCode,
        OtpPurpose purpose,
        CancellationToken cancellationToken = default
    );

    Task SendPasswordResetAsync(string email, string fullName, string resetLink, CancellationToken cancellationToken = default);

    Task SendAccountActivationAsync(string email, string fullName, string activationLink, CancellationToken cancellationToken = default);

    Task SendFirstPasswordSetupAsync(string email, string fullName, string setupLink, CancellationToken cancellationToken = default);
}
