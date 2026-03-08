using System.Security.Cryptography;
using System.Text;

namespace Core.Security.Hashing;

public static class HashingHelper
{
    public static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        if (string.IsNullOrWhiteSpace(password))
            throw new ArgumentException("Password cannot be null or empty.", nameof(password));

        using HMACSHA512 hmac = new();

        passwordSalt = hmac.Key;
        passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
    }

    public static bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
    {
        if (string.IsNullOrWhiteSpace(password))
            return false;

        if (passwordHash is null || passwordHash.Length == 0)
            return false;

        if (passwordSalt is null || passwordSalt.Length == 0)
            return false;

        using HMACSHA512 hmac = new(passwordSalt);

        byte[] computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

        return CryptographicOperations.FixedTimeEquals(computedHash, passwordHash);
    }

    public static void CreateOtpHash(string otp, string userId, out byte[] otpHash, out byte[] otpSalt)
    {
        if (string.IsNullOrWhiteSpace(otp))
            throw new ArgumentException("OTP cannot be null or empty.", nameof(otp));

        if (string.IsNullOrWhiteSpace(userId))
            throw new ArgumentException("UserId cannot be null or empty.", nameof(userId));

        using HMACSHA512 hmac = new();

        otpSalt = hmac.Key;
        otpHash = hmac.ComputeHash(Encoding.UTF8.GetBytes($"{otp}:x:{userId}"));
    }

    public static bool VerifyOtpHash(string otp, string userId, byte[] otpHash, byte[] otpSalt)
    {
        if (string.IsNullOrWhiteSpace(otp) || string.IsNullOrWhiteSpace(userId))
            return false;

        if (otpHash is null || otpHash.Length == 0)
            return false;

        if (otpSalt is null || otpSalt.Length == 0)
            return false;

        using HMACSHA512 hmac = new(otpSalt);

        byte[] computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes($"{otp}:x:{userId}"));

        return CryptographicOperations.FixedTimeEquals(computedHash, otpHash);
    }

    public static void CreateActionTokenHash(string token, out byte[] tokenHash)
    {
        if (string.IsNullOrWhiteSpace(token))
            throw new ArgumentException("Token cannot be null or empty.", nameof(token));

        tokenHash = SHA256.HashData(Encoding.UTF8.GetBytes(token));
    }

    public static bool VerifyActionTokenHash(string token, byte[] tokenHash)
    {
        if (string.IsNullOrWhiteSpace(token))
            return false;

        if (tokenHash is null || tokenHash.Length == 0)
            return false;

        byte[] computedHash = SHA256.HashData(Encoding.UTF8.GetBytes(token));

        return CryptographicOperations.FixedTimeEquals(computedHash, tokenHash);
    }
}
