using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Core.Security.OTP;

public class OtpHelper : IOtpHelper
{
    public string CreateOtpCode(int length = 8)
    {
        if (length < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(length));
        }

        int max = (int)Math.Pow(10, length);
        int min = (int)Math.Pow(10, length - 1);

        int value = RandomNumberGenerator.GetInt32(min, max);
        return value.ToString();
    }
}
