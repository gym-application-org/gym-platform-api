using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enums;

public enum OtpPurpose
{
    Login = 0,
    PasswordReset = 1,
    EmailVerification = 2,
    AccountActivation = 3
}
