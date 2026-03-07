using System.Security.Cryptography;
using System.Text;

namespace Core.Security.Hashing;

public static class HashingHelper
{
    public static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        using HMACSHA512 hmac = new();

        passwordSalt = hmac.Key;
        passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
    }

    public static bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
    {
        using HMACSHA512 hmac = new(passwordSalt);

        byte[] computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

        return computedHash.SequenceEqual(passwordHash);
    }

    public static void CreateOtpHash(string otp, string userId, out byte[] otpHash, out byte[] otpSalt)
    {
        using HMACSHA512 hmac = new();

        otpSalt = hmac.Key;
        otpHash = hmac.ComputeHash(Encoding.UTF8.GetBytes($"{otp}:x:{userId}"));
    }

    public static bool VerifyOtpHash(string otp, string userId, byte[] otpHash, byte[] otpSalt)
    {
        using HMACSHA512 hmac = new(otpSalt);

        byte[] computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes($"{otp}:x:{userId}"));

        return computedHash.SequenceEqual(otpHash);
    }
}
