using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Core.Security.Encryption;
using Core.Security.Entities;
using Core.Security.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Core.Security.JWT;

public class JwtHelper : ITokenHelper
{
    public IConfiguration Configuration { get; }
    private readonly TokenOptions _tokenOptions;
    private DateTime _accessTokenExpiration;

    public JwtHelper(IConfiguration configuration)
    {
        Configuration = configuration;
        const string configurationSection = "TokenOptions";
        _tokenOptions =
            Configuration.GetSection(configurationSection).Get<TokenOptions>()
            ?? throw new NullReferenceException($"\"{configurationSection}\" section cannot found in configuration.");
    }

    public AccessToken CreateToken(User user, IList<OperationClaim> operationClaims)
    {
        _accessTokenExpiration = DateTime.UtcNow.AddMinutes(_tokenOptions.AccessTokenExpiration);
        SecurityKey securityKey = SecurityKeyHelper.CreateSecurityKey(_tokenOptions.SecurityKey);
        SigningCredentials signingCredentials = SigningCredentialsHelper.CreateSigningCredentials(securityKey);
        JwtSecurityToken jwt = CreateJwtSecurityToken(_tokenOptions, user, signingCredentials, operationClaims);
        JwtSecurityTokenHandler jwtSecurityTokenHandler = new();
        string? token = jwtSecurityTokenHandler.WriteToken(jwt);

        return new AccessToken { Token = token, Expiration = _accessTokenExpiration };
    }

    public RefreshToken CreateRefreshToken(User user, string ipAddress)
    {
        RefreshToken refreshToken =
            new()
            {
                UserId = user.Id,
                Token = RandomRefreshToken(),
                Expires = DateTime.UtcNow.AddDays(7),
                CreatedByIp = ipAddress
            };

        return refreshToken;
    }

    public JwtSecurityToken CreateJwtSecurityToken(
        TokenOptions tokenOptions,
        User user,
        SigningCredentials signingCredentials,
        IList<OperationClaim> operationClaims
    )
    {
        DateTime now = DateTime.UtcNow;

        JwtSecurityToken jwt =
            new(
                tokenOptions.Issuer,
                tokenOptions.Audience,
                expires: _accessTokenExpiration,
                notBefore: now,
                claims: SetClaims(user, operationClaims, now),
                signingCredentials: signingCredentials
            );
        return jwt;
    }

    private IEnumerable<Claim> SetClaims(User user, IList<OperationClaim> operationClaims, DateTime nowUtc)
    {
        List<Claim> claims = [];
        claims.AddNameIdentifier(user.Id.ToString());

        claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()));
        claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
        claims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixTimeSeconds(nowUtc).ToString()));

        claims.AddEmail(user.Email);

        claims.AddRoles(operationClaims.Select(c => c.Name).ToArray());
        return claims;
    }

    private static long ToUnixTimeSeconds(DateTime utc)
    {
        DateTimeOffset dto = new DateTimeOffset(utc, TimeSpan.Zero);
        return dto.ToUnixTimeSeconds();
    }

    private string RandomRefreshToken()
    {
        byte[] numberByte = new byte[32];
        using var random = RandomNumberGenerator.Create();
        random.GetBytes(numberByte);
        return Convert.ToBase64String(numberByte);
    }
}
