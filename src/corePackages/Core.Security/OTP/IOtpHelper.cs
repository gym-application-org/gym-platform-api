using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Security.OTP;

public interface IOtpHelper
{
    public string CreateOtpCode(int length = 8);
}
