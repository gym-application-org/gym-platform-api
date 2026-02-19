using Core.Persistence.Repositories;
using Core.Security.Enums;

namespace Core.Security.Entities;

public class User : Entity<int>
{
    public string Email { get; set; }
    public byte[] PasswordSalt { get; set; }
    public byte[] PasswordHash { get; set; }
    public bool Status { get; set; }
    public AuthenticatorType AuthenticatorType { get; set; }

    public virtual ICollection<UserOperationClaim> UserOperationClaims { get; set; } = null!;
    public virtual ICollection<RefreshToken> RefreshTokens { get; set; } = null!;
    public virtual ICollection<EmailAuthenticator> EmailAuthenticators { get; set; } = null!;
    public virtual ICollection<OtpAuthenticator> OtpAuthenticators { get; set; } = null!;

    public User()
    {
        Email = string.Empty;
        PasswordHash = Array.Empty<byte>();
        PasswordSalt = Array.Empty<byte>();
    }

    public User(string email, byte[] passwordSalt, byte[] passwordHash, bool status, AuthenticatorType authenticatorType)
    {
        Email = email;
        PasswordSalt = passwordSalt;
        PasswordHash = passwordHash;
        Status = status;
        AuthenticatorType = authenticatorType;
    }

    public User(int id, string email, byte[] passwordSalt, byte[] passwordHash, bool status, AuthenticatorType authenticatorType)
        : base(id)
    {
        Email = email;
        PasswordSalt = passwordSalt;
        PasswordHash = passwordHash;
        Status = status;
        AuthenticatorType = authenticatorType;
    }
}
