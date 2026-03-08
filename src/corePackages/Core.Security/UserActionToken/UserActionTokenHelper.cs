using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.WebUtilities;

namespace Core.Security.UserActionToken;

public class UserActionTokenHelper : IUserActionTokenHelper
{
    public string CreateActionToken(int length = 32)
    {
        if (length < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(length));
        }

        byte[] bytes = RandomNumberGenerator.GetBytes(length);
        return WebEncoders.Base64UrlEncode(bytes);
    }
}
